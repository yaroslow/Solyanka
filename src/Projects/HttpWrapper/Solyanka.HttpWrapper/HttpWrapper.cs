using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Solyanka.HttpWrapper.Abstractions;
using Solyanka.HttpWrapper.PolicyFactories;

namespace Solyanka.HttpWrapper
{
    /// <summary>
    /// Wrapper over <see cref="HttpClient"/>
    /// </summary>
    public static class HttpWrapper
    {
        /// <summary>
        /// Send request with policies
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="retryPolicyFactory"><see cref="IRetryPolicyFactory"/></param>
        /// <param name="timeoutPolicy"><see cref="IHttpRequestTimeoutPolicy"/></param>
        /// <param name="exceptionPolicyFactory"><see cref="IExceptionPolicyFactory"/></param>
        /// <param name="cachePolicyFactory"><see cref="ICachePolicyFactory"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <typeparam name="TResponse">Request response type</typeparam>
        /// <returns>Response of request</returns>
        public static async Task<TResponse> SendRequest<TResponse>(
            Func<CancellationToken, Task<TResponse>> request, IRetryPolicyFactory retryPolicyFactory = null, 
            IHttpRequestTimeoutPolicy timeoutPolicy = null, IExceptionPolicyFactory exceptionPolicyFactory = null, 
            ICachePolicyFactory cachePolicyFactory = null, CancellationToken cancellationToken = default)
        {
            var builder = new HttpRequestBuilder();

            if (retryPolicyFactory != null)
            {
                builder.AddRetryPolicy(retryPolicyFactory);
            }
            if (timeoutPolicy != null)
            {
                builder.AddTimeoutPolicy(timeoutPolicy);
            }
            if (exceptionPolicyFactory != null)
            {
                builder.AddExceptionPolicy(exceptionPolicyFactory);
            }
            if (cachePolicyFactory != null)
            {
                builder.AddCachePolicy<TResponse>(cachePolicyFactory);
            }

            return await builder.SendRequest(request, cancellationToken);
        }

        /// <summary>
        /// Send request with default policies (except cache)
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <typeparam name="TResponse">Request response type</typeparam>
        /// <returns>Response of request</returns>
        public static async Task<TResponse> SendRequest<TResponse>(
            Func<CancellationToken, Task<TResponse>> request,
            CancellationToken cancellationToken = default)
        {
            return await new HttpRequestBuilder()
                .AddRetryPolicy(DefaultRetryPolicyFactory.Default)
                .AddTimeoutPolicy(DefaultHttpRequestTimeoutPolicy.Default)
                .AddExceptionPolicy(DefaultExceptionPolicyFactory.Default)
                .SendRequest(request, cancellationToken);
        }

        /// <summary>
        /// Send request with default policies (include cache)
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="cache"><see cref="IDistributedCache"/></param>
        /// <param name="key">Cache key</param>
        /// <param name="timeToLive">Time to live in cache</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <typeparam name="TResponse">Request response type</typeparam>
        /// <returns>Response of request</returns>
        public static async Task<TResponse> SendRequestAndCache<TResponse>(
            Func<CancellationToken, Task<TResponse>> request,
            IDistributedCache cache, string key, TimeSpan? timeToLive = null,
            CancellationToken cancellationToken = default)
        {
            return await new HttpRequestBuilder()
                .AddRetryPolicy(DefaultRetryPolicyFactory.Default)
                .AddTimeoutPolicy(DefaultHttpRequestTimeoutPolicy.Default)
                .AddCachePolicy<TResponse>(new DefaultCachePolicyFactory(cache, key, timeToLive))
                .AddExceptionPolicy(DefaultExceptionPolicyFactory.Default)
                .SendRequest(request, cancellationToken);
        }
    }
}