using System;
using System.Collections.Generic;
using System.Linq;
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
                case ResourceType.None:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.PureWater:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Tree:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Stone:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.CoalOre:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.CopperOre:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.IronOre:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.GoldOre:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Diamonds:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Gold:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Copper:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Iron:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Wood:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Coal:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Sand:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Brick:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Paper:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Circuit:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Tools:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Barrel:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Pottasche:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Glas:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.WaterBarrel:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Knowledge:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Energy:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Water:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Degradeable:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Any:
                    return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
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

        public static string GetText(this IEnumerable<ResourceType> source)
        {
            string result = "";
            var groups = source.GroupBy(e => e);
            foreach (var group in groups)
            {
                var count = group.Count();
                result += $"{group.Key} {(count > 1 ? "x" + count : "")}, ";
            }
            if (result.Length < 3)
                return result;
            return result.Substring(0, result.Length - 2);

        }
    }
}