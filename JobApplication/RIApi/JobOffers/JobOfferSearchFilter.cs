using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public record JobOfferSearchFilter(string searchTerm)
    {
        [JsonPropertyName("cityList")]
        public List<string> CityList { get; } = [];
    }
}
