using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Helper
{
    public static class ExtensionMethods
    {
        public static bool CanBeUsedFor(this ResourceType actualType, ResourceType requestedType)
        {
            return (actualType & requestedType) == requestedType;
        }

        public static CCColor4B GetColor(this ResourceType type)
        {
            switch (type)
            {
                case ResourceType.None: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.PureWater: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Tree: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Stone: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.CoalOre: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.CopperOre: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.IronOre: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.GoldOre: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.DiamondOre: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Gold: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Copper: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Iron: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Wood: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Coal: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Sand: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Brick: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Paper: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Circuit: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Tools: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Barrel: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Pottasche: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Glas: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.WaterBarrel: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Knowledge: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Energy: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Water: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Degradeable: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                case ResourceType.Diamond: return new CCColor4B(0.25f, 0.25f, 0.25f, 0.25f);
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static string GetText(this StructureDescription.ResourceCollection source)
        {
            string result = "";
            var groups = source.ResourceTypes.GroupBy(e => e);
            foreach (var group in groups)
            {
                var count = group.Count();
                result += $"{group.Key} {(count > 1 ? "x" + count : "")}, ";
            }
            if (result.Length < 3)
                return result;
            return result.Substring(0, result.Length - 2);
        }

        public static bool HasBorder(this ResourceType type)
        {
            return true; // type != ResourceType.Water;
        }

        public static bool IsPassable(this ResourceType type)
        {
            if (type == ResourceType.Water)
                return false;
            return true;
        }
        public static void DrawCircle(this CCDrawNode node, CCPoint position, float radius, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            node.DrawSolidCircle(position, radius, borderColor);
            node.DrawSolidCircle(position, radius - borderThickness, fillColor);
        }

        public static CCPoint GetGlobalPosition(this CCNode node)
        {
            CCPoint position = node.Position;
            while ((node = node.Parent) != null)
            {
                position += node.Position;
            }
            return position;
        }
        public static CCPoint RotateAround(this CCPoint point, CCPoint centerOfRotation, float angleInDegrees)
        {
            double angle = angleInDegrees / 360 * (2 * PI);
            var sin = Sin(angle);
            var cos = Cos(angle);
            float xOld = point.X - centerOfRotation.X;
            float yOld = point.Y - centerOfRotation.Y;
            float xNew = (float)(xOld * cos - yOld * sin);
            float yNew = (float)(yOld * cos + xOld * sin);
            xNew += centerOfRotation.X;
            yNew += centerOfRotation.Y;
            return new CCPoint(xNew, yNew);
        }

        public static CCPoint[] RotateAround(this CCPoint[] points, CCPoint centerOfRotation, float angleInDegrees)
        {
            var result = new CCPoint[points.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = points[i].RotateAround(centerOfRotation, angleInDegrees);
            }
            return result;
        }

        public static T GetElementAfter<T>(this T[] source, T element)
        {
            for (int i = 0; i < source.Length - 1; i++)
            {
                if (Equals(source[i], element))
                    return source[i + 1];
            }
            throw new ArgumentException("No element found/element is last entry", nameof(element));
        }

        public static int IndexOf<T>(this IEnumerable<T> source, T element)
        {
            var s = source.ToArray();
            for (int i = 0; i < s.Length; i++)
            {
                if (Equals(s[i], element))
                    return i;
            }
            return -1;
        }

        public static IEnumerable<TKey> Unique<TElement, TKey>(this IEnumerable<TElement> source, Func<TElement, TKey> selector)
        {
            var counter = new Dictionary<TKey, int>();
            foreach (var element in source)
            {
                var key = selector(element);
                if (counter.ContainsKey(key))
                    counter[key]++;
                else
                    counter.Add(key, 1);
            }
            foreach (var kvp in counter)
            {
                if (kvp.Value == 1)
                    yield return kvp.Key;
            }
        }
    }
}