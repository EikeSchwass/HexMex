using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class EdgeLayer : TouchLayer
    {
        public World World { get; }
        private CCDrawNode DrawNode { get; } = new CCDrawNode();

        private bool RedrawRequested { get; set; }

        public EdgeLayer(World world, HexMexCamera camera) : base(camera)
        {
            World = world;
            World.EdgeManager.EdgeAdded += (s, e) => RedrawRequested = true;
            AddChild(DrawNode);
            Schedule();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            RedrawRequested = false;

            DrawNode.Clear();
            foreach (var edge in World.EdgeManager)
            {
                var p1 = edge.From.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                var p2 = edge.To.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                DrawNode.DrawSegment(p1, p2, World.GameSettings.LayoutSettings.HexagonMargin / 4, World.GameSettings.VisualSettings.ColorCollection.White.ToColor4F());
            }
        }
    }
}