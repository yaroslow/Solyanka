using System;
using System.Collections.Generic;
using MassTransit;

namespace Solyanka.Connector.Bus
{
    /// <summary>
    /// Config for consumers of messages
    /// </summary>
    public class ConsumersConfiguration
    {
        /// <summary>
        /// Types of consumers
        /// </summary>
        public List<Type> ConsumerTypes { get; }

        /// <summary/>
        public ConsumersConfiguration()
        {
            ConsumerTypes = new List<Type>();
        }

        /// <summary>
        /// Add consumer of messages
        /// </summary>
        /// <typeparam name="TConsumer"><see cref="IConsumer"/></typeparam>
        /// <returns><see cref="ConsumersConfiguration"/></returns>
        public ConsumersConfiguration AddConsumer<TConsumer>() where TConsumer : IConsumer
        {
            ConsumerTypes.Add(typeof(TConsumer));
            return this;
        }
    }
}