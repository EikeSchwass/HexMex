using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class GameScene : HexMexScene
    {
        public World World { get; } = new World();
        public HexMexCamera HexMexCamera { get; }

        public GameScene(CCWindow window, DataLoader dataLoader) : base(window, dataLoader)
        {
            HexMexCamera = new HexMexCamera(BoundingBoxTransformedToWorld.Size);
            AddChild(new GameLayer(CCColor4B.Red, World) { Camera = HexMexCamera });
            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            //World.Update(dt);
            HexMexCamera.Position += new CCPoint(dt * 10, dt*15);
        }
    }
}
