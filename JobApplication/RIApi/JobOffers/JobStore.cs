using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobStore
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        [JsonPropertyName("street")]
        public required string Street { get; set; }
        [JsonPropertyName("amountOfJobs")]
        public required int AmountOfJobs { get; set; }
    }
}
