using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class Link
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("method")]
        public required string Method { get; set; }
        [JsonPropertyName("href")]
        public required string Href { get; set; }
    }
}
