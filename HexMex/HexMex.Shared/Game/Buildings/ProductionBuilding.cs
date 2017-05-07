using CocosSharp;

namespace HexMex.Game.Buildings
{
    public abstract class ProductionBuilding : Building, IHasResourceStorage, ICCUpdatable
    {
        protected ProductionBuilding(HexagonNode position, ResourceManager resourceManager) : base(position, resourceManager)
        {
            ResourceStorage = new ResourceStorage();
        }

        public Recipe Recipe { get; protected set; }

        public ResourceStorage ResourceStorage { get; }
        public abstract void Update(float dt);
    }
}