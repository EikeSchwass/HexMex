using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class HexagonLayer : TouchLayer
    {
        private CCPoint[] Corners { get; }
        private CCDrawNode DynamicDrawNode { get; } = new CCDrawNode();
        private List<Hexagon> DynamicHexagons { get; } = new List<Hexagon>();

        private bool RedrawRequested { get; set; } = true;

        private CCDrawNode StaticDrawNode { get; } = new CCDrawNode();

        private List<Hexagon> StaticHexagons { get; } = new List<Hexagon>();

        private World World { get; }

        public HexagonLayer(World world, HexMexCamera camera) : base(camera)
        {
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, 1).ToArray();
            World = world;
            World.HexagonManager.HexagonRevealed += HexagonRevealed;
            AddChild(StaticDrawNode);
            AddChild(DynamicDrawNode);
            StaticHexagons.AddRange(World.HexagonManager);
            Schedule(Update, 1);
            RenderStatic();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested || DynamicHexagons.Count == 0)
                return;
            RedrawRequested = false;
            RenderDynamic();
        }

        private void HexagonRequiresRedraw(Hexagon hexagon)
        {
            if (StaticHexagons.Contains(hexagon))
            {
                StaticHexagons.Remove(hexagon);
                DynamicHexagons.Add(hexagon);
            }
            RedrawRequested = true;
        }

        private void HexagonRevealed(HexagonManager hexagonManager, Hexagon hexagon)
        {
            hexagon.RequiresRedraw += HexagonRequiresRedraw;
            StaticHexagons.Add(hexagon);
            RenderStatic();
        }

        private void RenderDynamic()
        {
            DynamicDrawNode.Clear();
            foreach (var hexagon in DynamicHexagons)
            {
                RenderHexagon(DynamicDrawNode, hexagon);
            }
        }

        private void RenderHexagon(CCDrawNode drawNode, Hexagon hexagon)
        {
            var worldPosition = hexagon.Position.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
            var corners = Corners.Select(c => c * World.WorldSettings.HexagonRadius + worldPosition).ToArray();
            drawNode.DrawPolygon(corners, 6, ColorCollection.HexagonBackgroundColor, World.WorldSettings.HexagonBorderThickness, ColorCollection.HexagonBorderColor);

            if (hexagon is ResourceHexagon resourceHexagon)
            {
                float factor = resourceHexagon.RemainingResources * 1.0f / World.WorldSettings.MaxNumberOfResourcesInHexagon;
                var innerCorners = Corners.Select(c => c * (World.WorldSettings.HexagonRadius * factor) + worldPosition).ToArray();
                drawNode.DrawPolygon(innerCorners, 6, resourceHexagon.ResourceType.GetColor(), 0, CCColor4B.Transparent);
            }
        }

        private void RenderStatic()
        {
            StaticDrawNode.Clear();
            foreach (var hexagon in StaticHexagons)
            {
                RenderHexagon(StaticDrawNode, hexagon);
            }
        }
    }
}