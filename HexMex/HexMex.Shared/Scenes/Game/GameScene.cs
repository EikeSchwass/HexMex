using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class GameScene : HexMexScene
    {
        public World World { get; }
        public HexMexCamera HexMexCamera { get; }

        public sealed override CCCamera Camera { get; set; }

        public GameScene(CCWindow window, DataLoader dataLoader, World world) : base(window, dataLoader)
        {
            World = world;
            HexMexCamera = new HexMexCamera(BoundingBoxTransformedToWorld.Size);
            HexMexCamera.MoveToPosition(CCPoint.Zero);
            Camera = HexMexCamera;
            AddChild(new GameLayer(CCColor4B.Black, World, HexMexCamera));
            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            World.Update(dt);
        }
    }
}
