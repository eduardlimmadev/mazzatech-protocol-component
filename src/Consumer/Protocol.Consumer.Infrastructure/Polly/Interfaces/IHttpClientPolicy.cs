using Polly.Retry;

namespace Protocol.Consumer.Infrastructure.Polly.Interfaces
{
    public interface IHttpClientPolicy
    {
        AsyncRetryPolicy<HttpResponseMessage> LinearHttpRetry { get; set; }
    }
}
