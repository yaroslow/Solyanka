using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solyanka.Keeper.Entity
{
    /// <summary>
    /// Base enums type
    /// </summary>
    public abstract class Enumeration : Entity<int>, IComparable
    {
        /// <summary>
        /// Enumeration name
        /// </summary>
        public string Name { get; private set; }


        private Enumeration() {}
        
        /// <summary/>
        protected Enumeration(string name, int id) : base(id)
        {
            Name = name;
        }

        
        /// <summary>
        /// Get all enumerations of type <see cref="T"/>
        /// They should be public static readonly properties
        /// </summary>
        /// <typeparam name="T">Enumeration type</typeparam>
        /// <returns>List of enumerations of type <see cref="T"/></returns>
        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | 
                                             BindingFlags.Static | 
                                             BindingFlags.DeclaredOnly); 

            return fields.Select(a => a.GetValue(null)).Cast<T>();
        }
        
        /// <summary>
        /// Match value with <see cref="Enumeration"/> 
        /// </summary>
        /// <param name="value">Id</param>
        /// <typeparam name="T">Type of <see cref="Enumeration"/></typeparam>
        /// <returns><see cref="Enumeration"/></returns>
        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Match<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        /// <summary>
        /// Match name with <see cref="Enumeration"/>
        /// </summary>
        /// <param name="displayName"><see cref="Enumeration"/> name</param>
        /// <typeparam name="T">Type of <see cref="Enumeration"/></typeparam>
        /// <returns><see cref="Enumeration"/></returns>
        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Match<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static TEnumeration Match<TEnumeration, TValue>(TValue value, string description, Func<TEnumeration, bool> predicate) where TEnumeration : Enumeration
        {
            return GetAll<TEnumeration>().FirstOrDefault(predicate) ??
                throw new InvalidOperationException($"'{value}' is not a valid {description} of property in {typeof(TEnumeration)}");
        }
        

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Enumeration) obj);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"><see cref="Enumeration"/></param>
        /// <returns><see cref="bool"/></returns>
        protected bool Equals(Enumeration other)
        {
            return base.Equals(other) && string.Equals(Name, other.Name);
        }

        /// <inheritdoc />
        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        /// <summary>
        /// Operator ==
        /// </summary>
        /// <param name="left">Left <see cref="Enumeration"/>> operand</param>
        /// <param name="right">Right <see cref="Enumeration"/>> operand</param>
        /// <returns><see cref="bool"/></returns>
        public static bool operator ==(Enumeration left, Enumeration right)
        {
            return !(left == null) && left.Equals(right);
        }

        /// <summary>
        /// Operator !=
        /// </summary>
        /// <param name="left">Left <see cref="Enumeration"/>> operand</param>
        /// <param name="right">Right <see cref="Enumeration"/>> operand</param>
        /// <returns><see cref="bool"/></returns>
        public static bool operator !=(Enumeration left, Enumeration right)
        {
            return !(left == right);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
        
        /// <inheritdoc />
        public override string ToString() => Name;
    }
}