using Polly.Retry;

namespace Solyanka.HttpWrapper.Abstractions;

/// <summary>
/// Retry policy factory
/// </summary>
public interface IRetryPolicyFactory
{
    /// <summary>
    /// Get retry policy
    /// </summary>
    /// <returns><see cref="AsyncRetryPolicy"/></returns>
    public AsyncRetryPolicy GetRetryPolicy();
}