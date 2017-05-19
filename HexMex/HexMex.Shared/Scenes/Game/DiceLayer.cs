using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class DiceLayer : TouchLayer
    {
        private CCDrawNode DrawNode { get; }
        private World World { get; }
        private CCPoint TopCenter { get; set; }
        private bool Enabled { get; set; }

        public DiceLayer(World world)
        {
            World = world;
            AddChild(DrawNode = new CCDrawNode());
            Schedule(Update, 0.05f);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var centerX = Application.MainWindow.WindowSizeInPixels.Width / 2;
            TopCenter = ScreenToWorldspace(new CCPoint(centerX, 0));
            Enabled = true;
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!Enabled)
                return;
            var diceManager = World.DiceManager;
            var animationTime = World.GameSettings.VisualSettings.DiceAnimationTime;
            if (diceManager.DiceThrowInterval - animationTime < diceManager.TimeSinceLastDiceThrow)
            {
                var dieCount = World.GameSettings.GameplaySettings.DieCount;
                var dieFaceCount = World.GameSettings.GameplaySettings.DieCount;
                var dieThrow = Enumerable.Repeat(0, dieCount).Select(i => HexMexRandom.Next(1, dieFaceCount + 1)).ToArray();
                RenderDiceThrowResult(new DiceThrowResult(dieThrow), true);
            }
            else if (diceManager.LastDiceThrowResult != DiceThrowResult.None)
            {
                RenderDiceThrowResult(diceManager.LastDiceThrowResult, false);
            }
        }

        private void RenderDiceThrowResult(DiceThrowResult diceThrowResult, bool preview)
        {
            const float margin = 4;
            var colorCollection = World.GameSettings.VisualSettings.ColorCollection;
            var borderColor = preview ? colorCollection.GrayLight : colorCollection.White;
            DrawNode.Clear();
            var dieThrows = diceThrowResult.DieThrows.ToArray();
            var totalWidth = World.GameSettings.VisualSettings.DiceAnimationSize;
            var width = totalWidth / dieThrows.Length - (dieThrows.Length - 1) * margin;
            var totalHeight = width * 1.8f;
            var dieSize = new CCSize(width, width);

            DrawNode.DrawRect(new CCRect(TopCenter.X - totalWidth / 2, TopCenter.Y - totalHeight, totalWidth, totalHeight), colorCollection.GrayVeryDark, 1, borderColor);

            for (int i = 0; i < dieThrows.Length; i++)
            {
                float x = i * (dieSize.Width + margin * 2);
                x += dieSize.Width / 2;
                x += TopCenter.X;
                x -= totalWidth / 2;
                float y = TopCenter.Y - dieSize.Height / 2 - margin;
                var pos = new CCPoint(x, y);
                DrawNode.DrawRect(new CCRect(x - dieSize.Width / 2 + margin, y - dieSize.Height / 2 + margin, dieSize.Width - margin * 2, dieSize.Height - margin * 2), colorCollection.Black, 1, borderColor);
                DrawNode.DrawNumber(dieThrows[i], pos, (dieSize.Height - margin * 2) * 0.6f, 2, colorCollection.White.ToColor4F());
            }
            DrawNode.DrawRect(new CCRect(TopCenter.X - totalWidth / 3, TopCenter.Y - totalHeight - (totalHeight - width) * 0.85f, totalWidth / 1.5f, (totalHeight - width) * 1.7f), colorCollection.Black, 1, borderColor);
            DrawNode.DrawNumber(diceThrowResult.Sum, new CCPoint(TopCenter.X, TopCenter.Y - totalHeight), (totalHeight - width) * 0.7f, 2, colorCollection.White.ToColor4F());

        }
    }
}