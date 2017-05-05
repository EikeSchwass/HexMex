using System;
using System.Collections.Generic;
using HexMex.Game;

namespace HexMex.Helper
{
    public static class TypeExtensions
    {
        private static Dictionary<Type, Resource> Resources { get; } = new Dictionary<Type, Resource>();

        public static Resource GetResource(this Type type)
        {
            if (!type.IsAssignableFrom(typeof(Resource)))
                throw new ArgumentException("The type doesn't belong to a Resource");
            if (!Resources.ContainsKey(type))
                Resources.Add(type, CreateResourceFromType(type));
            return Resources[type];
        }

        private static Resource CreateResourceFromType(Type type)
        {
            var resource = (Resource)Activator.CreateInstance(type);
            return resource;
        }
    }
}