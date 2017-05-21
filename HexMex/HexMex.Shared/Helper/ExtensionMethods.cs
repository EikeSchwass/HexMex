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

        public static CCColor4B GetColor(this ResourceType type, ColorCollection colorCollection)
        {
            switch (type)
            {
                case None: return CCColor4B.Lerp(colorCollection.GrayNormal, CCColor4B.Transparent, 0.5f);
                case PureWater: return colorCollection.BlueLight;
                case Tree: return colorCollection.GreenDark;
                case Stone: return colorCollection.GrayNormal;
                case CoalOre: return CCColor4B.Lerp(Coal.GetColor(colorCollection), colorCollection.GrayNormal, 0.5f);
                case CopperOre: return CCColor4B.Lerp(Copper.GetColor(colorCollection), colorCollection.GrayNormal, 0.5f);
                case IronOre: return CCColor4B.Lerp(Iron.GetColor(colorCollection), colorCollection.GrayNormal, 0.5f);
                case GoldOre: return CCColor4B.Lerp(Gold.GetColor(colorCollection), colorCollection.GrayNormal, 0.5f);
                case DiamondOre: return CCColor4B.Lerp(Diamond.GetColor(colorCollection), colorCollection.GrayNormal, 0.5f);
                case Gold: return colorCollection.YellowNormal;
                case Copper: return colorCollection.RedDark;
                case Iron: return colorCollection.BlueVeryLight;
                case Wood: return colorCollection.RedVeryDark;
                case Coal: return colorCollection.GrayVeryDark;
                case Sand: return CCColor4B.Lerp(colorCollection.YellowNormal, colorCollection.RedNormal, 0.5f);
                case Brick: return colorCollection.RedNormal;
                case Paper: return colorCollection.White;
                case Circuit: return colorCollection.GreenNormal;
                case Tools: return colorCollection.GrayLight;
                case Barrel: return colorCollection.BlueVeryDark;
                case Pottasche: return colorCollection.GrayDark;
                case Glas: return CCColor4B.Lerp(colorCollection.GrayVeryLight, CCColor4B.Transparent, 0.5f);
                case WaterBarrel: return colorCollection.BlueDark;
                case Knowledge: return colorCollection.RedLight;
                case Energy: return colorCollection.YellowLight;
                case Water: return colorCollection.BlueNormal;
                case Degradeable: return colorCollection.GrayVeryLight;
                case Diamond: return colorCollection.YellowVeryLight;
                default: throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public static CCColor4F ToColor4F(this CCColor4B color) => new CCColor4F(color);

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
            if (type == PureWater)
                return false;
            return true;
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