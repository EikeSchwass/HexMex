using CocosSharp;
using HexMex.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HexMex.Scenes
{
    public class HexMexScene : CCScene
    {
        public HexMexScene(CCWindow window) : base(window)
        {
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
    }
}


