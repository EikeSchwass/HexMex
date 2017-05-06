using CocosSharp;
using HexMex.Helper;
using HexMex.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HexMex.Scenes
{
    public class HexMexScene : CCScene
    {
        public HexMexScene(CCWindow window, DataLoader dataLoader) : base(window)
        {
            DataLoader = dataLoader;
            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (Window.DefaultDirector.CanPopScene)
                    Window.DefaultDirector.PopScene();
                else
                    ((AppDelegate)Window.Application.ApplicationDelegate).AppSuspender.Suspend();
            }
        }

        public DataLoader DataLoader { get; }
    }
}


