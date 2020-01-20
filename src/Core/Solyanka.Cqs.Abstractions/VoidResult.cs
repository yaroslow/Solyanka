using System;
using System.Threading.Tasks;

namespace Solyanka.Cqs.Abstractions
{
    /// <summary>
    /// Representation of type void
    /// </summary>
    public struct VoidResult : IEquatable<VoidResult>, IComparable<VoidResult>, IComparable
    {
        /// <summary>
        /// <see cref="VoidResult"/>
        /// </summary>
        public static readonly VoidResult Value = new VoidResult();

        /// <summary>
        /// <see cref="Task"/> из <see cref="VoidResult"/>
        /// </summary>
        public static readonly Task<VoidResult> TaskValue = Task.FromResult(Value);
        
        /// <inheritdoc />
        public bool Equals(VoidResult other)
        {
            return true;
        }

        /// <summary/>
        public int CompareTo(VoidResult other)
        {
            return 0;
        }

        /// <summary/>
        public int CompareTo(object obj)
        {
            return 0;
        }

        /// <summary/>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary/>
        public override bool Equals(object obj)
        {
            return obj is VoidResult;
        }

        /// <summary/>
        public static bool operator ==(VoidResult left, VoidResult right)
        {
            return true;
        }

        /// <summary/>
        public static bool operator !=(VoidResult left, VoidResult right)
        {
            return false;
        }

        /// <summary/>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
