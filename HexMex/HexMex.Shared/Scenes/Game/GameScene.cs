using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class GameScene : HexMexScene
    {
        public World World { get; } = new World();
        public HexMexCamera HexMexCamera { get; }

        public sealed override CCCamera Camera { get; set; }

        public GameScene(CCWindow window, DataLoader dataLoader) : base(window, dataLoader)
        {
            HexMexCamera = new HexMexCamera(BoundingBoxTransformedToWorld.Size);
            //HexMexCamera.MoveToPosition(CCPoint.Zero);
            Camera = HexMexCamera;
            AddChild(new GameLayer(CCColor4B.Black, World, HexMexCamera));
            Schedule();
        }
    }
}
