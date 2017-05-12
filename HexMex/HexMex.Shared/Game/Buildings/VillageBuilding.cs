namespace HexMex.Game.Buildings
{
    [BuildingInformation("Village", "Whatever", 5, new[] { ResourceType.Water }, new[] { ResourceType.Bread, ResourceType.Water }, 5, ResourceType.Bread, ResourceType.Bread, ResourceType.Bread)]
    public class VillageBuilding : Building
    {
        public VillageBuilding(HexagonNode position, World world) : base(position, world, 5, new[] { ResourceType.Water }, new[] { ResourceType.Water, ResourceType.Bread, })
        {
        }
    }
}