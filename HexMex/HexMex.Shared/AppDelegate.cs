using System;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;
using HexMex.Scenes.Game;
using HexMex.Scenes.MainMenu;

namespace HexMex.Shared
{
    public class AppDelegate : CCApplicationDelegate
    {
        public ICanSuspendApp AppSuspender { get; }
        public ColorCollectionFile ColorCollectionFile { get; }
        public LanguageSettings LanguageSettings { get; }
        private BuildingDescriptionDatabase BuildingDescriptionDatabase { get; }

        private DataLoader DataLoader { get; }

        public AppDelegate(DataLoader dataLoader, ICanSuspendApp appSuspender, BuildingDescriptionDatabase buildingDescriptionDatabase, ColorCollectionFile colorCollectionFile, LanguageSettings languageSettings)
        {
            if (DataLoader != null)
                throw new InvalidOperationException("Only one AppDelegate can be instantiated");
            DataLoader = dataLoader;
            AppSuspender = appSuspender;
            BuildingDescriptionDatabase = buildingDescriptionDatabase;
            ColorCollectionFile = colorCollectionFile;
            LanguageSettings = languageSettings;
        }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            application.Paused = true;
        }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            application.ContentRootDirectory = "Content";
            application.ContentSearchPaths.Add("config");

            // This will set the world bounds to be (0,0, w, h)
            // CCSceneResolutionPolicy.ShowAll will ensure that the aspect ratio is preserved
            var width = application.MainWindow.WindowSizeInPixels.Width;
            var height = application.MainWindow.WindowSizeInPixels.Height;

            var defaultWidth = 1080;
            var defaultHeight = 1920;

            var scaleFactor = width * height / (defaultWidth * defaultHeight);
            Font.FontScaleFactor = scaleFactor;

            CCScene.SetDefaultDesignResolution(width, height, CCSceneResolutionPolicy.ShowAll);

            // Determine whether to use the high or low def versions of our images
            // Make sure the default texel to content size ratio is set correctly
            // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)

            var scene = new MainMenuScene(mainWindow, BuildingDescriptionDatabase, ColorCollectionFile, LanguageSettings);
            mainWindow.RunWithScene(scene);
            mainWindow.DisplayStats = true;
            mainWindow.StatsScale = 5;
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            application.Paused = false;
        }
    }
}