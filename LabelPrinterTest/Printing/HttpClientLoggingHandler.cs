using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LabelPrinterTest.Printing {
    public class HttpClientLoggingHandler : DelegatingHandler {
        private readonly ILogger _logger;

        public HttpClientLoggingHandler(HttpMessageHandler innerHandler, ILoggerFactory loggerFactory)
            : base(innerHandler) {
            _logger = loggerFactory.CreateLogger<HttpClientLoggingHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            _logger.LogTrace("request headers: {headers}", request.Headers);
            if(request.Content != null) {
                _logger.LogTrace("request body: {body}", Convert.ToHexString(await request.Content.ReadAsByteArrayAsync()));
            }

            HttpResponseMessage resp = await base.SendAsync(request, cancellationToken);

            _logger.LogTrace("response headers: {headers}", resp.Headers);
            if(resp.Content != null) {
                _logger.LogTrace("response body: {body}", Convert.ToHexString(await resp.Content.ReadAsByteArrayAsync()));
            }

            return resp;
        }
    }
}
