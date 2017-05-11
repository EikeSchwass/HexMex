using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Buildings;

namespace HexMex.Scenes.Game
{
    public class ControlLayer : CCLayer
    {
        public ButtonManager ButtonManager { get; }
        public MenuLayer MenuLayer { get; }
        public StructureManager StructureManager { get; }

        public ControlLayer(ButtonManager buttonManager, StructureManager structureManager, MenuLayer menuLayer, ResourceManager resourceManager, HexagonManager hexagonManager)
        {
            ButtonManager = buttonManager;
            MenuLayer = menuLayer;
            ResourceManager = resourceManager;
            HexagonManager = hexagonManager;
            StructureManager = structureManager;
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
            var buildButton = (BuildButton)sender;
            var construction = new Construction(buildButton.HexagonNode, BuildingConstructionFactory.Factories[typeof(MineBuilding)], ResourceManager, HexagonManager, StructureManager);
            StructureManager.CreateStrucuture(construction);
            ButtonManager.RemoveButton(sender);
            //MenuLayer.OpenMenu(sender.Position);
        }

        public HexagonManager HexagonManager { get; }

        public ResourceManager ResourceManager { get; }

        private void ButtonRemoved(ButtonManager buttonmanager, Button button)
        {
            RemoveChild(button);
        }
    }
}