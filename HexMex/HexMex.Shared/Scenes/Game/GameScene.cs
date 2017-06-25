using CocosSharp;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class GameScene : HexMexScene
    {
        public sealed override CCCamera Camera { get; set; }
        public HexMexCamera HexMexCamera { get; }
        public World World { get; }
        private WinDefeatLayer WinDefeatLayer { get; }
        private GameLayer GameLayer { get; }

        public GameScene(CCWindow window, World world) : base(window)
        {
            World = world;
            World.Victory += World_Victory;
            World.Defeat += World_Defeat;
            HexMexCamera = new HexMexCamera(BoundingBoxTransformedToWorld.Size);
            HexMexCamera.MoveToPosition(CCPoint.Zero);
            Camera = HexMexCamera;
            AddChild(GameLayer = new GameLayer(World, HexMexCamera, CCColor4B.Black));
            AddChild(WinDefeatLayer = new WinDefeatLayer());
            HexMexCamera.SetZoomFactor(0.5f);
        }

        private void World_Defeat(World world)
        {
            World.Stop();
            WinDefeatLayer.ShowDefeatMessage(5, LayerCallback);
        }

        private void World_Victory(World world)
        {
            World.Stop();
            WinDefeatLayer.ShowVictoryMessage(5, LayerCallback);
        }

        private void LayerCallback(WinDefeatLayer winDefeatLayer)
        {
            Director.PopScene();
        }
    }
}