using System.Net.Http.Headers;

namespace JobApplication.RIApi.Auth
{
    public class AuthenticationDelegatingHandler(TokenStore tokenStore, RIAuthClient authClient)
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            await EnsureValidAuthenticationToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);
            return await base.SendAsync(request, cancellationToken);
        }

        private async Task EnsureValidAuthenticationToken()
        {
            if (tokenStore.IsValid)
                return;

            var authenticationResponse = await authClient.GetAccessToken();
            tokenStore.SetToken(authenticationResponse!);
        }
    }
}
