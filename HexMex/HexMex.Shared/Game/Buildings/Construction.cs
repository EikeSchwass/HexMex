using System;
using CocosSharp;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, ICCUpdatable, IHasResourceStorage, IHasProgress
    {
        public Construction(ConstructionCompletedDelegate onConstructionCompleted, HexagonNode position, ResourceManager resourceManager, float constructionTime, params ResourceIngredient[] ingredients) : base(position, resourceManager)
        {
            OnConstructionCompleted = onConstructionCompleted;
            ConstructionTime = constructionTime;
            Ingredients = ingredients;
        }

        public override event Action<Structure> RequiresRedraw;

        public bool IsConstructing { get; private set; }

        public float PassedConstructionTime { get; private set; }
        public float Progress { get; private set; }

        public ResourceStorage ResourceStorage { get; } = new ResourceStorage();

        private ConstructionCompletedDelegate OnConstructionCompleted { get; }
        public float ConstructionTime { get; }
        private ResourceIngredient[] Ingredients { get; }

        public override void OnResourceArrived(ResourcePackage resource)
        {
            base.OnResourceArrived(resource);
            ResourceStorage.StoreResource(resource);
            if (ResourceStorage.HasEnoughStored(Ingredients))
            {
                ResourceStorage.RemoveResources(Ingredients);
                IsConstructing = true;
                RequiresRedraw?.Invoke(this);
            }
        }

        public override void OnRequiresRedraw()
        {
            RequiresRedraw?.Invoke(this);
        }

        public void StartConstruction()
        {
            foreach (var ingredient in Ingredients)
            {
                for (int i = 0; i < ingredient.Amount; i++)
                {
                    RequestResource(ingredient.ResourceType);
                }
            }
        }

        public void Update(float dt)
        {
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