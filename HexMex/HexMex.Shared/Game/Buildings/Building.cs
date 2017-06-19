using System;
using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Building : Structure, IHasProgress
    {
        public event Action<Building> ProductionCompleted;
        public event Action<Building> ProductionStarted;
        public float ProductionTime { get; }
        public float CurrentProductionTime { get; protected set; }
        public bool IsProducing { get; private set; }
        public float Progress => IsProducing ? CurrentProductionTime / ProductionTime : 0;

        public bool IsSuspended { get; private set; }

        private bool NotifiedAddedToWorld { get; set; }
        private bool Enqueued { get; set; }

        public Building(HexagonNode position, World world, BuildingDescription buildingDescription) : base(position, world, buildingDescription)
        {
            ProductionTime = buildingDescription.ProductionInformation.ProductionTime;
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
            if (IsSuspended)
                return;
            if (IsProducing)
            {
                CurrentProductionTime += dt;
                if (CurrentProductionTime >= ProductionTime && !ResourceDirector.PendingProvisions.Any())
                    CompleteProduction();
            }
        }

        protected void CheckAndStartProduction()
        {
            if (!IsProducing && ResourceDirector.ReadyForProduction && !Enqueued)
            {
                var energy = Description.ProductionInformation.Ingredients.EnvironmentResource.Energy;
                if (energy > 0)
                {
                    Enqueued = true;
                    World.GlobalResourceManager.Enqueue(new EnergyPackage(energy,
                                                                          e =>
                                                                          {
                                                                              CurrentProductionTime = 0;
                                                                              IsProducing = true;
                                                                              OnProductionStarted();
                                                                              ProductionStarted?.Invoke(this);
                                                                              Enqueued = false;
                                                                          }));
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

        protected virtual void OnAddedToWorld()
        {
            RequestIngredients();
        }

        protected virtual void OnProductionCompleted()
        {
            if (Description.VerbalStructureDescription.InternalName.IndexOf("Barrel", StringComparison.OrdinalIgnoreCase) != -1)
            {
                Console.WriteLine("Barrel produces");
            }
            World.GlobalResourceManager.Knowledge += Description.ProductionInformation.Products.Knowledge;
            World.GlobalResourceManager.EnvironmentResource += Description.ProductionInformation.Products.EnvironmentResource;
            var resourceTypeSources = Description.ProductionInformation.Products.ResourceTypes.ToArray();
            if (resourceTypeSources.Any())
                ResourceDirector.ProvideResources(resourceTypeSources);
            else
                ResourceDirector_AllProvisionsLeft(ResourceDirector);
        }

        protected virtual void OnProductionStarted()
        {
            RequestIngredients();
        }

        protected virtual void RequestIngredients()
        {
            ResourceDirector.RequestIngredients(Description.ProductionInformation.Ingredients.ResourceTypes.ToArray());
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
            if (Description.VerbalStructureDescription.InternalName.IndexOf("Barrel", StringComparison.OrdinalIgnoreCase) != -1)
            {
                Console.WriteLine("Barrel on its way");
            }
        }
    }
}