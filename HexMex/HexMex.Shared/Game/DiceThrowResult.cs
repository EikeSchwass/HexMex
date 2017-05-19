using System.Collections.ObjectModel;
using System.Linq;

namespace HexMex.Game
{
    public struct DiceThrowResult
    {
        public static DiceThrowResult None { get; } = new DiceThrowResult();
        public ReadOnlyCollection<int> DieThrows { get; }
        public int Sum { get; }

        public DiceThrowResult(params int[] dice)
        {
            DieThrows = new ReadOnlyCollection<int>(dice ?? Enumerable.Empty<int>().ToArray());
            Sum = DieThrows.Sum();
        }

        public bool Equals(DiceThrowResult other)
        {
            if (Sum != other.Sum)
                return false;
            if (DieThrows == null)
            {
                return other.DieThrows == null;
            }
            return other.DieThrows != null && DieThrows.SequenceEqual(other.DieThrows);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DiceThrowResult && Equals((DiceThrowResult)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DieThrows != null ? DieThrows.GetHashCode() : 0) * 397) ^ Sum;
            }
        }

        public static bool operator ==(DiceThrowResult left, DiceThrowResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(DiceThrowResult left, DiceThrowResult right)
        {
            return !left.Equals(right);
        }
    }
}