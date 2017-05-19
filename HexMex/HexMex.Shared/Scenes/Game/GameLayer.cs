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
            var buildMenuLayer = new BuildMenuLayer(World, HexMexCamera);
            var controlLayer = new ButtonLayer(World, HexMexCamera);
            var edgeLayer = new EdgeLayer(World, HexMexCamera);
            var diceLayer = new DiceLayer(World);
            var diceEffectLayer = new DiceEffectLayer(World, HexMexCamera);
            buildMenuLayer.ConstructionRequested += ConstructBuilding;
            controlLayer.ConstructionRequested += (s, b) => buildMenuLayer.DisplayBuildMenuFor(b);

            var layers = new CCLayer[] { hexagonLayer, diceEffectLayer, edgeLayer, resourcePackageLayer, structureLayer, controlLayer, diceLayer, buildMenuLayer };

            foreach (var layer in layers)
            {
                AddChild(layer);
            }

            TouchLayers = new ReadOnlyCollection<TouchLayer>(layers.OfType<TouchLayer>().Reverse().ToList());

            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            World.Update(dt);
        }

        private void ConstructBuilding(BuildMenuLayer buildMenu, BuildingConstructionFactory selectedFactory, BuildButton buildButton)
        {
            if (World.StructureManager[buildButton.HexagonNode] != null)
                throw new InvalidOperationException("Spot has to be empty");
            var construction = new Construction(buildButton.HexagonNode, selectedFactory, World);
            World.StructureManager.CreateStrucuture(construction);
            World.ButtonManager.RemoveButton(buildButton);
        }
    }
}