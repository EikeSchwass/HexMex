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
        public GameLayer(World world, HexMexCamera camera, CCColor4B color) : base(color)
        {
            HexMexCamera = camera;
            TouchHandler = new GameTouchHandler(this, HexMexCamera);
            World = world;
            var hexagonLayer = new HexagonLayer(World.HexagonManager, World.WorldSettings, HexMexCamera);
            var blendLayer = new CCLayerColor(camera, new CCColor4B(0, 0, 0, 0.4f));
            var resourcePackageLayer = new ResourcePackageLayer(World.ResourcePackageManager, World.WorldSettings, HexMexCamera);
            var structureLayer = new StructureLayer(World.StructureManager, World.WorldSettings, HexMexCamera);
            var buildMenuLayer = new BuildMenuLayer(HexMexCamera);
            var controlLayer = new ButtonLayer(World.ButtonManager, HexMexCamera);
            buildMenuLayer.ConstructionRequested += ConstructBuilding;
            controlLayer.ConstructionRequested += (s, b) => buildMenuLayer.DisplayBuildMenuFor(b);

            var layers = new CCLayer[]
            {
                hexagonLayer,
                blendLayer,
                resourcePackageLayer,
                structureLayer,
                controlLayer,
                buildMenuLayer
            };

            foreach (var layer in layers)
            {
                AddChild(layer);
            }

            TouchLayers = new ReadOnlyCollection<TouchLayer>(layers.OfType<TouchLayer>().Reverse().ToList());

            Schedule();
        }

        private void ConstructBuilding(BuildMenuLayer buildMenu, BuildingConstructionFactory selectedFactory, BuildButton buildButton)
        {
            if (World.StructureManager[buildButton.HexagonNode] != null)
                throw new InvalidOperationException("Spot has to be empty");
            var construction = new Construction(buildButton.HexagonNode, selectedFactory, World);
            World.StructureManager.CreateStrucuture(construction);
            World.ButtonManager.RemoveButton(buildButton);
        }

        public HexMexCamera HexMexCamera { get; }
        public GameTouchHandler TouchHandler { get; }

        public ReadOnlyCollection<TouchLayer> TouchLayers { get; }
        public World World { get; }

        public override void Update(float dt)
        {
            base.Update(dt);
            World.Update(dt);
        }
    }
}