using Polly;
using Polly.Retry;
using Protocol.Consumer.Infrastructure.Polly.Interfaces;
using System.Net;

namespace Protocol.Consumer.Infrastructure.Polly
{
    public class HttpClientPolicy : IHttpClientPolicy
    {
        private readonly int NUM_RETRY = 5;
        private readonly List<HttpStatusCode> STATUS_CODE_RETRY = new ()
        {
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound,
            HttpStatusCode.FailedDependency,
            HttpStatusCode.BadGateway,
            HttpStatusCode.ServiceUnavailable,
            HttpStatusCode.Forbidden,
            HttpStatusCode.GatewayTimeout,
            HttpStatusCode.InternalServerError,
            HttpStatusCode.RequestTimeout
        };

        public HttpClientPolicy()
        {
            LinearHttpRetry = Policy.HandleResult<HttpResponseMessage>(
                res => STATUS_CODE_RETRY.Contains(res.StatusCode))
                .WaitAndRetryAsync(NUM_RETRY, retryAttemp => TimeSpan.FromSeconds(Math.Pow(1, retryAttemp)));
        }

        public AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; set; }
    }
}
