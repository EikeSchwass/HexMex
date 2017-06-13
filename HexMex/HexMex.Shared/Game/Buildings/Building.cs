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
        private bool Enqueued { get; set; }

        protected Building(HexagonNode position, World world, StructureDescription structureDescription) : base(position, world, structureDescription)
        {
            ProductionTime = structureDescription.ProductionInformation.ProductionTime;
            ResourceDirector.AllIngredientsArrived += ResourceDirector_AllIngredientsArrived;
            ResourceDirector.AllProvisionsLeft += ResourceDirector_AllProvisionsLeft;
        }

        public void Resume()
        {
            IsSuspended = false;
        }

        public void Suspend()
        {
            IsSuspended = true;
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

        protected virtual void OnAddedToWorld()
        {
            RequestIngredients();
        }

        protected virtual void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Description.ProductionInformation.Products.ResourceTypes.ToArray());
            World.GlobalResourceManager.EnvironmentResource += Description.ProductionInformation.Products.EnvironmentResource;
        }

        protected virtual void OnProductionStarted()
        {
            RequestIngredients();
        }

        protected virtual void RequestIngredients()
        {
            ResourceDirector.RequestIngredients(Description.ProductionInformation.Ingredients.ResourceTypes.ToArray());
        }

        protected void CheckAndStartProduction()
        {
            if (ResourceDirector.ReadyForProduction && !IsProducing && !Enqueued)
            {
                var energy = Description.ProductionInformation.Ingredients.EnvironmentResource.Energy;
                if (energy > 0)
                {
                    World.GlobalResourceManager.Enqueue(new EnergyPackage(energy,
                                                                          e =>
                                                                          {
                                                                              CurrentProductionTime = 0;
                                                                              IsProducing = true;
                                                                              OnProductionStarted();
                                                                              ProductionStarted?.Invoke(this);
                                                                              Enqueued = false;
                                                                          }));
                    Enqueued = true;
                }
                else
                {
                    CurrentProductionTime = 0;
                    IsProducing = true;
                    OnProductionStarted();
                    ProductionStarted?.Invoke(this);
                }
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