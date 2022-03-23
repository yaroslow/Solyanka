using System;
using Solyanka.Ddd.Interfaces;

namespace Solyanka.Ddd.Entity
{
    /// <summary>
    /// Entity
    /// </summary>
    /// <typeparam name="TId">Identificator type</typeparam>
    public abstract class EntityBase<TId> : IHasId<TId> where TId: IEquatable<TId>
    {
        /// <summary>
        /// Protected default constructor
        /// </summary>
        protected EntityBase() {}

        /// <summary>
        /// Constructor of <see cref="EntityBase{TId}"/>
        /// </summary>
        /// <param name="id">Identificator</param>
        public EntityBase(TId id)
        {
            Id = id;
        }


        object IHasId.Id => Id;

        /// <inheritdoc />
        public TId Id { get; protected set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as EntityBase<TId>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id?.Equals(default) != false || other.Id?.Equals(default) != false)
                return false;

            return Id.Equals(other.Id);
        }

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="left">Left operand</param>
        /// <param name="right">Right operand</param>
        /// <returns>Left == right</returns>
        public static bool operator ==(EntityBase<TId> left, EntityBase<TId> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="a">Left operand</param>
        /// <param name="b">Right operand</param>
        /// <returns>Left != right</returns>
        public static bool operator !=(EntityBase<TId> a, EntityBase<TId> b)
        {
            return !(a == b);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}