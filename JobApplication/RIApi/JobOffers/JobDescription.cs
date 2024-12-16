using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobDescription
    {
        [JsonPropertyName("jobDescriptionId")]
        public string JobDescriptionId { get; init; } = string.Empty;
        [JsonPropertyName("accountingCompany")]
        public string AccountingCompany { get; init; } = string.Empty;
        [JsonPropertyName("jobType")]
        public int JobType { get; init; }
        [JsonPropertyName("jobNumber")]
        public int JobNumber { get; init; }
        [JsonPropertyName("paymentClass")]
        public int PaymentClass { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; init; } = string.Empty;
        [JsonPropertyName("description")]
        public string Description { get; init; } = string.Empty;
        [JsonPropertyName("functions")]
        public string[] Functions { get; init; } = [];
        [JsonPropertyName("skills")]
        public string[] Skills { get; init; } = [];
        [JsonPropertyName("offers")]
        public string[] Offers { get; init; } = [];
        [JsonPropertyName("functionsTitle")]
        public string FunctionsTitle { get; init; } = string.Empty;
        [JsonPropertyName("skillsTitle")]
        public string SkillsTitle { get; init; } = string.Empty;
        [JsonPropertyName("offersTitle")]
        public string OffersTitle { get; init; } = string.Empty;
        [JsonPropertyName("paymentInfo")]
        public string PaymentInfo { get; init; } = string.Empty;
        [JsonPropertyName("paymentInfoAddition")]
        public string PaymentInfoAddition { get; init; } = string.Empty;
    }
}
