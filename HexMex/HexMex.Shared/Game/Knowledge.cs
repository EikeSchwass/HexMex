namespace HexMex.Game
{
    public struct Knowledge
    {
        public static Knowledge Zero { get; } = new Knowledge();
        public static Knowledge Knowledge1One { get; } = new Knowledge(1, 0, 0);
        public static Knowledge Knowledge2One { get; } = new Knowledge(0, 1, 0);
        public static Knowledge Knowledge3One { get; } = new Knowledge(0, 0, 1);

        public Knowledge(int knowledge1, int knowledge2, int knowledge3)
        {
            Knowledge3 = knowledge3;
            Knowledge2 = knowledge2;
            Knowledge1 = knowledge1;
        }

        public int Knowledge1 { get; }
        public int Knowledge2 { get; }
        public int Knowledge3 { get; }

        public static Knowledge operator +(Knowledge k1, Knowledge k2) => new Knowledge(k1.Knowledge1 + k2.Knowledge1, k1.Knowledge2 + k2.Knowledge2, k1.Knowledge3 + k2.Knowledge3);
        public static Knowledge operator -(Knowledge k1, Knowledge k2) => new Knowledge(k1.Knowledge1 - k2.Knowledge1, k1.Knowledge2 - k2.Knowledge2, k1.Knowledge3 - k2.Knowledge3);
        public static bool operator >(Knowledge k1, Knowledge k2) => k1.Knowledge1 > k2.Knowledge1 && k1.Knowledge2 > k2.Knowledge2 && k1.Knowledge3 > k2.Knowledge3;
        public static bool operator >=(Knowledge k1, Knowledge k2) => k1.Knowledge1 >= k2.Knowledge1 && k1.Knowledge2 >= k2.Knowledge2 && k1.Knowledge3 >= k2.Knowledge3;
        public static bool operator <(Knowledge k1, Knowledge k2) => k1.Knowledge1 < k2.Knowledge1 && k1.Knowledge2 < k2.Knowledge2 && k1.Knowledge3 < k2.Knowledge3;
        public static bool operator <=(Knowledge k1, Knowledge k2) => k1.Knowledge1 <= k2.Knowledge1 && k1.Knowledge2 <= k2.Knowledge2 && k1.Knowledge3 <= k2.Knowledge3;

        public bool Equals(Knowledge other)
        {
            return Knowledge1 == other.Knowledge1 && Knowledge2 == other.Knowledge2 && Knowledge3 == other.Knowledge3;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Knowledge && Equals((Knowledge)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Knowledge1;
                hashCode = (hashCode * 397) ^ Knowledge2;
                hashCode = (hashCode * 397) ^ Knowledge3;
                return hashCode;
            }
        }
        public static bool operator ==(Knowledge left, Knowledge right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(Knowledge left, Knowledge right)
        {
            return !left.Equals(right);
        }
    }
}