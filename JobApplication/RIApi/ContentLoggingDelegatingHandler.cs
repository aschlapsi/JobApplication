using System.Diagnostics.Tracing;

namespace JobApplication.RIApi
{
    public record ContentLoggingOptions(bool EnableRequestContentLogging, bool EnableResponseContentLogging)
    {
        public static readonly ContentLoggingOptions None = new(false, false);
        public static readonly ContentLoggingOptions Request = new(true, false);
        public static readonly ContentLoggingOptions Response = new(false, true);
        public static readonly ContentLoggingOptions RequestAndResponse = new(true, true);

        public bool EnableContentLogging { get => EnableRequestContentLogging || EnableResponseContentLogging; }
    }

    public class ContentLoggingDelegatingHandler(ILogger<ContentLoggingDelegatingHandler> logger, ContentLoggingOptions options)
        : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (options.EnableRequestContentLogging)
                    await LogRequestContent(request);

                var response = await base.SendAsync(request, cancellationToken);

                if (options.EnableResponseContentLogging)
                    await LogResponseContent(response);

                return response;
            }
            catch (Exception e)
            {
                logger.LogError(e, "HTTP request failed");
                throw;
            }
        }

        private async Task LogRequestContent(HttpRequestMessage request)
        {
            var content = request.Content;
            if (content is not null)
                logger.LogInformation("Request content: {requestContent}", await content.ReadAsStringAsync());
        }

        private async Task LogResponseContent(HttpResponseMessage? response)
        {
            if (response is null)
                return;

            var content = response.Content;
            if (content is not null)
                logger.LogInformation("Response content: {responseContent}", await content.ReadAsStringAsync());
        }
    }
}
