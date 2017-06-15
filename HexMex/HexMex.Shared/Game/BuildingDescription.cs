namespace HexMex.Game
{
    public class BuildingDescription
    {
        public ConstructionInformation ConstructionInformation { get; }
        public bool IsProducer => ProductionInformation != null;
        public ProductionInformation ProductionInformation { get; }
        public Knowledge UnlockCost { get; }
        public VerbalStructureDescription VerbalStructureDescription { get; }
        public RenderInformation RenderInformation { get; }

        public BuildingDescription(VerbalStructureDescription verbalStructureDescription, Knowledge unlockCost, ConstructionInformation constructionInformation, ProductionInformation productionInformation, RenderInformation renderInformation)
        {
            ConstructionInformation = constructionInformation;
            ProductionInformation = productionInformation;
            RenderInformation = renderInformation;
            VerbalStructureDescription = verbalStructureDescription;
            UnlockCost = unlockCost;
        }

        public BuildingDescription(VerbalStructureDescription verbalStructureDescription, Knowledge unlockCost, ConstructionInformation constructionInformation, RenderInformation renderInformation) : this(verbalStructureDescription, unlockCost, constructionInformation, null, renderInformation) { }
    }
}