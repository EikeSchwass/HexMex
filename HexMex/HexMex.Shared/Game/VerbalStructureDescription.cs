namespace HexMex.Game
{
    public struct VerbalStructureDescription
    {
        public TranslationKey NameID { get; }
        public TranslationKey DescriptionID { get; }
        public string InternalName { get; }

        public VerbalStructureDescription(string internalName, TranslationKey name, TranslationKey description)
        {
            InternalName = internalName;
            DescriptionID = description;
            NameID = name;
        }

        public bool Equals(VerbalStructureDescription other)
        {
            return Equals(InternalName, other.InternalName);
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
                return (InternalName.GetHashCode() * 397) ^ InternalName.GetHashCode();
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