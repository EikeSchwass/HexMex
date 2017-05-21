using System;
using System.Collections.ObjectModel;
using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Buildings;

namespace HexMex.Scenes.Game
{
    public class GameLayer : CCLayerColor
    {
        public HexMexCamera HexMexCamera { get; }
        public GameTouchHandler TouchHandler { get; }

        public ReadOnlyCollection<TouchLayer> TouchLayers { get; }
        public World World { get; }

        public GameLayer(World world, HexMexCamera camera, CCColor4B color) : base(color)
        {
            HexMexCamera = camera;
            TouchHandler = new GameTouchHandler(this, HexMexCamera);
            World = world;
            var hexagonLayer = new HexagonLayer(World, HexMexCamera);
            var resourcePackageLayer = new ResourcePackageLayer(World, HexMexCamera);
            var structureLayer = new StructureLayer(World, HexMexCamera);
            var menuLayer = new MenuLayer(World, HexMexCamera);
            var controlLayer = new ButtonLayer(World, HexMexCamera);
            var edgeLayer = new EdgeLayer(World, HexMexCamera);
            controlLayer.ConstructionRequested += (buttonLayer, buildButton) => ConstructionMenuRequested(buildButton, menuLayer);

            var layers = new CCLayer[] { hexagonLayer, edgeLayer, resourcePackageLayer, structureLayer, controlLayer, menuLayer };

            foreach (var layer in layers)
            {
                AddChild(layer);
            }

            TouchLayers = new ReadOnlyCollection<TouchLayer>(layers.OfType<TouchLayer>().Reverse().ToList());

            Schedule();
        }

        private void ConstructionMenuRequested(BuildButton buildButton, MenuLayer menuLayer)
        {
            var buildMenu = new BuildMenu(World.GameSettings.VisualSettings);
            buildMenu.ConstructionRequested += (sender, selectedFactory) => ConstructBuilding(selectedFactory, buildButton);
            menuLayer.DisplayMenu(buildMenu);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            World.Update(dt);
        }

        private void ConstructBuilding(BuildingConstructionFactory selectedFactory, BuildButton buildButton)
        {
            if (World.StructureManager[buildButton.HexagonNode] != null)
                throw new InvalidOperationException("Spot has to be empty");
            var construction = new Construction(buildButton.HexagonNode, selectedFactory, World);
            World.StructureManager.CreateStrucuture(construction);
            World.ButtonManager.RemoveButton(buildButton);
        }
    }
}