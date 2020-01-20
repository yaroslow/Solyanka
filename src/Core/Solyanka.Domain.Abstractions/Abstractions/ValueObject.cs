using System;
using Solyanka.Domain.Abstractions.Abstractions.MarkerInterfaces;

namespace Solyanka.Domain.Abstractions.Abstractions
{
    /// <summary>
    /// Value object representing data container
    /// </summary>
    public abstract class ValueObject : IDomainUnit, IEquatable<ValueObject>
    {
        /// <inheritdoc />
        public abstract bool Equals(ValueObject other);

        /// <inheritdoc />
        public abstract override bool Equals(object obj);

        /// <inheritdoc />
        public abstract override int GetHashCode();

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="left">Left operand of type <see cref="ValueObject"/></param>
        /// <param name="right">Right operand of type <see cref="ValueObject"/></param>
        /// <returns>Equality</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="left">Left operand of type <see cref="ValueObject"/></param>
        /// <param name="right">Right operand of type <see cref="ValueObject"/></param>
        /// <returns>Inequality</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !Equals(left, right);
        }
    }
}