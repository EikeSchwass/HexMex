using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class ControlLayer : CCLayer
    {
        public MenuLayer MenuLayer { get; }

        public ControlLayer(ButtonManager buttonManager, MenuLayer menuLayer)
        {
            MenuLayer = menuLayer;
            buttonManager.ButtonAdded += ButtonAdded;
            buttonManager.ButtonRemoved += ButtonRemoved;
        }

        private void ButtonAdded(ButtonManager buttonmanager, Button button)
        {
            AddChild(button);
            switch (button)
            {
                case BuildButton buildButton:
                    buildButton.Touched += BuildButtonTouched;
                    break;
            }
        }

        private void BuildButtonTouched(Button sender, CCTouch obj)
        {
            MenuLayer.OpenMenu(sender.Position);
        }

        private void ButtonRemoved(ButtonManager buttonmanager, Button button)
        {
            RemoveChild(button);
        }
    }
}