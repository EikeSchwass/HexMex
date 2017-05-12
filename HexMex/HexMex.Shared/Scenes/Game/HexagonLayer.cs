using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class HexagonLayer : TouchLayer
    {
        public HexagonLayer(World world, HexMexCamera camera) : base(camera)
        {
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, 1).ToArray();
            World = world;
            World.HexagonManager.HexagonRevealed += HexagonRevealed;
            AddChild(DrawNode);
            Schedule(Update, 1);
        }

        private CCPoint[] Corners { get; }

        private CCDrawNode DrawNode { get; } = new CCDrawNode();

        private List<Hexagon> Hexagons { get; } = new List<Hexagon>();

        private bool RedrawRequested { get; set; }

        private World World { get; }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            RedrawRequested = false;
            Render();
        }

        private void HexagonRequiresRedraw(Hexagon hexagon)
        {
            RedrawRequested = true;
        }

        private void HexagonRevealed(HexagonManager hexagonManager, Hexagon hexagon)
        {
            hexagon.RequiresRedraw += HexagonRequiresRedraw;
            Hexagons.Add(hexagon);
            RedrawRequested = true;
        }

        private void Render()
        {
            DrawNode.Clear();
            foreach (var hexagon in Hexagons)
            {
                RenderHexagon(hexagon);
            }
        }

        private void RenderHexagon(Hexagon hexagon)
        {
            var worldPosition = hexagon.Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            var corners = Corners.Select(c => c * World.WorldSettings.HexagonRadius + worldPosition).ToArray();
            DrawNode.DrawPolygon(corners, 6, ColorCollection.HexagonBackgroundColor, World.WorldSettings.HexagonBorderThickness, ColorCollection.HexagonBorderColor);

            if (hexagon is ResourceHexagon resourceHexagon)
            {
                float factor = resourceHexagon.RemainingResources * 1.0f / World.WorldSettings.MaxNumberOfResourcesInHexagon;
                var innerCorners = Corners.Select(c => c * (World.WorldSettings.HexagonRadius * factor) + worldPosition).ToArray();
                DrawNode.DrawPolygon(innerCorners, 6, resourceHexagon.ResourceType.GetColor(), 0, CCColor4B.Transparent);
            }
        }
    }
}