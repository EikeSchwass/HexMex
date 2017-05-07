using System;

namespace HexMex.Game.Buildings
{
    public class MineBuilding : ProductionBuilding
    {
        public MineBuilding(HexagonNode position, ResourceManager resourceManager) : base(position, resourceManager)
        {
            //Recipe = new Recipe(Enumerable.Repeat(new ResourceIngredient(1, ResourceType.Any), 1), Enumerable.Repeat(new ResourceIngredient(1, ResourceType.Any), 1), 5);
        }

        public override event Action<Structure> RequiresRedraw;

        public override void OnRequiresRedraw()
        {
            RequiresRedraw?.Invoke(this);
        }

        public override void Update(float dt)
        {
            
        }
    }
}