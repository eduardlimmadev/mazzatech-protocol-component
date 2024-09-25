using Polly.Retry;

namespace Protocol.Publisher.Infrastructure.Polly.Interfaces
{
    public interface IHttpClientPolicy
    {
        AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; set; }
    }
}
