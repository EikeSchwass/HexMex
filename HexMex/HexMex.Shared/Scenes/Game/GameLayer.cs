using System.Collections.ObjectModel;
using System.Linq;
using CocosSharp;
using HexMex.Game;

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