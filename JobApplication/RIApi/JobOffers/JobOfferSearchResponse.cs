using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobOfferSearchResponse
    {
        [JsonPropertyName("numberOfHits")]
        public int NumberOfHits { get; init; }
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; init; }
        [JsonPropertyName("jobs")]
        public JobOffer[] Jobs { get; init; } = [];
    }
}
