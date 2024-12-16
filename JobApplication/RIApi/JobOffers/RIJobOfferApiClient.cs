using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace JobApplication.RIApi.Model
{
    public class RIJobOfferClient
    {
        private readonly HttpClient _httpClient;

        public RIJobOfferClient(HttpClient httpClient, IOptions<RIApiOptions> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);
        }

        public async Task<JobOfferSearchResponse?> SearchJobAsync(JobOfferSearchFilter filter)
        {
            var response = await _httpClient.PostAsJsonAsync("jobs/search", filter);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JobOfferSearchResponse>(responseStream);
        }

        public async Task<JobOfferFiltersResponse?> GetSearchFiltersAsync()
        {
            var content = new StringContent("{}", Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("jobs/search/filters", content);
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JobOfferFiltersResponse>(responseStream);
        }

        public async Task<JobOfferWithDescription?> GetJobDetails(string jobDescriptionId, string jobId)
        {
            var response = await _httpClient.GetAsync($"jobs/{jobDescriptionId}/offers/{jobId}");
            response.EnsureSuccessStatusCode();
            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<JobOfferWithDescription>(responseStream);
        }
    }
}
