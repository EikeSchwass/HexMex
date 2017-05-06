using System;
using CocosSharp;
using HexMex.Game;

namespace HexMex.Helper
{
    public static class ResourceTypeExtensions
    {
        public static bool DoesResourceTypeSatisfyRequest(this ResourceType actualType, ResourceType requestedType)
        {
            return (actualType & requestedType) == requestedType;
        }

        public static CCColor4B GetColor(this ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Workforce:
                    return CCColor4B.Yellow;
                case ResourceType.Water:
                    return CCColor4B.Blue;
                case ResourceType.Meat:
                    return CCColor4B.Orange;
                case ResourceType.Bread:
                    return CCColor4B.Gray;
                case ResourceType.Nutrition:
                    return CCColor4B.White;
                case ResourceType.Platinum:
                    return CCColor4B.Green;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}