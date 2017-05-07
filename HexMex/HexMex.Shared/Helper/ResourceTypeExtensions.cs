using System;
using CocosSharp;
using HexMex.Game;

namespace HexMex.Helper
{
    public static class ResourceTypeExtensions
    {
        public static bool CanBeUsedFor(this ResourceType actualType, ResourceType requestedType)
        {
            return (actualType & requestedType) == requestedType;
        }

        public static CCColor4B GetColor(this ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Workforce: return CCColor4B.Yellow;
                case ResourceType.Water: return CCColor4B.Blue;
                case ResourceType.Meat: return CCColor4B.Orange;
                case ResourceType.Bread: return CCColor4B.Gray;
                case ResourceType.Nutrition: return CCColor4B.White;
                case ResourceType.Any: return CCColor4B.Green;
                case ResourceType.Nothing: return CCColor4B.Aquamarine;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static bool HasBorder(this ResourceType type)
        {
            return true;// type != ResourceType.Water;
        }

        public static bool IsPassable(this ResourceType type)
        {
            if (type == ResourceType.Water)
                return false;
            return true;
        }
    }
}