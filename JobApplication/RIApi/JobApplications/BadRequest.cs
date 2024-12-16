using System.Text.Json.Serialization;

namespace JobApplication.RIApi.JobApplications
{
    public class BadRequest
    {
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("modelState")]
        public Dictionary<string, string[]> ModelState { get; set; } = [];
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }
        [JsonPropertyName("errorID")]
        public string ErrorID { get; set; } = string.Empty;
    }

    public class Response
    {
        public Response()
        {
            BadRequest = null;
        }

        public Response(BadRequest badRequest)
        {
            BadRequest = badRequest;
        }

        public BadRequest? BadRequest { get; set; } = default;
        public bool Succeeded { get => BadRequest is null; }
    }

    public class Response<T> : Response
    {
        public Response(T value)
            : base()
        {
            Value = value;
        }

        public Response(BadRequest badRequest)
            : base(badRequest)
        {
            Value = default;
        }

        public T? Value { get; init; } = default;
    }
}
