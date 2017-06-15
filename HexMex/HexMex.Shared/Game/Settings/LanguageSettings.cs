using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HexMex.Droid;

namespace HexMex.Game.Settings
{
    public class LanguageSettings
    {
        private LanguageDefintion CurrentLanguage { get; }
        private ReadOnlyCollection<LanguageDefintion> LanguageDefinitions { get; }

        public LanguageSettings(IList<LanguageDefintion> languageDefintions)
        {
            CurrentLanguage = languageDefintions.First();
            LanguageDefinitions = new ReadOnlyCollection<LanguageDefintion>(languageDefintions);
        }

        public string GetByKey(TranslationKey key) => CurrentLanguage.Translations[key].Value;
    }
}