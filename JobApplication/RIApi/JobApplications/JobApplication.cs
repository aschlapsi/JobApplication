using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{

    public class JobApplication
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("applicant")]
        public JobApplicant Applicant { get; set; } = new JobApplicant();
        [JsonPropertyName("documents")]
        public JobDocument[] Documents { get; set; } = [];
    }
}
