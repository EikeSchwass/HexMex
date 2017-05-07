using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class ControlLayer : CCLayer
    {
        public ControlLayer(World world)
        {
            World = world;
            world.ButtonManager.ButtonAdded += ButtonAdded;
            world.ButtonManager.ButtonRemoved += ButtonRemoved;
        }

        public World World { get; }

        private void ButtonAdded(ButtonManager buttonmanager, Button button)
        {
            AddChild(button);
        }

        private void ButtonRemoved(ButtonManager buttonmanager, Button button)
        {
            RemoveChild(button);
        }
    }
}