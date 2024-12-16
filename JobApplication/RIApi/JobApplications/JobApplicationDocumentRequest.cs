using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class JobApplicationDocumentRequest
    {
        [JsonPropertyName("documentType")]
        public string DocumentType { get; set; } = string.Empty;
        [JsonPropertyName("documentName")]
        public string DocumentName { get; set; } = string.Empty;
        [JsonPropertyName("documentBlob")]
        public string DocumentBlob { get; set; } = string.Empty;
    }

}
