using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobApplicant
    {
        [JsonPropertyName("titleCode")]
        public string? TitleCode { get; set; }

        [JsonPropertyName("firstName")]
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [JsonPropertyName("lastName")]
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [JsonPropertyName("gender")]
        [Required]
        public string Gender { get; set; } = string.Empty;
        
        [JsonPropertyName("nationality")]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string Nationality { get; set; } = string.Empty;
        
        [JsonPropertyName("telephoneNumber")]
        [Required]
        [Phone]
        public string TelephoneNumber { get; set; } = string.Empty;
        
        [JsonPropertyName("email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [JsonPropertyName("birthDate")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        
        [JsonPropertyName("countryCode")]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string CountryCode { get; set; } = string.Empty;
        
        [JsonPropertyName("zip")]
        [Required]
        public string Zip { get; set; } = string.Empty;
        
        [JsonPropertyName("city")]
        [Required]
        public string City { get; set; } = string.Empty;
        
        [JsonPropertyName("street")]
        [Required]
        public string Street { get; set; } = string.Empty;
        
        [JsonPropertyName("workPermit")]
        public bool WorkPermit { get; set; }
        
        [JsonPropertyName("driversLicenseClasses")]
        public string? DriversLicenseClasses { get; set; }

        [JsonPropertyName("employedBefore")]
        public bool EmployedBefore { get; set; }
        
        [JsonPropertyName("militaryServiceFinished")]
        [Required]
        public bool MilitaryServiceFinished { get; set; }
    }
}
