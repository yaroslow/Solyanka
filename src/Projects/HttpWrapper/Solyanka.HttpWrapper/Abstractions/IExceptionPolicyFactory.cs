using System;
using System.Threading.Tasks;

namespace Solyanka.HttpWrapper.Abstractions
{
    /// <summary>
    /// Exception policy factory
    /// </summary>
    public interface IExceptionPolicyFactory
    {
        /// <summary>
        /// Get exception policy
        /// </summary>
        /// <returns>Func that handles exception</returns>
        public Func<Exception, Task> GetExceptionPolicy();
    }
}