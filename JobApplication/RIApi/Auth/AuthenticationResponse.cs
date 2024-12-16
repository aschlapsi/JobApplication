using System.Text.Json.Serialization;

namespace JobApplication.RIApi.Auth
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; init; } = string.Empty;
        [JsonPropertyName("access_token_expires_in")]
        public int AccessTokenExpiresIn { get; init; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; init; } = string.Empty;
    }
}
