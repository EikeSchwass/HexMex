using HexMex.Game.Settings;

namespace HexMex.Game
{
    public struct TranslationKey
    {
        private string Key { get; }
        public TranslationKey(string key)
        {
            Key = key;
        }

        public string Translate(LanguageSettings languageSettings)
        {
            return languageSettings.GetByKey(this);
        }

        public bool Equals(TranslationKey other)
        {
            return string.Equals(Key, other.Key);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is TranslationKey && Equals((TranslationKey)obj);
        }
        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode() : 0);
        }
        public static bool operator ==(TranslationKey left, TranslationKey right)
        {
            return left.Equals(right);
        }
        public static bool operator !=(TranslationKey left, TranslationKey right)
        {
            return !left.Equals(right);
        }
    }
}