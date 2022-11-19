using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Solyanka.HttpWrapper.Abstractions;

namespace Solyanka.HttpWrapper.PolicyFactories;

/// <inheritdoc />
public class DefaultCachePolicyFactory : ICachePolicyFactory
{
    /// <summary>
    /// Default objects time to live in cache if not specified
    /// </summary>
    public static TimeSpan DefaultTimeToLive => TimeSpan.FromMinutes(10);

        
    /// <summary>
    /// <see cref="IDistributedCache"/>
    /// </summary>
    public IDistributedCache Cache { get; }
        
    /// <summary>
    /// Key
    /// </summary>
    public string Key { get; }
        
    /// <summary>
    /// Objects time to live in cache
    /// </summary>
    public TimeSpan TimeToLive { get; }


    /// <summary>
    /// Constructor of <see cref="DefaultCachePolicyFactory"/>
    /// </summary>
    /// <param name="cache"><see cref="IDistributedCache"/></param>
    /// <param name="key">Key</param>
    /// <param name="timeToLive">Objects time to live in cache</param>
    public DefaultCachePolicyFactory(IDistributedCache cache, string key, TimeSpan? timeToLive = null)
    {
        Cache = cache;
        Key = key;
        TimeToLive = timeToLive ?? DefaultTimeToLive;
    }

        
    /// <inheritdoc />
    public Func<Task<object?>> GetCachePolicyPop<TResponse>()
    {
        return async () =>
        {
            var bytes = await Cache.GetAsync(Key);
            var json = Encoding.Default.GetString(bytes);
            return JsonConvert.DeserializeObject<TResponse>(json);
        };
    }

    /// <inheritdoc />
    public Func<object?, Task> GetCachePolicyPush<TResponse>()
    {
        return async obj =>
        {
            var json = JsonConvert.SerializeObject(obj);
            var bytes = Encoding.Default.GetBytes(json);
            await Cache.SetAsync(Key, bytes, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeToLive
            });
        };
    }
}