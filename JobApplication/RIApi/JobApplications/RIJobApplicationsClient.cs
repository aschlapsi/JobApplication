using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;

namespace JobApplication.RIApi.JobApplications
{
    public class RIJobApplicationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RIJobApplicationsClient> _logger;

        public RIJobApplicationsClient(
            HttpClient httpClient,
            IOptions<RIApiOptions> options,
            ILogger<RIJobApplicationsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<JobApplicationCredentials?> GetCredentials(string challengeKey, string challengeAnswer)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"job-applications/credentials");
            requestMessage.Headers.Add("ChallengeType", "recaptcha");
            requestMessage.Headers.Add("ChallengeKey", challengeKey);
            requestMessage.Headers.Add("ChallengeAnswer", challengeAnswer);
            var response = await _httpClient.SendAsync(requestMessage);
            return await Deserialize<JobApplicationCredentials>(response);
        }

        public async Task<JobApplication?> GetApplication(JobApplicationCredentials credentials)
        {
            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"job-applications/{credentials.Id}");
            requestMessage.Headers.Add("application-auth-code", credentials.AuthCode);
            var response = await _httpClient.SendAsync(requestMessage);
            return await Deserialize<JobApplication>(response);
        }

        public async Task<Response> PutApplicant(JobApplicationCredentials credentials, JobApplicant applicant)
        {
            var response = await _httpClient.PutAsync(
                $"job-applications/{credentials.Id}/applicant",
                CreateJsonContent(applicant, credentials.AuthCode)
            );

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return new Response<JobDocument>(await DeserializeBadRequestContent(response));

            response.EnsureSuccessStatusCode();
            return new Response();
        }

        public async Task<Response<JobDocument>> PostDocument(JobApplicationCredentials credentials, JobApplicationDocumentRequest jobApplicationDocumentRequest)
        {
            var content = new StringContent(JsonSerializer.Serialize(jobApplicationDocumentRequest), null, "application/json");
            content.Headers.Add("application-auth-code", credentials.AuthCode);

            var response = await _httpClient.PostAsync(
                $"job-applications/{credentials.Id}/documents",
                content
                //CreateJsonContent(jobApplicationDocumentRequest, credentials.AuthCode)
            );

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return new Response<JobDocument>(await DeserializeBadRequestContent(response));

            return new Response<JobDocument>((await Deserialize<JobDocument>(response))!);
        }

        public async Task DeleteDocument(JobApplicationCredentials credentials, int documentId)
        {
            using var requestMessage = new HttpRequestMessage(
                HttpMethod.Delete,
                $"job-applications/{credentials.Id}/documents/{documentId}"
            );
            requestMessage.Headers.Add("application-auth-code", credentials.AuthCode);
            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }

        public async Task Submit(JobApplicationCredentials credentials, JobApplicationDetails jobApplicationDetails)
        {
            var response = await _httpClient.PostAsync(
                $"job-applications/{credentials.Id}/submit",
                CreateJsonContent(jobApplicationDetails, credentials.AuthCode)
            );
            response.EnsureSuccessStatusCode();
        }

        private JsonContent CreateJsonContent<T>(T entity, string authCode)
        {
            var content = JsonContent.Create(entity);
            content.Headers.Add("application-auth-code", authCode);

            return content;
        }

        private async Task<T?> Deserialize<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(responseStream);
        }

        private async Task<BadRequest> DeserializeBadRequestContent(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return (JsonSerializer.Deserialize<BadRequest>(responseContent!))!;
        }
    }
}
