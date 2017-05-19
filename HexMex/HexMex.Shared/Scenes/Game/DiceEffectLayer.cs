using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class DiceEffectLayer : TouchLayer
    {
        private List<Hexagon> ActiveHexagons { get; } = new List<Hexagon>();
        private CCDrawNode DrawNode { get; }
        public World World { get; }
        private CCPoint[] HexagonCorners { get; }
        public DiceEffectLayer(World world, HexMexCamera camera) : base(camera)
        {
            DrawNode = new CCDrawNode();
            AddChild(DrawNode);
            World = world;
            HexagonCorners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, World.GameSettings.LayoutSettings.HexagonRadius).ToArray();
            World.DiceManager.NewDiceThrowResult += NewDiceThrowResult;
            Schedule();
        }

        private void NewDiceThrowResult(DiceManager sender, DiceThrowResult diceThrowResult)
        {
            ActiveHexagons.Clear();
            foreach (var hexagon in World.HexagonManager)
            {
                if (hexagon.PayoutSum != diceThrowResult.Sum && hexagon.PayoutSum != 0)
                    continue;
                ActiveHexagons.Add(hexagon);
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (World.DiceManager.LastDiceThrowResult == DiceThrowResult.None)
            {
                ActiveHexagons.Clear();
                DrawNode.Clear();
                return;
            }
            Render();
        }

        private void Render()
        {
            DrawNode.Clear();
            float thickness = (float)GetEffectWidth(World.DiceManager.TimeSinceLastDiceThrow);
            if (thickness > 6)
            {

            }
            foreach (var hexagon in ActiveHexagons.ToArray())
            {
                var pos = hexagon.Position.GetWorldPosition(World.GameSettings.LayoutSettings.HexagonRadius, World.GameSettings.LayoutSettings.HexagonMargin);
                for (int i = 0; i < HexagonCorners.Length; i++)
                {
                    var p1 = HexagonCorners[i] + pos;
                    var p2 = HexagonCorners[(i + 1) % HexagonCorners.Length] + pos;
                    DrawNode.DrawSegment(p1, p2, thickness / 2, World.GameSettings.VisualSettings.ColorCollection.YellowLight.ToColor4F());
                }
            }
        }

        private static double GetEffectWidth(double t)
        {
            const double maxAt = 0.1;
            const double maxHeight = 4;

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