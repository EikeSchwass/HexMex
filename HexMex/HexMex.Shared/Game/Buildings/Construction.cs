using System;
using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public event Action<Construction> ConstructionCompleted;
        public BuildingDescription BuildingDescription { get; }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        public float Progress { get; private set; }

        private bool ResourcesRequested { get; set; }

        public Construction(HexagonNode position, BuildingDescription buildingDescription, World world) : base(position, world, new BuildingDescription(new VerbalStructureDescription("Construction", new TranslationKey("constructionName"), new TranslationKey("constructionDescription")), Knowledge.Zero, buildingDescription.ConstructionInformation, new RenderInformation("constructionFill", "constructionBorder"), false))
        {
            BuildingDescription = buildingDescription;
            ResourceDirector.AllIngredientsArrived += StartConstructing;
        }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (!ResourcesRequested)
            {
                ResourcesRequested = true;
                ResourceDirector.RequestIngredients(BuildingDescription.ConstructionInformation.ResourceTypes.ToArray());
            }
            if (!IsConstructing)
                return;
            PassedConstructionTime += dt;
            Progress = PassedConstructionTime / BuildingDescription.ConstructionInformation.ConstructionTime;
            if (PassedConstructionTime >= BuildingDescription.ConstructionInformation.ConstructionTime)
            {
                PassedConstructionTime = 0;
                IsConstructing = false;

                Building building = new Building(Position, World, BuildingDescription);
                World.StructureManager.RemoveStructure(this);
                World.StructureManager.CreateStrucuture(building);
                ConstructionCompleted?.Invoke(this);
            }
            OnRequiresRedraw();
        }

        private void StartConstructing(ResourceDirector arg1, ResourceType[] arg2)
        {
            World.GlobalResourceManager.Enqueue(new EnergyPackage(BuildingDescription.ConstructionInformation.EnvironmentResource.Energy, e => IsConstructing = true));
        }
    }
}