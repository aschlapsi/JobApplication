using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Model
{
    public class JobGroup
    {
        [JsonPropertyName("jobGroupId")]
        public required int JobGroupId { get; init; }
        [JsonPropertyName("name")]
        public required string Name { get; init; }
        [JsonPropertyName("subGroups")]
        public JobGroup[] SubGroups { get; init; } = Array.Empty<JobGroup>();
    }
}
