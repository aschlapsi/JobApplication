using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobOfferFiltersResponse
    {
        [JsonPropertyName("accountingCompanies")]
        public AccountingCompany[] AccountingCompanies { get; init; } = [];
        [JsonPropertyName("jobGroups")]
        public JobGroup[] JobGroups { get; init; } = [];
        [JsonPropertyName("employmentLevels")]
        public EmploymentLevel[] EmploymentLevels { get; init; } = [];
        [JsonPropertyName("jobTypes")]
        public JobType[] JobTypes { get; init; } = [];
        [JsonPropertyName("jobLevels")]
        public string[] JobLevels { get; init; } = [];
        [JsonPropertyName("provinces")]
        public Province[] Provinces { get; init; } = [];
        [JsonPropertyName("districts")]
        public District[] Districts { get; init; } = [];
        [JsonPropertyName("cities")]
        public string[] Cities { get; init; } = [];
    }

    public class AccountingCompany
    {
        [JsonPropertyName("accountingCompanyId")]
        public string AccountingCompanyId { get; init; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
    }

    public class EmploymentLevel
    {
        [JsonPropertyName("employmentLevelId")]
        public string EmploymentLevelId { get; init; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
    }

    public class JobType
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
    }

    public class Province
    {
        [JsonPropertyName("provinceId")]
        public string ProvinceId { get; init; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
    }

    public class District
    {
        [JsonPropertyName("districtId")]
        public string DistrictId { get; init; } = string.Empty;
        [JsonPropertyName("name")]
        public string Name { get; init; } = string.Empty;
    }
}
