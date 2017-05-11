namespace HexMex.Game.Buildings
{
    [BuildingInformation("Village", 5, ResourceType.Bread, ResourceType.Bread, ResourceType.Bread)]
    public class VillageBuilding : Building
    {
        public VillageBuilding(HexagonNode position, ResourceManager resourceManager, HexagonManager hexagonManager) : base(position, resourceManager, hexagonManager, 5, new[] { ResourceType.Water }, new[] { ResourceType.Water })
        {
        }
    }
}