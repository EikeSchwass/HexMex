using System;
using System.Linq;

namespace HexMex.Game.Buildings
{
    public abstract class Building : Structure, IHasProgress
    {
        public event Action<Building> ProductionCompleted;
        public event Action<Building> ProductionStarted;
        public float ProductionTime { get; }
        public float CurrentProductionTime { get; private set; }
        public bool IsProducing { get; private set; }
        public float Progress => IsProducing ? CurrentProductionTime / ProductionTime : 0;

        public bool IsSuspended { get; private set; }

        private bool NotifiedAddedToWorld { get; set; }

        protected Building(HexagonNode position, World world, float productionTime, StructureDescription description) : base(position, world, description)
        {
            ProductionTime = productionTime;
            ResourceDirector.AllIngredientsArrived += ResourceDirector_AllIngredientsArrived;
            ResourceDirector.AllProvisionsLeft += ResourceDirector_AllProvisionsLeft;
        }

        public void Suspend()
        {
            IsSuspended = true;
        }

        public void Resume()
        {
            IsSuspended = false;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!NotifiedAddedToWorld)
            {
                OnAddedToWorld();
                NotifiedAddedToWorld = true;
            }
            if (!IsProducing || IsSuspended)
                return;
            CurrentProductionTime += dt;
            if (CurrentProductionTime >= ProductionTime && !ResourceDirector.PendingProvisions.Any())
                CompleteProduction();
        }

        protected abstract void OnAddedToWorld();

        protected abstract void OnProductionCompleted();

        protected abstract void OnProductionStarted();

        private void CheckAndStartProduction()
        {
            if (ResourceDirector.ReadyForProduction && !IsProducing)
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