using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Polly;
using Solyanka.HttpWrapper.Abstractions;

namespace Solyanka.HttpWrapper
{
    /// <summary>
    /// Constructor of http request
    /// </summary>
    public class HttpRequestBuilder
    {
        internal List<IAsyncPolicy> Policies { get; }
        internal Func<Exception, Task> OnException { get; set; }
        internal Func<object, Task> PutInCache { get; set; }
        internal Func<Task<object>> GetFromCache { get; set; }


        /// <summary>
        /// Constructor of <see cref="HttpRequestBuilder"/>
        /// </summary>
        public HttpRequestBuilder()
        {
            Policies = new List<IAsyncPolicy>();
            OnException = null;
            PutInCache = null;
            GetFromCache = null;
        }
        
        
        /// <summary>
        /// Add timeout policy to request pipeline
        /// </summary>
        /// <param name="policy"><see cref="IHttpRequestTimeoutPolicy"/></param>
        /// <returns><see cref="HttpRequestBuilder"/></returns>
        public HttpRequestBuilder AddTimeoutPolicy(IHttpRequestTimeoutPolicy policy)
        {
            Policies.Add(policy.GetTimeoutPolicy());

            return this;
        }
        
        /// <summary>
        /// Add retry policy to request pipeline
        /// </summary>
        /// <param name="policyFactory"><see cref="IRetryPolicyFactory"/></param>
        /// <returns><see cref="HttpRequestBuilder"/></returns>
        public HttpRequestBuilder AddRetryPolicy(IRetryPolicyFactory policyFactory)
        {
            Policies.Add(policyFactory.GetRetryPolicy());

            return this;
        }
        
        /// <summary>
        /// Add cache policy to request pipeline
        /// </summary>
        /// <param name="policyFactory"><see cref="ICachePolicyFactory"/></param>
        /// <typeparam name="TResponse">Type of request response</typeparam>
        /// <returns><see cref="HttpRequestBuilder"/></returns>
        public HttpRequestBuilder AddCachePolicy<TResponse>(ICachePolicyFactory policyFactory)
        {
            GetFromCache = policyFactory.GetCachePolicyPop<TResponse>();
            PutInCache = policyFactory.GetCachePolicyPush<TResponse>();

            return this;
        }
        
        /// <summary>
        /// Add exception policy to request pipeline
        /// </summary>
        /// <param name="policyFactory"><see cref="IExceptionPolicyFactory"/></param>
        /// <returns><see cref="HttpRequestBuilder"/></returns>
        public HttpRequestBuilder AddExceptionPolicy(IExceptionPolicyFactory policyFactory)
        {
            OnException = policyFactory.GetExceptionPolicy();

            return this;
        }
        
        
        /// <summary>
        /// Send request with all policies
        /// </summary>
        /// <param name="request">Request of <see cref="HttpClient"/></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <returns>Return value of <see cref="HttpClient"/> response</returns>
        public async Task<TResponse> SendRequest<TResponse>(Func<CancellationToken, Task<TResponse>> request, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (GetFromCache != null)
                {
                    var entry = (TResponse)await GetFromCache();
                    if (entry != null)
                        return entry;
                }

                if (Policies.Count == 0) return await request(cancellationToken);

                var policyWrap = Policy.WrapAsync(Policies.ToArray());
                var result = await policyWrap.ExecuteAsync(request, cancellationToken);

                if (PutInCache != null)
                {
                    await PutInCache(result);
                }

                return result;
            }
            catch (Exception e)
            {
                if (OnException == null)
                    throw;
                await OnException(e);
            }

            return default;
        }
    }
}