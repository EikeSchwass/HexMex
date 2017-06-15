using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using static System.Math;
using static HexMex.Game.ResourceType;

namespace HexMex.Helper
{
    public static class ExtensionMethods
    {
        public static bool CanBeUsedFor(this ResourceType actualType, ResourceType requestedType)
        {
            return (actualType & requestedType) == requestedType;
        }
        public static void DrawCircle(this CCDrawNode node, CCPoint position, float radius, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            node.DrawSolidCircle(position, radius, borderColor);
            node.DrawSolidCircle(position, radius - borderThickness, fillColor);
        }

        public static void DrawNumber(this CCDrawNode node, int number, CCPoint position, float height, float thickness, CCColor4F color)
        {
            int[] blops = new int[(number - 1) / 5 + 1];
            for (int i = 0; i < blops.Length; i++)
            {
                blops[i] = 5;
            }
            blops[blops.Length - 1] = (number - 1) % 5 + 1;

            float margin = height / 10;
            float blopMargin = margin * 4;
            float blopWidth = thickness * 4 + margin * 3;
            float totalWidth = (blops.Length - 1) * (blopWidth + blopMargin) + blops.Last() * thickness + (blops.Last() - 1) * margin;

            for (int i = 0; i < blops.Length; i++)
            {
                for (int j = 0; j < Min(blops[i], 4); j++)
                {
                    float x = i * (blopWidth + blopMargin);
                    x += j * (thickness + margin);
                    x += thickness / 2;
                    x += position.X;
                    x -= totalWidth / 2;
                    float y1 = position.Y + height / 2 + thickness / 2;
                    float y2 = position.Y - height / 2 + thickness / 2;
                    var from = new CCPoint(x, y1);
                    var to = new CCPoint(x, y2);
                    node.DrawSegment(from, to, thickness / 2, color);
                }
                if (blops[i] == 5)
                {
                    float x1 = i * (blopWidth + blopMargin);
                    x1 += position.X;
                    x1 -= totalWidth / 2;
                    float x2 = x1 + blopWidth;
                    float y1 = position.Y - height / 2;
                    float y2 = position.Y + height / 2;
                    var p1 = new CCPoint(x1, y1);
                    var p2 = new CCPoint(x2, y2);
                    node.DrawSegment(p1, p2, thickness / 2, color);
                }
            }
        }
        public static ResourceTypeSource FromHexagon(this ResourceType resourceType) => new ResourceTypeSource(resourceType, SourceType.Hexagon);

        public static ResourceTypeSource FromNetwork(this ResourceType resourceType) => new ResourceTypeSource(resourceType, SourceType.Network);

        public static CCColor4B GetColor(this ResourceType type, ColorCollection colorCollection)
        {
            switch (type)
            {
                case None: return colorCollection.ResourceNone;
                case PureWater: return colorCollection.ResourcePureWater;
                case Tree: return colorCollection.ResourceTree;
                case Stone: return colorCollection.ResourceStone;
                case CoalOre: return colorCollection.ResourceCoalOre;
                case CopperOre: return colorCollection.ResourceCopperOre;
                case IronOre: return colorCollection.ResourceIronOre;
                case GoldOre: return colorCollection.ResourceGoldOre;
                case DiamondOre: return colorCollection.ResourceDiamondOre;
                case Gold: return colorCollection.ResourceGold;
                case Copper: return colorCollection.ResourceCopper;
                case Iron: return colorCollection.ResourceIron;
                case Wood: return colorCollection.ResourceWood;
                case Coal: return colorCollection.ResourceCoal;
                case Sand: return colorCollection.ResourceSand;
                case Brick: return colorCollection.ResourceBrick;
                case Paper: return colorCollection.ResourcePaper;
                case Circuit: return colorCollection.ResourceCircuit;
                case Tools: return colorCollection.ResourceTools;
                case Barrel: return colorCollection.ResourceBarrel;
                case Pottasche: return colorCollection.ResourcePottasche;
                case Glas: return colorCollection.ResourceGlas;
                case WaterBarrel: return colorCollection.ResourceWaterBarrel;
                case Knowledge1: return colorCollection.ResourceKnowledge1;
                case Knowledge2: return colorCollection.ResourceKnowledge2;
                case Knowledge3: return colorCollection.ResourceKnowledge3;
                case Energy: return colorCollection.ResourceEnergy;
                case Water: return colorCollection.ResourceWater;
                case Degradeable: return colorCollection.ResourceDegradeable;
                case Diamond: return colorCollection.ResourceDiamond;
                case Anything: return colorCollection.ResourceAnything;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
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

        public static string GetText(this ResourceCollection source)
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

        public static bool IsPassable(this ResourceType type)
        {
            if (type == PureWater)
                return false;
            return true;
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

        public static CCColor4F ToColor4F(this CCColor4B color) => new CCColor4F(color);

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