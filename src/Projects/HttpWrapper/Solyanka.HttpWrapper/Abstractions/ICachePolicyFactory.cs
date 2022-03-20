using System;
using System.Threading.Tasks;

namespace Solyanka.HttpWrapper.Abstractions
{
    /// <summary>
    /// Cache policy factory
    /// </summary>
    public interface ICachePolicyFactory
    {
        /// <summary>
        /// Get cache policy func to pop objects
        /// </summary>
        /// <typeparam name="TResponse">Type of response object</typeparam>
        /// <returns>Func to pop objects</returns>
        public Func<Task<object>> GetCachePolicyPop<TResponse>();

        /// <summary>
        /// Get cache policy func to push objects
        /// </summary>
        /// <typeparam name="TResponse">Type of response object</typeparam>
        /// <returns>Func to push objects</returns>
        public Func<object, Task> GetCachePolicyPush<TResponse>();
    }
}