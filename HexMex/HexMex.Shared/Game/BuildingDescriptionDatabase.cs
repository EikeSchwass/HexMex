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


        public BuildingDescription ByNameKey(string nameKey) => BuildingDescriptions.FirstOrDefault(b => Equals(b.VerbalStructureDescription.NameID, new TranslationKey(nameKey)));

        private static BuildingDescription LoadBuilding(XmlReader reader)
        {
            var name = reader.GetAttribute("Name") ?? "empty";
            var canExtractWater = Convert.ToBoolean(reader["CanExtractWater"] ?? "False");
            name = name[0].ToString().ToLower() + name.Substring(1);
            var nameID = name + "Name";
            var descriptionID = name + "Description";
            var verbalStructureDescription = new VerbalStructureDescription(name, new TranslationKey(nameID), new TranslationKey(descriptionID));
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
            float constructionTime = Convert.ToSingle(reader["ConstructionTime"], CultureInfo.InvariantCulture);
            List<ResourceTypeSource> resources = new List<ResourceTypeSource>();
            EnvironmentResource environmentResource = new EnvironmentResource();
            do
            {
                reader.Read();
                reader.MoveToContent();
                if (reader.Name == nameof(EnvironmentResource))
                {
                    int co2 = Convert.ToInt32(reader["CO2"] ?? "0", CultureInfo.InvariantCulture);
                    int o2 = Convert.ToInt32(reader["O2"] ?? "0", CultureInfo.InvariantCulture);
                    int energy = Convert.ToInt32(reader["Energy"] ?? "0", CultureInfo.InvariantCulture);
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
            float productionTime = Convert.ToSingle(reader[nameof(ProductionInformation.ProductionTime)], CultureInfo.InvariantCulture);
            List<ResourceTypeSource> ingredients = new List<ResourceTypeSource>();
            List<ResourceTypeSource> products = new List<ResourceTypeSource>();
            EnvironmentResource ingredientEnvironmentResource = new EnvironmentResource();
            EnvironmentResource productsEnvironmentResource = new EnvironmentResource();
            Knowledge producedKnowledge = Knowledge.Zero;
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
                            int co2 = Convert.ToInt32(reader["CO2"] ?? "0", CultureInfo.InvariantCulture);
                            int o2 = Convert.ToInt32(reader["O2"] ?? "0", CultureInfo.InvariantCulture);
                            int energy = Convert.ToInt32(reader["Energy"] ?? "0", CultureInfo.InvariantCulture);
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
                            int co2 = Convert.ToInt32(reader["CO2"] ?? "0", CultureInfo.InvariantCulture);
                            int o2 = Convert.ToInt32(reader["O2"] ?? "0", CultureInfo.InvariantCulture);
                            int energy = Convert.ToInt32(reader["Energy"] ?? "0", CultureInfo.InvariantCulture);
                            productsEnvironmentResource = new EnvironmentResource(co2, o2, energy);
                        }
                        else if (reader.Name == nameof(Knowledge))
                        {
                            int k1 = Convert.ToInt32(reader[nameof(Knowledge.Knowledge1)] ?? "0", CultureInfo.InvariantCulture);
                            int k2 = Convert.ToInt32(reader[nameof(Knowledge.Knowledge2)] ?? "0", CultureInfo.InvariantCulture);
                            int k3 = Convert.ToInt32(reader[nameof(Knowledge.Knowledge3)] ?? "0", CultureInfo.InvariantCulture);
                            producedKnowledge = new Knowledge(k1, k2, k3);
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
            var productionInformation = new ProductionInformation(new IngredientsCollection(ingredientEnvironmentResource, ingredients.ToArray()), new ProductsCollection(productsEnvironmentResource, producedKnowledge, products.ToArray()), productionTime);
            return productionInformation;
        }

        private static Knowledge LoadUnlockCost(XmlReader reader)
        {
            var k1 = Convert.ToInt32(reader["Knowledge1"] ?? "0", CultureInfo.InvariantCulture);
            var k2 = Convert.ToInt32(reader["Knowledge2"] ?? "0", CultureInfo.InvariantCulture);
            var k3 = Convert.ToInt32(reader["Knowledge3"] ?? "0", CultureInfo.InvariantCulture);

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