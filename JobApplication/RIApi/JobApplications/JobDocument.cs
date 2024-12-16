using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobDocument
    {
        [JsonPropertyName("documentId")]
        public int DocumentId { get; set; }
        [JsonPropertyName("documentType")]
        public string DocumentType { get; set; } = string.Empty;
        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; } = string.Empty;
    }
}
