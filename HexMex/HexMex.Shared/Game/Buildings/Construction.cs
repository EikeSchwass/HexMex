using System;
using System.Linq;

namespace HexMex.Game.Buildings
{
    public class Construction : Structure, IHasProgress
    {
        public BuildingConstructionFactory ConstructionFactory { get; }

        public bool IsConstructing { get; private set; }
        public float PassedConstructionTime { get; private set; }

        private bool ResourcesRequested { get; set; }

        public event Action<Construction> ConstructionCompleted;

        public float Progress { get; private set; }

        public Construction(HexagonNode position, BuildingConstructionFactory buildingConstructionFactory, World world) : base(position, world, new BuildingDescription(new VerbalStructureDescription(new TranslationKey("constructionName"), new TranslationKey("constructionDescription")), Knowledge.Zero, buildingConstructionFactory.StructureDescription.ConstructionInformation, new RenderInformation("constructionFill", "constructionBorder")))
        {
            ConstructionFactory = buildingConstructionFactory;
            ResourceDirector.AllIngredientsArrived += StartConstructing;
        }

        public sealed override void Update(float dt)
        {
            base.Update(dt);
            if (!ResourcesRequested)
            {
                ResourcesRequested = true;
                ResourceDirector.RequestIngredients(ConstructionFactory.StructureDescription.ConstructionInformation.ResourceTypes.ToArray());
            }
            if (!IsConstructing)
                return;
            PassedConstructionTime += dt;
            Progress = PassedConstructionTime / ConstructionFactory.StructureDescription.ConstructionInformation.ConstructionTime;
            if (PassedConstructionTime >= ConstructionFactory.StructureDescription.ConstructionInformation.ConstructionTime)
            {
                PassedConstructionTime = 0;
                IsConstructing = false;

                var structure = ConstructionFactory.CreateFunction(Position, World);
                World.StructureManager.RemoveStructure(this);
                World.StructureManager.CreateStrucuture(structure);
                ConstructionCompleted?.Invoke(this);
            }
            OnRequiresRedraw();
        }

        private void StartConstructing(ResourceDirector arg1, ResourceType[] arg2)
        {
            World.GlobalResourceManager.Enqueue(new EnergyPackage(ConstructionFactory.StructureDescription.ConstructionInformation.EnvironmentResource.Energy, e => IsConstructing = true));
        }
    }
}