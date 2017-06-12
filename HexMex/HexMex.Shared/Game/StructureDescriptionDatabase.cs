using System;
using HexMex.Game.Buildings;
using static HexMex.Game.ResourceType;
using static HexMex.Game.StructureDescription;

namespace HexMex.Game
{
    public static class StructureDescriptionDatabase
    {
        public static StructureDescription BarrelBlacksmith { get; }
        public static StructureDescription BrickFactory { get; }
        public static StructureDescription CircuitFactory { get; }
        public static StructureDescription CoalPowerplant { get; }
        public static StructureDescription CoalRefinery { get; }
        public static StructureDescription CopperExtractor { get; }
        public static StructureDescription DiamondExtractor { get; }
        public static StructureDescription Forestry { get; }
        public static StructureDescription GlasFactory { get; }
        public static StructureDescription GoldExtractor { get; }
        public static StructureDescription Habor { get; }
        public static StructureDescription IronExtractor { get; }
        public static StructureDescription Laboratory1 { get; }
        public static StructureDescription Laboratory2 { get; }
        public static StructureDescription Laboratory3 { get; }
        public static StructureDescription PaperFactory { get; }
        public static StructureDescription PotaschFactory { get; }
        public static StructureDescription SandFactory { get; }
        public static StructureDescription ToolBlacksmith { get; }
        public static StructureDescription WaterPowerplant { get; }
        public static StructureDescription WaterPump { get; }

        static StructureDescriptionDatabase()
        {
            Habor = new StructureDescription("Habor", "Must be placed adjacent to water. Trades diamonds for needed resources.", new ResourceCollection(Iron, Wood, Wood, Copper), 10, new ResourceCollection(Diamond, PureWater), new ResourceCollection(Anything), 2.5f);
            WaterPump = new StructureDescription("Water Pump", "Pumps water from an adjacent hexagon and fills it into barrels for transportation.", new ResourceCollection(Brick, Copper, Tools, Circuit), 7, new ResourceCollection(Barrel, PureWater, PureWater), new ResourceCollection(WaterBarrel), 4f);
            BarrelBlacksmith = new StructureDescription("Barrel Blacksmith", "Produces barrels that can be used to transport water.", new ResourceCollection(0, Iron, Iron, Wood), 5, new ResourceCollection(0, Iron, Wood, Wood), new ResourceCollection(Barrel), 4f);
            BrickFactory = new StructureDescription("Brick Factory", "Produces bricks from stone. Needs to be adjacent to stone", new ResourceCollection(Iron, Copper, Wood), 8, new ResourceCollection(Stone, Stone, Water), new ResourceCollection(Brick, Brick), 8f);
            CircuitFactory = new StructureDescription("Circuit Factory", "Produces circuits.", new ResourceCollection(Iron, Copper, Copper, Tools), 7, new ResourceCollection(Iron, Gold, Copper, Copper), new ResourceCollection(Circuit), 9f);
            CoalPowerplant = new StructureDescription("Coal Powerplant", "Burns Coal to produce power", new ResourceCollection(Brick, Brick, Wood), 7, new ResourceCollection(Coal, Coal), new ResourceCollection(Energy, Energy, Energy, Energy), 1f);
            CoalRefinery = new StructureDescription("Coal Refinery", "Converts coal ore to coal", new ResourceCollection(Coal, Coal, Iron), 15, new ResourceCollection(CoalOre), new ResourceCollection(Coal), 5);
            CopperExtractor = new StructureDescription("Copper Extractor", "Extracts copper from adjacent hexagons.", new ResourceCollection(Iron, Copper, Wood), 7, new ResourceCollection(CopperOre), new ResourceCollection(Copper), 2);
            DiamondExtractor = new StructureDescription("Diamond Extractor", "Extracts diamonds from adjacent hexagons. Diamonds can be used to trade at a habor.", new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas), 20, new ResourceCollection(DiamondOre), new ResourceCollection(Diamond), 3);
            Forestry = new StructureDescription("Forestry", "Needs to be placed adjacent to a forest (duh).", new ResourceCollection(Iron, Wood, Wood), 3, new ResourceCollection(Tree), new ResourceCollection(Wood, Wood, Wood), 2f);
            GlasFactory = new StructureDescription("Glas Factory", "Produces glas from potash and sand.", new ResourceCollection(Iron, Iron, Brick, Circuit, Coal, Coal, Copper, Wood), 12, new ResourceCollection(Pottasche, Pottasche, Pottasche, Pottasche, Sand, Sand, Sand, Sand, Sand), new ResourceCollection(Glas), 14f);
            GoldExtractor = new StructureDescription("Gold Extractor", "Extracts gold from adjacent hexagons.", new ResourceCollection(Iron, Iron, Stone, Copper, Wood), 7, new ResourceCollection(GoldOre), new ResourceCollection(Gold), 3.5f);
            IronExtractor = new StructureDescription("Iron Extractor", "Extracts iron from adjacent hexagons.", new ResourceCollection(Iron, Iron, Wood), 8, new ResourceCollection(IronOre), new ResourceCollection(Iron), 2);
            Laboratory1 = new StructureDescription("Laboratory 1", "Generates simple knowledge", new ResourceCollection(Brick, Iron, Copper, Wood), 7, new ResourceCollection(Water, Coal, Wood), new ResourceCollection(Knowledge1), 5f);
            Laboratory2 = new StructureDescription("Laboratory 2", "Generates intermediate knowledge", new ResourceCollection(Brick, Iron, Tools, Copper, Wood), 10, new ResourceCollection(Tools, Water, Paper), new ResourceCollection(Knowledge2), 10f);
            Laboratory3 = new StructureDescription("Laboratory 3", "Generates advanced knowledge", new ResourceCollection(Brick, Iron, Tools, Tools, Copper, Circuit, Circuit), 15, new ResourceCollection(Tools, Water, Paper, Circuit), new ResourceCollection(Knowledge3), 20f);
            PaperFactory = new StructureDescription("Paper Factory", "Produces paper from wood.", new ResourceCollection(Copper, Iron, Tools), 3, new ResourceCollection(Wood, Wood, Water), new ResourceCollection(Paper), 3f);
            PotaschFactory = new StructureDescription("Potasch Factory", "Produces potasch for glas.", new ResourceCollection(Brick, Iron, Copper, Coal, Wood, Wood), 12, new ResourceCollection(Water, Wood, Wood, Coal), new ResourceCollection(Pottasche), 5f);
            SandFactory = new StructureDescription("Sand Factory", "Produces sand from stone. Needs to be adjacent to stone", new ResourceCollection(Iron, Tools, Copper, Wood), 10, new ResourceCollection(Stone, Stone), new ResourceCollection(Sand, Sand, Sand), 7f);
            ToolBlacksmith= new StructureDescription("Tool Blacksmith", "Needs Iron and produces tools.", new ResourceCollection(Iron, Iron, Iron), 6, new ResourceCollection(Iron), new ResourceCollection(Tools), 3f);
            WaterPowerplant = new StructureDescription("Water Powerplant", "Needs adjacent water to produce power", new ResourceCollection(Brick, Brick, Iron, Wood), 12, new ResourceCollection(PureWater, PureWater, PureWater, PureWater, PureWater, PureWater), new ResourceCollection(Energy, Energy), 1f);
        }

        public static StructureDescription Get<T>() where T : Building => Get(typeof(T));

        public static StructureDescription Get(Type buildingType)
        {
            if (buildingType == typeof(BarrelBlacksmith))
                return BarrelBlacksmith;
            if (buildingType == typeof(BrickFactory))
                return BrickFactory;
            if (buildingType == typeof(CircuitFactory))
                return CircuitFactory;
            if (buildingType == typeof(CoalPowerplant))
                return CoalPowerplant;
            if (buildingType == typeof(CoalRefinery))
                return CoalRefinery;
            if (buildingType == typeof(CopperExtractor))
                return CopperExtractor;
            if (buildingType == typeof(DiamondExtractor))
                return DiamondExtractor;
            if (buildingType == typeof(Forestry))
                return Forestry;
            if (buildingType == typeof(GlasFactory))
                return GlasFactory;
            if (buildingType == typeof(GoldExtractor))
                return GoldExtractor;
            if (buildingType == typeof(Habor))
                return Habor;
            if (buildingType == typeof(IronExtractor))
                return IronExtractor;
            if (buildingType == typeof(Laboratory1))
                return Laboratory1;
            if (buildingType == typeof(Laboratory2))
                return Laboratory2;
            if (buildingType == typeof(Laboratory3))
                return Laboratory3;
            if (buildingType == typeof(PaperFactory))
                return PaperFactory;
            if (buildingType == typeof(PotaschFactory))
                return PotaschFactory;
            if (buildingType == typeof(SandFactory))
                return SandFactory;
            if (buildingType == typeof(ToolBlacksmith))
                return ToolBlacksmith;
            if (buildingType == typeof(WaterPowerplant))
                return WaterPowerplant;
            if (buildingType == typeof(WaterPump))
                return WaterPump;

            throw new ArgumentException(nameof(buildingType));
        }
    }
}