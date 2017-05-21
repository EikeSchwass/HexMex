using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public class BuildingConstructionFactory
    {
        public static IReadOnlyDictionary<Type, BuildingConstructionFactory> Factories { get; }

        public Func<HexagonNode, World, Structure> CreateFunction { get; }

        public StructureDescription StructureDescription { get; }
        private Type Type { get; }

        static BuildingConstructionFactory()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var factories = from type in types
                            where !type.IsAbstract
                            where !type.IsAutoClass
                            where type.IsSubclassOf(typeof(Building))
                            let description = (StructureDescription)type.GetProperty(nameof(StructureDescription), BindingFlags.Static | BindingFlags.Public)?.GetValue(null)
                            select new BuildingConstructionFactory(type, description, (pos, world) => (Structure)Activator.CreateInstance(type, pos, world));
            Factories = new ReadOnlyDictionary<Type, BuildingConstructionFactory>(factories.ToDictionary(f => f.Type));
        }

        private BuildingConstructionFactory(Type type, StructureDescription structureDescription, Func<HexagonNode, World, Structure> createFunction)
        {
            Type = type;
            CreateFunction = createFunction;
            StructureDescription = structureDescription;
        }
    }
}