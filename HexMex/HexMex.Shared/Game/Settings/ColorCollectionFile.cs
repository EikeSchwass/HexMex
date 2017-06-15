using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
using CocosSharp;

namespace HexMex.Game.Settings
{
    public class ColorCollectionFile
    {
        public string NameKey { get; }
        public string DescriptionKey { get; }

        public CCColor4B this[string colorKey] => ColorEntries[colorKey];
        public float InnerHexagonBlendIntensity { get; }
        public float OuterHexagonBlendIntensity { get; }
        private Dictionary<string, CCColor4B> ColorEntries { get; } = new Dictionary<string, CCColor4B>();

        public ColorCollectionFile(string nameKey, string descriptionKey, float innerHexagonBlendIntensity, float outerHexagonBlendIntensity, IEnumerable<ColorEntry> colorEntries)
        {
            NameKey = nameKey;
            DescriptionKey = descriptionKey;
            InnerHexagonBlendIntensity = innerHexagonBlendIntensity;
            OuterHexagonBlendIntensity = outerHexagonBlendIntensity;
            foreach (var colorEntry in colorEntries)
            {
                ColorEntries.Add(colorEntry.Key, colorEntry.Color);
            }
        }
        public static ColorCollectionFile CreateFromXml(string colorXml)
        {
            using (var reader = XmlReader.Create(new StringReader(colorXml)))
            {
                while (reader.Read())
                {
                    if (!reader.IsStartElement())
                        continue;
                    if (reader.IsStartElement("xml"))
                        continue;
                    if (reader.IsStartElement("ColorCollection"))
                        break;
                }

                var nameKey = reader.GetAttribute("NameKey") ?? "empty";
                var descriptionKey = reader.GetAttribute("DescriptionKey") ?? "empty";
                var innterHexagonBlendIntensity = Convert.ToSingle(reader.GetAttribute("InnerHexagonBlendIntensity") ?? "0", NumberFormatInfo.InvariantInfo);
                var outerHexagonBlendIntensity = Convert.ToSingle(reader.GetAttribute("OuterHexagonBlendIntensity") ?? "0", NumberFormatInfo.InvariantInfo);
                List<ColorEntry> colorEntries = new List<ColorEntry>();
                while (reader.Read())
                {
                    if (reader.IsStartElement("Color"))
                    {
                        var colorKey = reader.GetAttribute("ColorKey");
                        var colorString = reader.GetAttribute("Value");
                        var color = ParseColor(colorString);
                        var colorEntry = new ColorEntry(colorKey, color);
                        colorEntries.Add(colorEntry);
                    }
                }
                var colorCollectionFile = new ColorCollectionFile(nameKey, descriptionKey, innterHexagonBlendIntensity, outerHexagonBlendIntensity, colorEntries);
                return colorCollectionFile;
            }
        }
        private static CCColor4B ParseColor(string colorString)
        {
            int alpha = 255;
            int index = -2;
            if (colorString.Length == 8)
            {
                alpha = Parse(colorString.Substring(0, 2));
                index = 0;
            }
            var r = Parse(colorString.Substring(index += 2, 2));
            var g = Parse(colorString.Substring(index += 2, 2));
            var b = Parse(colorString.Substring(index += 2, 2));

            int Parse(string hexNumber)
            {
                return Convert.ToInt32(hexNumber, 16);
            }

            return new CCColor4B(r, g, b, alpha);
        }
    }
}