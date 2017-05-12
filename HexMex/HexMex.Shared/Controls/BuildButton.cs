using CocosSharp;
using HexMex.Game;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Controls
{
    public class BuildButton : Button
    {
        private CCColor4B backgroundColor = ColorCollection.BuildButtonBackgroundColor;
        private CCColor4B borderColor = ColorCollection.BuildButtonBorderColor;
        private float borderThickness = 1;

        public BuildButton(WorldSettings settings, HexagonNode position)
        {
            WorldSettings = settings;
            HexagonNode = position;
            var x = (float)(Sin(PI / 3) * (WorldSettings.HexagonMargin - WorldSettings.HexagonBorderThickness));
            var y = (float)(Cos(PI / 3) * (WorldSettings.HexagonMargin - WorldSettings.HexagonBorderThickness));
            Radius = (float)Sqrt(x * x + y * y) * 2;
            AddChild(BackgroundNode);
            AddChild(OutlineNode);
            AddChild(SpriteNode);
            Render();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var scale = Radius * Radius / (SpriteNode.ScaledContentSize.Width * SpriteNode.ScaledContentSize.Height);
            SpriteNode.Scale = scale;
        }

        public CCColor4B BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                RenderBackground();
            }
        }

        public CCColor4B BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                RenderOutline();
            }
        }

        public float BorderThickness
        {
            get => borderThickness;
            set
            {
                borderThickness = value;
                RenderOutline();
            }
        }

        public HexagonNode HexagonNode { get; }

        public float Radius { get; }

        public CCSprite SpriteNode { get; } = new CCSprite("images/plus");
        private CCDrawNode BackgroundNode { get; } = new CCDrawNode();
        private CCDrawNode OutlineNode { get; } = new CCDrawNode();

        private WorldSettings WorldSettings { get; }

        private void Render()
        {
            RenderBackground();
            RenderOutline();
        }

        private void RenderBackground()
        {
            BackgroundNode.Clear();
            BackgroundNode.DrawDot(CCPoint.Zero, Radius, BackgroundColor);
        }

        private void RenderOutline()
        {
            OutlineNode.Clear();
            OutlineNode.DrawEllipse(new CCRect(-Radius, -Radius, Radius * 2, Radius * 2), BorderThickness, BorderColor);
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var point = this.GetGlobalPosition();
            return (location - point).Length <= Radius * 2;
        }

    }
}