namespace HexMex.Game
{
    public struct VerbalStructureDescription
    {
        public string Name { get; }
        public string Description { get; }

        public VerbalStructureDescription(string name, string descritpion)
        {
            Description = descritpion;
            Name = name;
        }

        public bool Equals(VerbalStructureDescription other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is VerbalStructureDescription && Equals((VerbalStructureDescription)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ (Description != null ? Description.GetHashCode() : 0);
            }
        }
        public static bool operator ==(VerbalStructureDescription left, VerbalStructureDescription right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(VerbalStructureDescription left, VerbalStructureDescription right)
        {
            return !left.Equals(right);
        }
    }
}