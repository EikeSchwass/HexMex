using Android.App;
using Android.Content.PM;
using Android.OS;
using CocosSharp;
using HexMex.Shared;
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

            CCApplication application = new CCApplication
            {
                ApplicationDelegate = new AppDelegate(new AndroidDataLoader(), this)
            };

            SetContentView(application.AndroidContentView);
            application.StartGame();
        }
    }
}