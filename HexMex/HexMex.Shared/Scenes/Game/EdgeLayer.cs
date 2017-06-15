using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;

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
            World.StructureManager.StructureAdded += (sm, s) => RedrawRequested = true;
            World.StructureManager.StructureRemoved += (sm, s) => RedrawRequested = true;
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
            List<HexagonNode> handledPositions = new List<HexagonNode>();
            foreach (var structure in World.StructureManager)
            {
                if (structure is Construction)
                    continue;
                handledPositions.Add(structure.Position);
                foreach (var accessibleNode in structure.Position.GetAccessibleAdjacentHexagonNodes(World.HexagonManager).Where(h => !handledPositions.Contains(h)))
                {
                    var p1 = structure.Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                    var p2 = accessibleNode.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                    var color = World.GameSettings.VisualSettings.ColorCollection.EdgeBackground;
                    DrawNode.DrawSegment(p1, p2, World.GameSettings.VisualSettings.EdgeThickness, color);
                }
            }
        }
    }
}