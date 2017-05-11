using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace HexMex.Game.Buildings
{
    public class BuildingConstructionFactory
    {
        static BuildingConstructionFactory()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var factories = from type in types
                            where !type.IsAbstract
                            where !type.IsAutoClass
                            where type.IsSubclassOf(typeof(Structure))
                            let description = type.GetCustomAttribute<BuildingInformationAttribute>()
                            select new BuildingConstructionFactory(type, description, (pos, res, hex) => (Structure)Activator.CreateInstance(type, pos, res, hex));
            Factories = new ReadOnlyDictionary<Type, BuildingConstructionFactory>(factories.ToDictionary(f => f.Type));
        }

        private BuildingConstructionFactory(Type type, BuildingInformationAttribute buildingInformation, Func<HexagonNode, ResourceManager, HexagonManager, Structure> createFunction)
        {
            Type = type;
            CreateFunction = createFunction;
            BuildingInformation = buildingInformation;
        }

        public BuildingInformationAttribute BuildingInformation { get; }

        public static IReadOnlyDictionary<Type, BuildingConstructionFactory> Factories { get; }

        public Func<HexagonNode, ResourceManager, HexagonManager, Structure> CreateFunction { get; }
        private Type Type { get; }
    }
}