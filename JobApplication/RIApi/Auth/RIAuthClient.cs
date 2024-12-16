using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JobApplication.RIApi.Auth
{
    public class RIAuthClient
    {
        private readonly HttpClient _httpClient;

        public RIAuthClient(HttpClient httpClient, IOptions<RIAuthOptions> options)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);

            var authString = $"{options.Value.ClientUser}:{options.Value.ClientSecret}";
            var base64EncodedAuthString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authString));
            _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {base64EncodedAuthString}");
        }

        public async Task<AuthenticationResponse?> GetAccessToken()
        {
            var content = new FormUrlEncodedContent(
            [
                new("grant_type", "client_credentials")
            ]);
            var response = await _httpClient.PostAsync("auth/token", content);
            response.EnsureSuccessStatusCode();
            using var authResponseContent = response.Content.ReadAsStream();
            return await JsonSerializer.DeserializeAsync<AuthenticationResponse>(authResponseContent);
        }
    }
}
