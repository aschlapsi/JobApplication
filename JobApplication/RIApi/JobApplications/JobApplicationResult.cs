using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobApplicationResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("applicationDetails")]
        public JobApplicationDetails ApplicationDetails { get; set; } = new JobApplicationDetails();
        [JsonPropertyName("applicant")]
        public JobApplicant Applicant { get; set; } = new JobApplicant();
        [JsonPropertyName("documents")]
        public JobDocument[] Documents { get; set; } = [];
    }
}
