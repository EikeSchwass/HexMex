using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class ControlLayer : CCLayer
    {
        public ControlLayer(ButtonManager buttonManager)
        {
            buttonManager.ButtonAdded += ButtonAdded;
            buttonManager.ButtonRemoved += ButtonRemoved;
        }

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