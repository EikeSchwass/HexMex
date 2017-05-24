using HexMex.Controls;
using static HexMex.Game.ResourceType;

namespace HexMex.Game.Buildings
{
    public class WaterPump : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Water Pump", "Pumps water from an adjacent hexagon and fills it into barrels for transportation.", new StructureDescription.ResourceCollection(Brick, Copper, Tools, Circuit), 7, new StructureDescription.ResourceCollection(Barrel, PureWater, PureWater), new StructureDescription.ResourceCollection(WaterBarrel), 4f);

        public WaterPump(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.BlueDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { Barrel }, new[] { PureWater, PureWater });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(WaterBarrel);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { Barrel }, new[] { PureWater, PureWater });
        }
    }

    public class CoalPowerplant : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Coal Powerplant", "Burns Coal to produce power", new StructureDescription.ResourceCollection(Brick, Brick, Wood), 7, new StructureDescription.ResourceCollection(Coal, Coal), new StructureDescription.ResourceCollection(Energy, Energy, Energy, Energy), 1f);

        public CoalPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.GrayVeryDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { Coal, Coal }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Energy, Energy, Energy, Energy);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { Coal, Coal }, null);
        }
    }

    public class WaterPowerplant : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Water Powerplant", "Needs adjacent water to produce power", new StructureDescription.ResourceCollection(Brick, Brick, Iron, Wood), 12, new StructureDescription.ResourceCollection(PureWater, PureWater, PureWater, PureWater, PureWater, PureWater), new StructureDescription.ResourceCollection(Energy, Energy), 1f);

        public WaterPowerplant(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.BlueLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { PureWater, PureWater, PureWater, PureWater, PureWater, PureWater });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Energy, Energy);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { PureWater, PureWater, PureWater, PureWater, PureWater, PureWater });
        }
    }

    public class PotaschFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Potasch Factory", "Produces potasch for glas.", new StructureDescription.ResourceCollection(Brick, Iron, Copper, Coal, Wood, Wood), 12, new StructureDescription.ResourceCollection(Water, Wood, Wood, Coal), new StructureDescription.ResourceCollection(Pottasche), 5f);

        public PotaschFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.GrayVeryDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(new[] { Water, Wood, Wood, Coal }, null);
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Pottasche);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(new[] { Water, Wood, Wood, Coal }, null);
        }
    }

    public class BrickFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Brick Factory", "Produces bricks from stone. Needs to be adjacent to stone", new StructureDescription.ResourceCollection(Iron, Copper, Wood), 8, new StructureDescription.ResourceCollection(Stone, Stone, Water), new StructureDescription.ResourceCollection(Brick, Brick), 8f);

        public BrickFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.RedLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Sand, Sand, Sand);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }
    }


    public class GlasFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Glas Factory", "Produces glas from potash and sand.", new StructureDescription.ResourceCollection(Iron, Iron, Brick, Circuit, Coal, Coal, Copper, Wood), 12, new StructureDescription.ResourceCollection(Pottasche, Pottasche, Pottasche, Pottasche, Sand, Sand, Sand, Sand, Sand), new StructureDescription.ResourceCollection(Glas), 14f);

        public GlasFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.RedLight,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Sand, Sand, Sand);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }
    }

    public class SandFactory : Building
    {
        public static StructureDescription StructureDescription { get; } = new StructureDescription("Sand Factory", "Produces sand from stone. Needs to be adjacent to stone", new StructureDescription.ResourceCollection(Iron, Tools, Copper, Wood), 10, new StructureDescription.ResourceCollection(Stone, Stone), new StructureDescription.ResourceCollection(Sand, Sand, Sand), 7f);

        public SandFactory(HexagonNode position, World world) : base(position, world, StructureDescription.ProductionInformation.ProductionTime) { }

        public override void Render(ExtendedDrawNode drawNode)
        {
            var position = Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var visualSettings = World.GameSettings.VisualSettings;
            drawNode.DrawCircle(position,
                                visualSettings.BuildingRadius,
                                visualSettings.ColorCollection.YellowDark,
                                visualSettings.StructureBorderThickness,
                                visualSettings.ColorCollection.White);
        }

        protected override void OnAddedToWorld()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }

        protected override void OnProductionCompleted()
        {
            ResourceDirector.ProvideResources(Sand, Sand, Sand);
        }

        protected override void OnProductionStarted()
        {
            ResourceDirector.RequestIngredients(null, new[] { Stone, Stone });
        }
    }
}