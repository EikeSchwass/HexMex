using CocosSharp;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class GameScene : HexMexScene
    {
        public sealed override CCCamera Camera { get; set; }
        public HexMexCamera HexMexCamera { get; }
        public World World { get; }

        public GameScene(CCWindow window, World world) : base(window)
        {
            World = world;
            HexMexCamera = new HexMexCamera(BoundingBoxTransformedToWorld.Size);
            HexMexCamera.MoveToPosition(CCPoint.Zero);
            Camera = HexMexCamera;
            AddChild(new GameLayer(World, HexMexCamera, CCColor4B.Black));
            HexMexCamera.SetZoomFactor(0.5f);
        }
    }
}