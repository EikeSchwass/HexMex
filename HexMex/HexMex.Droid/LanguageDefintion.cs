using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexMex.Game;

namespace HexMex.Droid
{
    public struct LanguageDefintion
    {
        public string Name { get; }
        public ReadOnlyDictionary<TranslationKey, Translation> Translations { get; }

        public LanguageDefintion(string name, IList<Translation> translations)
        {
            Name = name;
            Translations = new ReadOnlyDictionary<TranslationKey, Translation>(translations.ToDictionary(t => t.Key));
        }
    }
}