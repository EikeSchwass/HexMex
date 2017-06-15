using HexMex.Game;

namespace HexMex.Droid
{
    public struct Translation
    {
        public TranslationKey Key { get; }
        public string Value { get; }

        public Translation(TranslationKey key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}