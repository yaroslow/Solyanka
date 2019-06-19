using System.ComponentModel.DataAnnotations;

namespace Solyanka.Connector.Bus.Microsoft.DependencyInjection.RabbitMQ
{
    /// <summary>
    /// Settings of bus endpoint
    /// </summary>
    public class EndpointOptions
    {
        /// <summary>
        /// Host
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Host { get; set; }
        
        /// <summary>
        /// Virtual host
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string VirtualHost { get; set; }
        
        /// <summary>
        /// Username (login)
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }
        
        /// <summary>
        /// Password
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        
        /// <summary>
        /// Endpoint of bus service
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string ServiceEndpoint { get; set; }

        
        /// <summary>
        /// Validate
        /// </summary>
        public void Validate()
        {
            var validationContext = new ValidationContext(this);
            Validator.ValidateObject(this, validationContext);
        }
    }
}