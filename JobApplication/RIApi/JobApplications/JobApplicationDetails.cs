using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobApplicationDetails
    {
        [JsonPropertyName("jobId")]
        public string JobId { get; set; } = string.Empty;

        [JsonPropertyName("storeIdList")]
        public int[] StoreIdList { get; set; } = [];

        [JsonPropertyName("desiredSalary")]
        public int DesiredSalary { get; set; }

        [JsonPropertyName("recommendedBy")]
        public string RecommendedBy { get; set; } = string.Empty;

        [JsonPropertyName("availableFrom")]
        [DataType(DataType.Date)]
        public DateTime AvailableFrom { get; set; }

        [JsonPropertyName("agreedToDataProcessing")]
        public bool AgreedToDataProcessing { get; set; }

        [JsonPropertyName("agreedToDataRelaying")]
        public bool AgreedToDataRelaying { get; set; }

        [JsonPropertyName("externalSource")]
        [StringLength(500, MinimumLength = 0)]
        public string ExternalSource { get; set; } = string.Empty;
    }
}
