using System.Collections.Generic;
using System.IO;
using System.Xml;
using Android.App;
using Android.Content.PM;
using Android.OS;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Shared;
using HexMex.UnitTests;
using Microsoft.Xna.Framework;

namespace HexMex.Droid
{
    [Activity(Label = "HexMex.Droid", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Splash", AlwaysRetainTaskState = true, LaunchMode = LaunchMode.SingleInstance, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden)]
    public class Program : AndroidGameActivity, ICanSuspendApp
    {
        public void Suspend()
        {
            MoveTaskToBack(true);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            CCLog.Logger = (e, a) => { };
            System.Diagnostics.Debug.WriteLine("");

#if DEBUG
            Tests.RunTests();
#endif

            string buildingXml;
            string colorXml;
            string languageXml;

            using (var colorsStream = Assets.Open("colors.xml"))
            {
                using (var sr = new StreamReader(colorsStream))
                {
                    colorXml = sr.ReadToEnd();
                }
            }

            using (var buildingStream = Assets.Open("buildings.xml"))
            {
                using (var sr = new StreamReader(buildingStream))
                {
                    buildingXml = sr.ReadToEnd();
                }
            }

            using (var languageStream = Assets.Open("languages.xml"))
            {
                using (var sr = new StreamReader(languageStream))
                {
                    languageXml = sr.ReadToEnd();
                }
            }

            var buildingDescriptionDatabase = BuildingDescriptionDatabase.CreateFromXml(buildingXml);
            var colorCollectionFile = ColorCollectionFile.CreateFromXml(colorXml);
            var languageSettings = LoadLanguagesFromXml(languageXml);
            CCApplication application = new CCApplication {ApplicationDelegate = new AppDelegate(new AndroidDataLoader(), this, buildingDescriptionDatabase, colorCollectionFile, languageSettings)};

            SetContentView(application.AndroidContentView);
            application.StartGame();
        }
        private LanguageDefintion LoadLanguageDefinition(XmlReader reader)
        {
            var name = reader.GetAttribute("Name");
            List<Translation> translations = new List<Translation>();
            do
            {
                reader.Read();
                if (reader.IsStartElement("Translation"))
                {
                    var translationKey = new TranslationKey(reader.GetAttribute("Key"));
                    reader.Read();
                    reader.MoveToContent();
                    var translation = new Translation(translationKey, reader.Value.Trim());
                    translations.Add(translation);
                }
            }
            while (reader.IsStartElement() || reader.Name != "Language");
            return new LanguageDefintion(name, translations);
        }

        private LanguageSettings LoadLanguagesFromXml(string languageXml)
        {
            using (var reader = XmlReader.Create(new StringReader(languageXml)))
            {
                List<LanguageDefintion> languageDefintions = new List<LanguageDefintion>();
                while (reader.Read())
                {
                    bool breakOut = false;
                    while (!reader.IsStartElement("Language"))
                    {
                        if (!reader.Read())
                        {
                            breakOut = true;
                            break;
                        }
                    }
                    if (breakOut)
                        break;
                    var languageDefinition = LoadLanguageDefinition(reader);
                    languageDefintions.Add(languageDefinition);
                }
                var languageSettings = new LanguageSettings(languageDefintions);
                return languageSettings;
            }
        }
    }
}