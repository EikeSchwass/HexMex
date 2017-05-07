using CocosSharp;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class GameLayer : HexMexLayer
    {
        private const float MINTOUCHDISTANCE = 10;
        private float StartZoom { get; set; } = 1;
        private CCPoint StartPosition { get; set; } = CCPoint.Zero;

        public World World { get; }
        public HexMexCamera HexMexCamera { get; }
        public TouchHandler TouchHandler { get; }

        public GameLayer(CCColor4B color, World world, HexMexCamera camera) : base(color)
        {
            TouchHandler = new TouchHandler(this);
            TouchHandler.Dragging += Dragging;
            TouchHandler.Pintching += Pintching;
            World = world;
            HexMexCamera = camera;
            AddChild(new HexagonLayer(World) { Camera = camera });
            AddChild(new CCLayerColor(camera, new CCColor4B(0, 0, 0, 0.4f)));
            AddChild(new StructureLayer(World) { Camera = camera });
            AddChild(new ControlLayer(World) { Camera = camera });
        }

        private void Pintching(CCTouch touch1, CCTouch touch2)
        {
            var prevDiff = (touch1.PreviousLocation - touch2.PreviousLocation).Length;
            var currentDiff = (touch1.Location - touch2.Location).Length;
            if (currentDiff < MINTOUCHDISTANCE)
                return;

            var sizeFactor = currentDiff / prevDiff;
            sizeFactor = sizeFactor - (sizeFactor - 1) / 2;

            HexMexCamera.SetZoomFactor(sizeFactor * HexMexCamera.ZoomFactor);

        }

        private void Dragging(CCPoint delta)
        {
            HexMexCamera.MoveToPosition(HexMexCamera.Position + delta / HexMexCamera.ZoomFactor);
        }

    }
}