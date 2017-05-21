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
            AddChild(DrawNode);
            Schedule(Update, 0.05f);
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
            var worldPosition = hexagon.Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
            var corners = Corners.Select(c => c * World.GameSettings.LayoutSettings.HexagonRadius + worldPosition).ToArray();
            DrawNode.DrawPolygon(corners, hexagon.ResourceType.GetColor(colorCollection), visualSettings.HexagonOuterBorderThickness, colorCollection.White);
            DrawNode.DrawSolidArc(worldPosition, World.GameSettings.LayoutSettings.HexagonProgressRadius, (float)(hexagon.TimeSinceLastPayout / hexagon.PayoutInterval * 2 * PI), colorCollection.GrayVeryLight);
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