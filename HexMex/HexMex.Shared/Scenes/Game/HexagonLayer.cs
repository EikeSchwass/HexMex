using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class HexagonLayer : TouchLayer
    {
        private CCPoint[] Corners { get; }
        private ExtendedDrawNode DrawNode { get; } = new ExtendedDrawNode();

        private World World { get; }

        public HexagonLayer(World world, HexMexCamera camera) : base(camera)
        {
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, 1).ToArray();
            World = world;
            World.HexagonManager.HexagonRevealed += (hm, h) => Render();
            AddChild(DrawNode);
            Schedule(Update, 0.5f);
            Render();
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            Render();
        }

        private void Render()
        {
            DrawNode.Clear();
            foreach (var hexagon in World.HexagonManager)
            {
                RenderHexagon(hexagon);
            }
        }

        private void RenderHexagon(Hexagon hexagon)
        {
            var visualSettings = World.GameSettings.VisualSettings;
            var colorCollection = visualSettings.ColorCollection;
            var layoutSettings = World.GameSettings.LayoutSettings;
            var worldPosition = hexagon.Position.GetWorldPosition(layoutSettings.HexagonRadius, layoutSettings.HexagonMargin);
            var corners = Corners.Select(c => c * layoutSettings.HexagonRadius + worldPosition).ToArray();
            var innerColor = CCColor4B.Lerp(hexagon.ResourceType.GetColor(colorCollection), colorCollection.Black, 0.05f);
            var outerColor = CCColor4B.Lerp(hexagon.ResourceType.GetColor(colorCollection), colorCollection.Black, 0.75f);
            var center = new CCV3F_C4B(worldPosition, innerColor);
            var adjacentHexagonPositions = hexagon.Position.GetAdjacentHexagonPositions();
            for (int i = 0; i < corners.Length; i++)
            {
                var p1 = new CCV3F_C4B(corners[i], outerColor);
                var p2 = new CCV3F_C4B(corners[(i + 1) % corners.Length], outerColor);
                DrawNode.DrawTriangle(p1, center, p2);
                if (World.HexagonManager[adjacentHexagonPositions[(6 - i + 2) % 6]]?.ResourceType != hexagon.ResourceType)
                    DrawNode.DrawLine(corners[i], corners[(i + 1) % corners.Length], visualSettings.HexagonOuterBorderThickness, CCColor4B.Lerp(colorCollection.White, CCColor4B.Transparent, 0.5f));
            }
            DrawNode.DrawText(worldPosition, hexagon.ResourceType.ToString(), Font.ArialFonts[30], new CCSize(layoutSettings.HexagonRadius * 2, layoutSettings.HexagonRadius * 2));
        }

        private static double GetEffectWidth(double t)
        {
            const double maxAt = 0.1;
            const double maxHeight = 2;

            const double sinh = 0.8813735870;
            const double heightFactor = 6 + 4 * 1.4142135623;
            const double exp = 1 / maxAt * sinh;

            double firstPart = 1 / (1 + Exp(-exp * 2 * t)) - 0.5;
            double secondPart = Exp(-exp / 8 * t);
            double result = firstPart * secondPart * heightFactor * maxHeight;
            return result;
        }
    }
}