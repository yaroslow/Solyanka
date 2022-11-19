namespace Solyanka.ServiceBus.RabbitMq.Microsoft.DependencyInjection;

/// <summary>
/// Configuration of RabbitMQ endpoint connection
/// </summary>
public class RabbitMqEndpointSettings
{
    /// <summary>
    /// Host
    /// </summary>
    public string? Host { get; set; }
        
    /// <summary>
    /// Virtual host
    /// </summary>
    public string? VirtualHost { get; set; }
        
    /// <summary>
    /// Username/login
    /// </summary>
    public string? Username { get; set; }
        
    /// <summary>
    /// Password
    /// </summary>
    public string? Password { get; set; }
        
    /// <summary>
    /// Service endpoint name
    /// </summary>
    public string? ServiceEndpointName { get; set; }

    /// <summary>
    /// Maximum number of concurrent messages that are consumed
    /// </summary>
    public ushort PrefetchCount { get; set; } = 8;

    /// <summary>
    /// Count of retrying
    /// </summary>
    public int RetryCount { get; set; } = 2;

    /// <summary>
    /// Retrying interval
    /// </summary>
    public int RetryInterval { get; set; } = 100;
}