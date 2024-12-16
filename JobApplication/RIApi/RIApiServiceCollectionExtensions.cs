using JobApplication.RIApi.Auth;
using JobApplication.RIApi.JobApplications;
using JobApplication.RIApi.Model;

namespace JobApplication.RIApi
{
    public static class RIApiServiceCollectionExtensions
    {
        public static void AddRIApiClient(
            this IServiceCollection services,
            IConfiguration configuration,
            ContentLoggingOptions? contentLoggingOptions = null)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            contentLoggingOptions ??= ContentLoggingOptions.None;

            if (contentLoggingOptions.EnableContentLogging)
            {
                services.AddSingleton(contentLoggingOptions);
                services.AddTransient<ContentLoggingDelegatingHandler>();
            }

            services.Configure<RIAuthOptions>(configuration.GetSection(RIAuthOptions.RIAuthConfig));
            services.Configure<RIApiOptions>(configuration.GetSection(RIApiOptions.RIApiConfig));
            services.AddSingleton<TokenStore>();
            services.AddTransient<AuthenticationDelegatingHandler>();
            services.AddHttpClient<RIAuthClient>();
            services.AddHttpClient<RIJobOfferClient>()
                .ConfigureHttpClient(contentLoggingOptions);
            services.AddHttpClient<RIJobApplicationsClient>()
                .ConfigureHttpClient(contentLoggingOptions);
        }

        private static void ConfigureHttpClient(
            this IHttpClientBuilder httpClientBuilder,
            ContentLoggingOptions contentLoggingOptions)
        {
            httpClientBuilder.AddHttpMessageHandler<AuthenticationDelegatingHandler>();
            if (contentLoggingOptions.EnableContentLogging)
                httpClientBuilder.AddHttpMessageHandler<ContentLoggingDelegatingHandler>();
        }
    }
}
