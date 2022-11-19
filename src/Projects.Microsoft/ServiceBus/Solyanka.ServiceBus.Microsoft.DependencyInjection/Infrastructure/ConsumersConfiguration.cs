using MassTransit;

namespace Solyanka.ServiceBus.Microsoft.DependencyInjection.Infrastructure;

/// <summary>
/// Configuration of consumer subscribing
/// </summary>
public class ConsumersConfiguration
{
    /// <summary>
    /// Types that implement <see cref="IConsumer"/>
    /// </summary>
    public List<Type> ConsumerTypes { get; }

        
    /// <summary/>
    public ConsumersConfiguration()
    {
        ConsumerTypes = new List<Type>();
    }

        
    /// <summary>
    /// Subscribe consumer
    /// </summary>
    /// <typeparam name="TConsumer">Consumer implementing <see cref="IConsumer"/></typeparam>
    /// <returns><see cref="ConsumersConfiguration"/></returns>
    public ConsumersConfiguration SubscribeConsumer<TConsumer>() where TConsumer : IConsumer
    {
        ConsumerTypes.Add(typeof(TConsumer));
        return this;
    }

    /// <summary>
    /// Subscribe consumer
    /// </summary>
    /// <param name="consumerType">Consumer type that implements <see cref="IConsumer"/></param>
    /// <returns><see cref="ConsumersConfiguration"/></returns>
    /// <exception cref="ArgumentException">Exception throwing if consumer type does not implement <see cref="IConsumer"/></exception>
    public ConsumersConfiguration SubscribeConsumer(Type consumerType)
    {
        if (!consumerType.IsAssignableFrom(typeof(IConsumer)))
            throw new ArgumentException($"{nameof(consumerType)} does not implement {nameof(IConsumer)}");
            
        ConsumerTypes.Add(consumerType);
        return this;
    }
}