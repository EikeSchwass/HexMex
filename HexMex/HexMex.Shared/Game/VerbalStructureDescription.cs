namespace HexMex.Game
{
    public struct VerbalStructureDescription
    {
        public TranslationKey NameID { get; }
        public TranslationKey DescriptionID { get; }

        public VerbalStructureDescription(TranslationKey name, TranslationKey description)
        {
            DescriptionID = description;
            NameID = name;
        }

        public bool Equals(VerbalStructureDescription other)
        {
            return string.Equals(NameID, other.NameID) && string.Equals(DescriptionID, other.DescriptionID);
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
                return (NameID.GetHashCode() * 397) ^ DescriptionID.GetHashCode();
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