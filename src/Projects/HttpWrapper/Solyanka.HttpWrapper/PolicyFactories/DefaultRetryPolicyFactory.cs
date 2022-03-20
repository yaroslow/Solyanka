using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Retry;
using Polly.Timeout;
using Solyanka.HttpWrapper.Abstractions;

namespace Solyanka.HttpWrapper.PolicyFactories
{
    /// <inheritdoc />
    public class DefaultRetryPolicyFactory : IRetryPolicyFactory
    {
        /// <summary>
        /// Default periods of retrying if not specified
        /// </summary>
        public static IEnumerable<TimeSpan> DefaultPeriods => new[]
        {
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(4),
            TimeSpan.FromSeconds(8)
        };

        /// <summary>
        /// Default callback on exception if not specified
        /// </summary>
        public static Action<Exception, TimeSpan, int, Context> DefaultCallback => (_, _, _, _) => { }; 

        
        /// <summary>
        /// Periods of retrying
        /// </summary>
        public IEnumerable<TimeSpan> Periods { get; }
        
        /// <summary>
        /// Callback on exception
        /// </summary>
        public Action<Exception, TimeSpan, int, Context> Callback { get; }


        /// <summary>
        /// Constructor of <see cref="DefaultRetryPolicyFactory"/>
        /// </summary>
        /// <param name="periods">Periods of retrying</param>
        /// <param name="callback">Callback on exception</param>
        public DefaultRetryPolicyFactory(IEnumerable<TimeSpan> periods = null,
            Action<Exception, TimeSpan, int, Context> callback = null)
        {
            Periods = periods ?? DefaultPeriods;
            Callback = callback ?? DefaultCallback;
        }

        
        /// <inheritdoc />
        public AsyncRetryPolicy GetRetryPolicy()
        {
            return Policy
                .Handle<WebException>()
                .Or<HttpRequestException>()
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(Periods, (exception, timeSpan, retryNumber, context) =>
                {
                    Callback(exception, timeSpan, retryNumber, context);
                });
        }


        /// <summary>
        /// Default <see cref="DefaultRetryPolicyFactory"/>
        /// </summary>
        public static DefaultRetryPolicyFactory Default => new();
    }
}