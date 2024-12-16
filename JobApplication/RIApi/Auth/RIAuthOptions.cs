namespace JobApplication.RIApi.Auth
{
    public class RIAuthOptions
    {
        public const string RIAuthConfig = "RIAuthConfig";

        public string BaseUrl { get; set; } = string.Empty;
        public string ClientUser { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}