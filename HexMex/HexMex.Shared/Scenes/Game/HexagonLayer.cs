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
            var worldPosition = hexagon.Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var corners = Corners.Select(c => c * World.GameSettings.LayoutSettings.HexagonRadius + worldPosition).ToArray();
            drawNode.DrawPolygon(corners, 6, World.GameSettings.VisualSettings.ColorCollection.GrayDark, World.GameSettings.VisualSettings.HexagonOuterBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);

            if (hexagon is ResourceHexagon resourceHexagon)
            {
                float factor = resourceHexagon.RemainingResources * 1.0f / World.GameSettings.GameplaySettings.MaxNumberOfResourcesInHexagon;
                var innerCorners = Corners.Select(c => c * (World.GameSettings.LayoutSettings.HexagonRadius * factor) + worldPosition).ToArray();
                drawNode.DrawPolygon(corners, 6, resourceHexagon.ResourceType.GetColor(World.GameSettings.VisualSettings.ColorCollection), World.GameSettings.VisualSettings.HexagonInnerBorderThickness, World.GameSettings.VisualSettings.ColorCollection.White);
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