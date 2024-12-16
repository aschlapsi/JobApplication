namespace JobApplication.RIApi.Auth
{
    public class TokenStore
    {
        private AuthenticationResponse? _authenticationResponse;
        private DateTime? _expires;

        public void SetToken(AuthenticationResponse authenticationResponse)
        {
            _authenticationResponse = authenticationResponse;
            _expires = DateTime.UtcNow + TimeSpan.FromSeconds(authenticationResponse.AccessTokenExpiresIn - 300);
        }

        public bool HasToken { get => _authenticationResponse != null; }
        public bool IsExpired { get => _expires == null || DateTime.UtcNow >= _expires; }
        public bool IsValid {  get => HasToken && !IsExpired; }

        public string AccessToken
        {
            get
            {
                if (!IsValid)
                {
                    throw new InvalidOperationException("Token has not been set or is expired");
                }

                return _authenticationResponse!.AccessToken;
            }
        }
    }
}
