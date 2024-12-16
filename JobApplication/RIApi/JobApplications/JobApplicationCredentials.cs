using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobApplicationCredentials
    {
        [JsonPropertyName("id")]
        public int Id { get; init; }
        [JsonPropertyName("authCode")]
        public string AuthCode { get; init; } = string.Empty;
        [JsonPropertyName("expirationTimestamp")]
        public string ExpirationTimestamp { get; init; } = string.Empty;
    }
}
