using System;
using System.Threading.Tasks;
using Polly;
using Polly.Timeout;
using Solyanka.HttpWrapper.Abstractions;

namespace Solyanka.HttpWrapper.PolicyFactories
{
    /// <inheritdoc />
    public class DefaultHttpRequestTimeoutPolicy : IHttpRequestTimeoutPolicy
    {
        /// <summary>
        /// Default timeout if not specified
        /// </summary>
        public static TimeSpan DefaultTimeout => TimeSpan.FromSeconds(20);
        
        
        /// <summary>
        /// Timeout
        /// </summary>
        public TimeSpan Timeout { get; }
        
        /// <summary>
        /// <see cref="TimeoutStrategy"/>
        /// </summary>
        public TimeoutStrategy Strategy { get; }
        
        /// <summary>
        /// Callback on timeout
        /// </summary>
        public Func<Context, TimeSpan, Task, Exception, Task> Callback { get; }


        /// <summary>
        /// Constructor of <see cref="DefaultHttpRequestTimeoutPolicy"/>
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <param name="strategy"><see cref="TimeoutStrategy"/></param>
        /// <param name="callback">Callback on timeout</param>
        public DefaultHttpRequestTimeoutPolicy(TimeSpan? timeout = null,
            TimeoutStrategy strategy = TimeoutStrategy.Pessimistic, 
            Func<Context, TimeSpan, Task, Exception, Task> callback = null)
        {
            Timeout = timeout ?? DefaultTimeout;
            Strategy = strategy;
            Callback = callback;
        }


        /// <inheritdoc />
        public AsyncTimeoutPolicy GetTimeoutPolicy()
        {
            return Callback != null ? 
                Policy.TimeoutAsync(Timeout, Strategy, Callback) : 
                Policy.TimeoutAsync(Timeout, Strategy);
        }


        /// <summary>
        /// Default <see cref="DefaultHttpRequestTimeoutPolicy"/>
        /// </summary>
        public static DefaultHttpRequestTimeoutPolicy Default => new();
    }
}