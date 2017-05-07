using System;

namespace HexMex.Game.Buildings
{
    public class VillageBuilding : Building
    {
        public VillageBuilding(HexagonNode position, ResourceManager resourceManager) : base(position, resourceManager)
        {
        }

        public override event Action<Structure> RequiresRedraw;

        public override void OnRequiresRedraw()
        {
            RequiresRedraw?.Invoke(this);
        }
    }
}