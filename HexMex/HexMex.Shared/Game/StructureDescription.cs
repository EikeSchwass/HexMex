namespace HexMex.Game
{
    public class StructureDescription
    {
        public ConstructionInformation ConstructionInformation { get; }
        public bool IsProducer => ProductionInformation != null;
        public ProducerInformation ProductionInformation { get; }
        public Knowledge UnlockCost { get; }
        public VerbalStructureDescription VerbalStructureDescription { get; }

        public StructureDescription(VerbalStructureDescription verbalStructureDescription, Knowledge unlockCost, ConstructionInformation constructionInformation, ProducerInformation productionInformation)
        {
            ConstructionInformation = constructionInformation;
            ProductionInformation = productionInformation;
            VerbalStructureDescription = verbalStructureDescription;
            UnlockCost = unlockCost;
        }

        public StructureDescription(VerbalStructureDescription verbalStructureDescription, Knowledge unlockCost, ConstructionInformation constructionInformation) : this(verbalStructureDescription, unlockCost, constructionInformation, null) { }
    }
}