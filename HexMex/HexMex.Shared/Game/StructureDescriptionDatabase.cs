using System;
using HexMex.Game.Buildings;
using HexMex.Helper;
using static HexMex.Game.ResourceType;

namespace HexMex.Game
{
    public static class StructureDescriptionDatabase
    {
        private static StructureDescription BarrelBlacksmith { get; }
        private static StructureDescription BrickFactory { get; }
        private static StructureDescription CircuitFactory { get; }
        private static StructureDescription CoalPowerplant { get; }
        private static StructureDescription CoalRefinery { get; }
        private static StructureDescription CopperExtractor { get; }
        private static StructureDescription DiamondExtractor { get; }
        private static StructureDescription Forestry { get; }
        private static StructureDescription GlasFactory { get; }
        private static StructureDescription GoldExtractor { get; }
        private static StructureDescription Habor { get; }
        private static StructureDescription IronExtractor { get; }
        private static StructureDescription Laboratory1 { get; }
        private static StructureDescription Laboratory2 { get; }
        private static StructureDescription Laboratory3 { get; }
        private static StructureDescription PaperFactory { get; }
        private static StructureDescription PotaschFactory { get; }
        private static StructureDescription SandFactory { get; }
        private static StructureDescription SolarPowerplant { get; }
        private static StructureDescription ToolBlacksmith { get; }
        private static StructureDescription WaterPowerplant { get; }
        private static StructureDescription WaterPump { get; }

        static StructureDescriptionDatabase()
        {
            Habor = new StructureDescription(new VerbalStructureDescription("Habor", "Must be placed adjacent to water. Trades diamonds for needed resources."),
                                             new Knowledge(10, 0, 0),
                                             new ConstructionInformation(10, Iron.FromNetwork(), Wood.FromNetwork(), Wood.FromNetwork(), Copper.FromNetwork()),
                                             new ProducerInformation(new IngredientsCollection(Diamond.FromNetwork(), PureWater.FromHexagon()), new ProductsCollection(Anything.FromNetwork()), 2.5f));
            WaterPump = new StructureDescription(new VerbalStructureDescription("Water Pump", "Pumps water from an adjacent hexagon and fills it into barrels for transportation."),
                                                 new Knowledge(8, 5, 0),
                                                 new ConstructionInformation(7, Brick.FromNetwork(), Copper.FromNetwork(), Tools.FromNetwork(), Circuit.FromNetwork()),
                                                 new ProducerInformation(new IngredientsCollection(Barrel.FromNetwork(), PureWater.FromNetwork(), PureWater.FromNetwork()), new ProductsCollection(WaterBarrel.FromNetwork()), 4f));
            BarrelBlacksmith = new StructureDescription(new VerbalStructureDescription("Barrel Blacksmith", "Produces barrels that can be used to transport water."),
                                                        new Knowledge(8, 2, 0),
                                                        new ConstructionInformation(5, Iron.FromNetwork(), Iron.FromNetwork(), Wood.FromNetwork()),
                                                        new ProducerInformation(new IngredientsCollection(Iron.FromNetwork(), Wood.FromNetwork(), Wood.FromNetwork()), new ProductsCollection(Barrel.FromNetwork()), 4));
            BrickFactory = new StructureDescription(new VerbalStructureDescription("Brick Factory", "Produces bricks from stone. Needs to be adjacent to stone"),
                                                    new Knowledge(5, 0, 0),
                                                    new ConstructionInformation(8, Iron.FromNetwork(), Copper.FromNetwork(), Wood.FromNetwork()),
                                                    new ProducerInformation(new IngredientsCollection(Stone.FromNetwork(), Stone.FromNetwork(), Water.FromNetwork()), new ProductsCollection(Brick.FromNetwork(), Brick.FromNetwork()), 8f));
            CircuitFactory = new StructureDescription(new VerbalStructureDescription("Circuit Factory", "Produces circuits."),
                                                      new Knowledge(20, 15, 0),
                                                      new ConstructionInformation(7, Iron.FromNetwork(), Copper.FromNetwork(), Copper.FromNetwork(), Tools.FromNetwork()),
                                                      new ProducerInformation(new IngredientsCollection(Iron.FromNetwork(), Gold.FromNetwork(), Copper.FromNetwork(), Copper.FromNetwork()), new ProductsCollection(Circuit.FromNetwork()), 9f));
            CoalPowerplant = new StructureDescription(new VerbalStructureDescription("Coal Powerplant", "Burns Coal to produce power"),
                                                      Knowledge.Zero,
                                                      new ConstructionInformation(7, Brick.FromNetwork(), Brick.FromNetwork(), Wood.FromNetwork()),
                                                      new ProducerInformation(new IngredientsCollection(Coal.FromNetwork(), Coal.FromNetwork()), new ProductsCollection(new EnvironmentResource(10, 0, 4)), 1f));
            CoalRefinery = new StructureDescription(new VerbalStructureDescription("Coal Refinery", "Converts coal ore to coal"),
                                                    Knowledge.Zero,
                                                    new ConstructionInformation(15, Coal.FromNetwork(), Coal.FromNetwork(), Iron.FromNetwork()),
                                                    new ProducerInformation(new IngredientsCollection(CoalOre.FromHexagon()), new ProductsCollection(Coal.FromNetwork()), 5));
            CopperExtractor = new StructureDescription("Copper Extractor",
                                                       "Extracts copper from adjacent hexagons.",
                                                       new ResourceCollection(Iron, Copper, Wood),
                                                       7,
                                                       new ResourceCollection(CopperOre),
                                                       new ResourceCollection(Copper),
                                                       2);
            DiamondExtractor = new StructureDescription("Diamond Extractor",
                                                        "Extracts diamonds from adjacent hexagons. Diamonds can be used to trade at a habor.",
                                                        new ResourceCollection(Tools, Circuit, Circuit, Gold, Copper, Glas),
                                                        20,
                                                        new ResourceCollection(DiamondOre),
                                                        new ResourceCollection(Diamond),
                                                        3);
            Forestry = new StructureDescription("Forestry",
                                                "Needs to be placed adjacent to a forest (duh).",
                                                new ResourceCollection(Iron, Wood, Wood),
                                                3,
                                                new ResourceCollection(Tree),
                                                new ResourceCollection(Wood, Wood, Wood),
                                                2f);
            GlasFactory = new StructureDescription("Glas Factory",
                                                   "Produces glas from potash and sand.",
                                                   new ResourceCollection(Iron, Iron, Brick, Circuit, Coal, Coal, Copper, Wood),
                                                   12,
                                                   new ResourceCollection(Pottasche, Pottasche, Pottasche, Pottasche, Sand, Sand, Sand, Sand, Sand),
                                                   new ResourceCollection(Glas),
                                                   14f);
            GoldExtractor = new StructureDescription("Gold Extractor",
                                                     "Extracts gold from adjacent hexagons.",
                                                     new ResourceCollection(Iron, Iron, Stone, Copper, Wood),
                                                     7,
                                                     new ResourceCollection(GoldOre),
                                                     new ResourceCollection(Gold),
                                                     3.5f);
            IronExtractor = new StructureDescription("Iron Extractor",
                                                     "Extracts iron from adjacent hexagons.",
                                                     new ResourceCollection(Iron, Iron, Wood),
                                                     8,
                                                     new ResourceCollection(IronOre),
                                                     new ResourceCollection(Iron),
                                                     2);
            Laboratory1 = new StructureDescription("Laboratory 1",
                                                   "Generates simple knowledge",
                                                   new ResourceCollection(Brick, Iron, Copper, Wood),
                                                   7,
                                                   new ResourceCollection(Water, Coal, Wood),
                                                   new ResourceCollection(Knowledge1),
                                                   5f);
            Laboratory2 = new StructureDescription("Laboratory 2",
                                                   "Generates intermediate knowledge",
                                                   new ResourceCollection(Brick, Iron, Tools, Copper, Wood),
                                                   10,
                                                   new ResourceCollection(Tools, Water, Paper),
                                                   new ResourceCollection(Knowledge2),
                                                   10f);
            Laboratory3 = new StructureDescription("Laboratory 3",
                                                   "Generates advanced knowledge",
                                                   new ResourceCollection(Brick, Iron, Tools, Tools, Copper, Circuit, Circuit),
                                                   15,
                                                   new ResourceCollection(Tools, Water, Paper, Circuit),
                                                   new ResourceCollection(Knowledge3),
                                                   20f);
            PaperFactory = new StructureDescription("Paper Factory",
                                                    "Produces paper from wood.",
                                                    new ResourceCollection(Copper, Iron, Tools),
                                                    3,
                                                    new ResourceCollection(Wood, Wood, Water),
                                                    new ResourceCollection(Paper),
                                                    3f);
            PotaschFactory = new StructureDescription("Potasch Factory",
                                                      "Produces potasch for glas.",
                                                      new ResourceCollection(Brick, Iron, Copper, Coal, Wood, Wood),
                                                      12,
                                                      new ResourceCollection(Water, Wood, Wood, Coal),
                                                      new ResourceCollection(Pottasche),
                                                      5f);
            SandFactory = new StructureDescription("Sand Factory",
                                                   "Produces sand from stone. Needs to be adjacent to stone",
                                                   new ResourceCollection(Iron, Tools, Copper, Wood),
                                                   10,
                                                   new ResourceCollection(Stone, Stone),
                                                   new ResourceCollection(Sand, Sand, Sand),
                                                   7f);
            SolarPowerplant = new StructureDescription("Solar Powerplant",
                                                       "Produces energy from sunlight",
                                                       new ResourceCollection(Iron, Tools, Copper, Wood),
                                                       10,
                                                       new ResourceCollection(),
                                                       new ResourceCollection((EnvironmentResource)1),
                                                       12f);
            ToolBlacksmith = new StructureDescription("Tool Blacksmith",
                                                      "Needs Iron and produces tools.",
                                                      new ResourceCollection(Iron, Iron, Iron),
                                                      6,
                                                      new ResourceCollection(Iron),
                                                      new ResourceCollection(Tools),
                                                      3f);
            WaterPowerplant = new StructureDescription("Water Powerplant",
                                                       "Needs adjacent water to produce power",
                                                       new ResourceCollection(Brick, Brick, Iron, Wood),
                                                       12,
                                                       new ResourceCollection(PureWater, PureWater, PureWater, PureWater, PureWater, PureWater),
                                                       new ResourceCollection(Energy, Energy),
                                                       1f);
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
            if (buildingType == typeof(SolarPowerplant))
                return SolarPowerplant;
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