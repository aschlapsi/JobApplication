using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobOffer
    {
        [JsonPropertyName("jobId")]
        public string JobId { get; init; } = string.Empty;
        [JsonPropertyName("jobDescriptionId")]
        public string JobDescriptionId { get; init; } = string.Empty;
        [JsonPropertyName("accountingCompanyId")]
        public string AccountingCompanyId { get; init; } = string.Empty;
        [JsonPropertyName("accountingCompany")]
        public string AccountingCompany { get; init; } = string.Empty;
        [JsonPropertyName("displayAccountingCompanyId")]
        public string DisplayAccountingCompanyId { get; init; } = string.Empty;
        [JsonPropertyName("displayAccountingCompany")]
        public string DisplayAccountingCompany { get; init; } = string.Empty;
        [JsonPropertyName("jobType")]
        public string JobType { get; init; } = string.Empty;
        [JsonPropertyName("title")]
        public string Title { get; init; } = string.Empty;
        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; init; } = string.Empty;
        [JsonPropertyName("employmentLevelId")]
        public string EmploymentLevelId { get; init; } = string.Empty;
        [JsonPropertyName("employmentLevel")]
        public string EmploymentLevel { get; init; } = string.Empty;
        [JsonPropertyName("jobNumber")]
        public int JobNumber { get; init; }
        [JsonPropertyName("paymentClass")]
        public int PaymentClass { get; init; }
        [JsonPropertyName("jobGroups")]
        public JobGroup[] JobGroups { get; init; } = [];
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; init; } = string.Empty;
        [JsonPropertyName("zip")]
        public string Zip { get; init; } = string.Empty;
        [JsonPropertyName("city")]
        public string City { get; init; } = string.Empty;
        [JsonPropertyName("provinceId")]
        public string ProvinceId { get; init; } = string.Empty;
        [JsonPropertyName("provinceNumber")]
        public int ProvinceNumber { get; init; }
        [JsonPropertyName("provinceName")]
        public string ProvinceName { get; init; } = string.Empty;
        [JsonPropertyName("districtId")]
        public string DistrictId { get; init; } = string.Empty;
        [JsonPropertyName("districtNumber")]
        public int DistrictNumber { get; init; }
        [JsonPropertyName("districtName")]
        public string DistrictName { get; init; } = string.Empty;
        [JsonPropertyName("startDate")]
        public DateTime StartDate { get; init; }
        [JsonPropertyName("creationDate")]
        public DateTime CreationDate { get; init; }
        [JsonPropertyName("hours")]
        public float Hours { get; init; }
        [JsonPropertyName("minHours")]
        public float MinHours { get; init; }
        [JsonPropertyName("amountOfJobs")]
        public int AmountOfJobs { get; init; }
        [JsonPropertyName("stores")]
        public JobStore[] Stores { get; init; } = [];
        [JsonPropertyName("links")]
        public Link[] Links { get; init; } = [];
        [JsonPropertyName("synonyms")]
        public string[] Synonyms { get; init; } = [];
        [JsonPropertyName("jobLevels")]
        public string[] JobLevels { get; init; } = [];
    }
}
