using System;
using CocosSharp;
using HexMex.Helper;
using HexMex.Scenes.MainMenu;

namespace HexMex.Shared
{
    public class AppDelegate : CCApplicationDelegate
    {
        public AppDelegate(DataLoader dataLoader, ICanSuspendApp appSuspender)
        {
            if (DataLoader != null)
                throw new InvalidOperationException("Only one AppDelegate can be instantiated");
            DataLoader = dataLoader;
            AppSuspender = appSuspender;
        }

        private DataLoader DataLoader { get; }
        public ICanSuspendApp AppSuspender { get; }

        public override void ApplicationDidEnterBackground(CCApplication application)
        {
            application.Paused = true;
        }

        public override void ApplicationDidFinishLaunching(CCApplication application, CCWindow mainWindow)
        {
            application.ContentRootDirectory = "Content";
            application.ContentSearchPaths.Add("config");
            var windowSize = mainWindow.WindowSizeInPixels;

            var desiredWidth = 768;
            var desiredHeight = 1024;

            // This will set the world bounds to be (0,0, w, h)
            // CCSceneResolutionPolicy.ShowAll will ensure that the aspect ratio is preserved
            CCScene.SetDefaultDesignResolution(desiredWidth, desiredHeight, CCSceneResolutionPolicy.NoBorder);

            // Determine whether to use the high or low def versions of our images
            // Make sure the default texel to content size ratio is set correctly
            // Of course you're free to have a finer set of image resolutions e.g (ld, hd, super-hd)
            if (desiredWidth < windowSize.Width)
            {
                application.ContentSearchPaths.Add("hd");
                CCSprite.DefaultTexelToContentSizeRatio = 2.0f;
            }
            else
            {
                application.ContentSearchPaths.Add("ld");
                CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
            }
            var scene = new MainMenuScene(mainWindow);
            mainWindow.RunWithScene(scene);
        }

        public override void ApplicationWillEnterForeground(CCApplication application)
        {
            application.Paused = false;
        }
    }
}