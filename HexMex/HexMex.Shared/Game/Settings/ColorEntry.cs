using CocosSharp;

namespace HexMex.Game.Settings {
    public struct ColorEntry
    {
        public string Key { get; }
        public CCColor4B Color { get; }

        public ColorEntry(string key, CCColor4B color)
        {
            Key = key;
            Color = color;
        }
    }
}