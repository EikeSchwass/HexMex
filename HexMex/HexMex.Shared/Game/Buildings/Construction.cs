using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public Construction(HexagonNode position, float constructionTime, ResourceManager resourceManager, HexagonManager hexagonManager, ConstructionCompletedDelegate onConstructionCompleted, ResourceType ingredient, params ResourceType[] ingredients) : base(position, resourceManager, hexagonManager, ingredients.Concat(Enumerable.Repeat(ingredient, 1)), Enumerable.Empty<ResourceType>())
        {
            OnConstructionCompleted = onConstructionCompleted;
            ConstructionTime = constructionTime;
        }

        public bool IsConstructing { get; private set; }

        public float PassedConstructionTime { get; private set; }
        public float Progress { get; private set; }

        private ConstructionCompletedDelegate OnConstructionCompleted { get; }
        public float ConstructionTime { get; }

        protected override void StartProduction()
        {
            base.StartProduction();
            IsConstructing = true;
            Progress = 0;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!IsConstructing)
                return;
            PassedConstructionTime += dt;
            if (PassedConstructionTime >= ConstructionTime)
            {
                PassedConstructionTime = 0;
                IsConstructing = false;
                OnConstructionCompleted(this);
            }
        }
    }
}