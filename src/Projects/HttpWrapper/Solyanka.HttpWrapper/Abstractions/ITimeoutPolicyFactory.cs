using Polly.Timeout;

namespace Solyanka.HttpWrapper.Abstractions;

/// <summary>
/// Timeout policy factory
/// </summary>
public interface ITimeoutPolicyFactory
{
    /// <summary>
    /// Get timeout policy
    /// </summary>
    /// <returns><see cref="AsyncTimeoutPolicy"/></returns>
    public AsyncTimeoutPolicy GetTimeoutPolicy();
}