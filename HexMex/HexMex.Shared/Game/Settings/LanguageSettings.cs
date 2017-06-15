using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HexMex.Droid;

namespace HexMex.Game.Settings
{
    public class LanguageSettings
    {
        private static List<string> MissingTranslations { get; } = new List<string>();
        private static string MissingString => string.Join(Environment.NewLine, MissingTranslations);

        private LanguageDefintion CurrentLanguage { get; }
        private ReadOnlyCollection<LanguageDefintion> LanguageDefinitions { get; }

        public LanguageSettings(IList<LanguageDefintion> languageDefintions)
        {
            CurrentLanguage = languageDefintions.First();
            LanguageDefinitions = new ReadOnlyCollection<LanguageDefintion>(languageDefintions);
        }

        public string GetByKey(TranslationKey key)
        {
            if (CurrentLanguage.Translations.ContainsKey(key))
                return CurrentLanguage.Translations[key].Value;
            var s = $"<Translation Key=\"{key}\">{Environment.NewLine}{Environment.NewLine}</Translation>";
            if (!MissingTranslations.Contains(s))
            {
                MissingTranslations.Add(s);
                Debug.WriteLine(MissingString);
            }
            return "missing";
        }
    }
}