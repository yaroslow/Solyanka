using Polly.Timeout;

namespace Solyanka.HttpWrapper.Abstractions
{
    /// <summary>
    /// Timeout policy
    /// </summary>
    public interface IHttpRequestTimeoutPolicy
    {
        /// <summary>
        /// Get timeout policy
        /// </summary>
        /// <returns><see cref="AsyncTimeoutPolicy"/></returns>
        public AsyncTimeoutPolicy GetTimeoutPolicy();
    }
}