using System;
using System.Runtime.Serialization;
using System.Text;

namespace Solyanka.Exceptions.Exceptions
{
    /// <summary>
    /// Conrollable exception
    /// </summary>
    [Serializable]
    public class ControllableException : Exception
    {
        /// <summary>
        /// Exception identifier
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Exception code
        /// </summary>
        public string Code { get; protected set; }
        
        /// <summary>
        /// Exception subcode
        /// </summary>
        public string SubCode { get; protected set; }



        /// <inheritdoc />
        protected ControllableException()
        {
            Id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public ControllableException(string message) : base(message)
        {
            Id = Guid.NewGuid();
        }

        /// <inheritdoc />
        public ControllableException(string message, Exception innerException) : base(message, innerException)
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Constructor of <see cref="ControllableException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="code">Exception code of error</param>
        /// <param name="subCode">Exception subcode of error</param>
        public ControllableException(string message, string code, string subCode) : this(message)
        {
            Code = code;
            SubCode = subCode;
        }

        /// <summary>
        /// Constructor of <see cref="ControllableException"/>
        /// </summary>
        /// <param name="message">The message that describes the error</param>
        /// <param name="innerException">The exception that is the cause of the current exception</param>
        /// <param name="code">Exception code of error</param>
        /// <param name="subCode">Exception subcode of error</param>
        public ControllableException(string message, Exception innerException, string code = null, string subCode = null) 
            : this(message, innerException)
        {
            Code = code;
            SubCode = subCode;
        }

        
        /// <inheritdoc />
        protected ControllableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Id = Guid.NewGuid();
        }


        /// <inheritdoc />
        public override string ToString()
        {
            var stringBuilder = new StringBuilder($"Id: {Id}{Environment.NewLine}");
            if (string.IsNullOrEmpty(Code))
            {
                stringBuilder.Append($"Code: {Code}{Environment.NewLine}");
            }
            if (string.IsNullOrEmpty(SubCode))
            {
                stringBuilder.Append($"SubCode: {SubCode}{Environment.NewLine}");
            }
            stringBuilder.Append(base.ToString());

            return stringBuilder.ToString();
        }
    }
}