using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace HexMex.Game
{
    public class BuildingDescriptionDatabase
    {
        public ReadOnlyCollection<BuildingDescription> BuildingDescriptions { get; }

        private BuildingDescriptionDatabase(IList<BuildingDescription> buildingDescriptions)
        {
            BuildingDescriptions = new ReadOnlyCollection<BuildingDescription>(buildingDescriptions);
        }

        public static BuildingDescriptionDatabase CreateFromXml(string xml)
        {
            List<BuildingDescription> buildingDescriptions = new List<BuildingDescription>();
            using (var xmlReader = XmlReader.Create(new StringReader(xml)))
            {
                var enumerableBuilding = ReadBuildingsFromXml(xmlReader);
                buildingDescriptions.AddRange(enumerableBuilding);
            }

            return new BuildingDescriptionDatabase(buildingDescriptions);
        }

        /*static StructureDescriptionDatabase()
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
        */

        public BuildingDescription ByNameKey(string nameKey) => BuildingDescriptions.FirstOrDefault(b => Equals(b.VerbalStructureDescription.NameID, new TranslationKey(nameKey)));

        private static BuildingDescription LoadBuilding(XmlReader reader)
        {
            var name = reader.GetAttribute("Name") ?? "empty";
            var canExtractWater = Convert.ToBoolean(reader["CanExtractWater"] ?? "False");
            name = name[0].ToString().ToLower() + name.Substring(1);
            var nameID = name + "Name";
            var descriptionID = name + "Description";
            var verbalStructureDescription = new VerbalStructureDescription(new TranslationKey(nameID), new TranslationKey(descriptionID));
            ConstructionInformation constructionInformation = null;
            ProductionInformation productionInformation = null;
            RenderInformation renderInformation = new RenderInformation(name + "Fill", name + "Border");
            Knowledge unlockCost = Knowledge.Zero;

            do
            {
                reader.Read();
                if (!reader.IsStartElement())
                    continue;
                switch (reader.Name)
                {
                    case "ConstructionInformation":
                        constructionInformation = LoadConstructionInformation(reader);
                        break;
                    case "ProductionInformation":
                        productionInformation = LoadProductionInformation(reader);
                        break;
                    case "UnlockCost":
                        unlockCost = LoadUnlockCost(reader);
                        break;
                }
            }
            while (!(string.Equals(reader.Name, "Building") && !reader.IsStartElement()));
            return new BuildingDescription(verbalStructureDescription, unlockCost, constructionInformation, productionInformation, renderInformation, canExtractWater);
        }

        private static ConstructionInformation LoadConstructionInformation(XmlReader reader)
        {
            float constructionTime = Convert.ToSingle(reader["ConstructionTime"]);
            List<ResourceTypeSource> resources = new List<ResourceTypeSource>();
            EnvironmentResource environmentResource = new EnvironmentResource();
            do
            {
                reader.Read();
                reader.MoveToContent();
                if (reader.Name == nameof(EnvironmentResource))
                {
                    int co2 = Convert.ToInt32(reader["CO2"] ?? "0");
                    int o2 = Convert.ToInt32(reader["O2"] ?? "0");
                    int energy = Convert.ToInt32(reader["Energy"] ?? "0");
                    environmentResource = new EnvironmentResource(co2, o2, energy);
                }
                else if (reader.NodeType == XmlNodeType.Element)
                {
                    var resourceName = reader.Name;
                    bool fromHexagon = Convert.ToBoolean(reader["FromHexagon"] ?? "False", CultureInfo.InvariantCulture);
                    var resourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resourceName);
                    var resourceTypeSource = new ResourceTypeSource(resourceType, fromHexagon ? SourceType.Hexagon : SourceType.Network);
                    resources.Add(resourceTypeSource);
                }
            }
            while (reader.IsStartElement() || reader.Name != "ConstructionInformation");
            var constructionInformation = new ConstructionInformation(constructionTime, environmentResource, resources.ToArray());
            return constructionInformation;
        }

        private static ProductionInformation LoadProductionInformation(XmlReader reader)
        {
            float productionTime = Convert.ToSingle(reader[nameof(ProductionInformation.ProductionTime)]);
            List<ResourceTypeSource> ingredients = new List<ResourceTypeSource>();
            List<ResourceTypeSource> products = new List<ResourceTypeSource>();
            EnvironmentResource ingredientEnvironmentResource = new EnvironmentResource();
            EnvironmentResource productsEnvironmentResource = new EnvironmentResource();
            do
            {
                reader.Read();

                if (reader.IsStartElement(nameof(ProductionInformation.Ingredients)))
                {
                    reader.Read();
                    reader.MoveToContent();
                    do
                    {
                        if (reader.Name == nameof(EnvironmentResource))
                        {
                            int co2 = Convert.ToInt32(reader["CO2"] ?? "0");
                            int o2 = Convert.ToInt32(reader["O2"] ?? "0");
                            int energy = Convert.ToInt32(reader["Energy"] ?? "0");
                            ingredientEnvironmentResource = new EnvironmentResource(co2, o2, energy);
                        }
                        else
                        {
                            var resourceName = reader.Name;
                            bool fromHexagon = reader["Source"] == "Hexagon";
                            var resourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resourceName);
                            var resourceTypeSource = new ResourceTypeSource(resourceType, fromHexagon ? SourceType.Hexagon : SourceType.Network);
                            ingredients.Add(resourceTypeSource);
                        }
                        reader.Read();
                        reader.MoveToContent();
                    }
                    while (reader.IsStartElement() || reader.Name != nameof(ProductionInformation.Ingredients));
                }
                else if (reader.IsStartElement(nameof(ProductionInformation.Products)))
                {
                    reader.Read();
                    reader.MoveToContent();
                    do
                    {
                        if (reader.Name == nameof(EnvironmentResource))
                        {
                            int co2 = Convert.ToInt32(reader["CO2"] ?? "0");
                            int o2 = Convert.ToInt32(reader["O2"] ?? "0");
                            int energy = Convert.ToInt32(reader["Energy"] ?? "0");
                            productsEnvironmentResource = new EnvironmentResource(co2, o2, energy);
                        }
                        else
                        {
                            var resourceName = reader.Name;
                            bool fromHexagon = Convert.ToBoolean(reader["FromHexagon"] ?? "False", CultureInfo.InvariantCulture);
                            var resourceType = (ResourceType)Enum.Parse(typeof(ResourceType), resourceName);
                            var resourceTypeSource = new ResourceTypeSource(resourceType, fromHexagon ? SourceType.Hexagon : SourceType.Network);
                            products.Add(resourceTypeSource);
                        }
                        reader.Read();
                        reader.MoveToContent();
                    }
                    while (reader.IsStartElement() || reader.Name != nameof(ProductionInformation.Products));
                }
            }
            while (reader.IsStartElement() || reader.Name != nameof(ProductionInformation));
            var productionInformation = new ProductionInformation(new IngredientsCollection(ingredientEnvironmentResource, ingredients.ToArray()), new ProductsCollection(productsEnvironmentResource, products.ToArray()), productionTime);
            return productionInformation;
        }

        private static Knowledge LoadUnlockCost(XmlReader reader)
        {
            var k1 = Convert.ToInt32(reader["Knowledge1"] ?? "0");
            var k2 = Convert.ToInt32(reader["Knowledge2"] ?? "0");
            var k3 = Convert.ToInt32(reader["Knowledge3"] ?? "0");

            return new Knowledge(k1, k2, k3);
        }

        private static IEnumerable<BuildingDescription> ReadBuildingsFromXml(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.IsStartElement("xml"))
                    continue;
                if (!reader.IsStartElement())
                    continue;
                if (reader.IsStartElement("Buildings"))
                    break;
            }
            while (reader.Read())
            {
                if (reader.IsStartElement("Building"))
                {
                    yield return LoadBuilding(reader);
                }
            }
        }
    }
}