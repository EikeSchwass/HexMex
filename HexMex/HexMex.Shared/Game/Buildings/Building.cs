using System;

namespace HexMex.Game.Buildings
{
    public abstract class Building : Structure
    {
        public event Action<Building> ProductionCompleted;
        public event Action<Building> ProductionStarted;
        public float ProductionTime { get; }
        public float CurrentProductionTime { get; private set; }
        public bool IsProducing { get; private set; }

        private bool NotifiedAddedToWorld { get; set; }

        protected Building(HexagonNode position, World world, float productionTime) : base(position, world)
        {
            ProductionTime = productionTime;
            ResourceDirector.AllIngredientsArrived += ResourceDirector_AllIngredientsArrived;
            ResourceDirector.AllProvisionsLeft += ResourceDirector_AllProvisionsLeft;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!NotifiedAddedToWorld)
            {
                OnAddedToWorld();
                NotifiedAddedToWorld = true;
            }
            if (!IsProducing)
                return;
            CurrentProductionTime += dt;
            if (CurrentProductionTime >= ProductionTime)
                CompleteProduction();
        }

        protected virtual void OnAddedToWorld()
        {
        }

        protected abstract void OnProductionCompleted();

        protected abstract void OnProductionStarted();

        private void CheckAndStartProduction()
        {
            if (ResourceDirector.ReadyForProduction)
            {
                CurrentProductionTime = 0;
                IsProducing = true;
                OnProductionStarted();
                ProductionStarted?.Invoke(this);
            }
        }

        private void CompleteProduction()
        {
            CurrentProductionTime = 0;
            IsProducing = false;
            OnProductionCompleted();
            ProductionCompleted?.Invoke(this);
        }

        private void ResourceDirector_AllIngredientsArrived(ResourceDirector arg1, ResourceType[] arg2)
        {
            CheckAndStartProduction();
        }

        private void ResourceDirector_AllProvisionsLeft(ResourceDirector obj)
        {
            CheckAndStartProduction();
        }
    }
}