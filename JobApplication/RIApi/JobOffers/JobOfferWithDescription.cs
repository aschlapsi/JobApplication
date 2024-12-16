using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobOfferWithDescription
    {
        [JsonPropertyName("job")]
        public JobOffer Job { get; init; } = new JobOffer();
        [JsonPropertyName("description")]
        public JobDescription Description { get; init; } = new JobDescription();
    }
}
