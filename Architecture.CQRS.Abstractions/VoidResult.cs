using System;

namespace Architecture.CQRS.Abstractions
{
    public struct VoidResult : IEquatable<VoidResult>, IComparable<VoidResult>, IComparable
    {
        public bool Equals(VoidResult other)
        {
            return true;
        }

        public int CompareTo(VoidResult other)
        {
            return 0;
        }

        public int CompareTo(object obj)
        {
            return 0;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override bool Equals(object obj)
        {
            return obj is VoidResult;
        }

        public static bool operator ==(VoidResult left, VoidResult right)
        {
            return true;
        }

        public static bool operator !=(VoidResult left, VoidResult right)
        {
            return false;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}
