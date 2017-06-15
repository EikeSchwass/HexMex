using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Controls
{
    public class BuildButton : Button
    {
        private CCColor4B backgroundColor;
        private CCColor4B borderColor;
        private float borderThickness = 1;

        public CCColor4B BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Render();
            }
        }

        public CCColor4B BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Render();
            }
        }

        public float BorderThickness
        {
            get => borderThickness;
            set
            {
                borderThickness = value;
                Render();
            }
        }

        public HexagonNode HexagonNode { get; }

        public float Radius { get; }

        public CCSprite SpriteNode { get; } = new CCSprite("images/plus");
        private CCDrawNode DrawNode { get; } = new CCDrawNode();

        private GameSettings GameSettings { get; }

        public BuildButton(GameSettings settings, HexagonNode position)
        {
            GameSettings = settings;
            BorderColor = GameSettings.VisualSettings.ColorCollection.BuildButtonBorder;
            BackgroundColor = GameSettings.VisualSettings.ColorCollection.BuildButtonFill;
            HexagonNode = position;
            var x = (float)(Sin(PI / 3) * (GameSettings.LayoutSettings.HexagonMargin - GameSettings.VisualSettings.HexagonOuterBorderThickness));
            var y = (float)(Cos(PI / 3) * (GameSettings.LayoutSettings.HexagonMargin - GameSettings.VisualSettings.HexagonOuterBorderThickness));
            Radius = (float)Sqrt(x * x + y * y) * 2;
            AddChild(DrawNode);
            AddChild(SpriteNode);
            Render();
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var point = this.GetGlobalPosition();
            return (location - point).Length <= Radius * 2;
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var scale = Radius * Radius / (SpriteNode.ScaledContentSize.Width * SpriteNode.ScaledContentSize.Height);
            SpriteNode.Scale = scale;
        }

        private void Render()
        {
            DrawNode.Clear();
            DrawNode.DrawCircle(CCPoint.Zero, Radius, BackgroundColor, BorderThickness, BorderColor);
        }
    }
}