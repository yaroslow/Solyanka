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
    /// Durable
    /// (Default is true)
    /// </summary>
    public bool Durable { get; set; } = true;

    /// <summary>
    /// Maximum number of concurrent messages that are consumed
    /// (Default is 8)
    /// </summary>
    public ushort PrefetchCount { get; set; } = 8;

    /// <summary>
    /// Count of retrying
    /// (Default is 2)
    /// </summary>
    public int RetryCount { get; set; } = 2;

    /// <summary>
    /// Retrying interval
    /// (Default is 100)
    /// </summary>
    public int RetryInterval { get; set; } = 100;


    internal void Validate()
    {
        if (string.IsNullOrEmpty(Host)) throw new ArgumentNullException(nameof(Host));
        if (string.IsNullOrEmpty(VirtualHost)) throw new ArgumentNullException(nameof(VirtualHost));
        if (string.IsNullOrEmpty(Username)) throw new ArgumentNullException(nameof(Username));
        if (string.IsNullOrEmpty(ServiceEndpointName)) throw new ArgumentNullException(nameof(ServiceEndpointName));
    }
}