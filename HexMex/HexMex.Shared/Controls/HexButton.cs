using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;
using HexMex.Scenes.Game;

namespace HexMex.Controls
{
    public class HexButton : Button
    {
        private CCColor4B backgroundColor = ColorCollection.HexagonBackgroundColor;
        private CCColor4B borderColor = ColorCollection.HexagonBorderColor;
        private float borderThickness = 1;
        private float radius;

        public HexButton(string text, float radius, Font font)
        {
            this.radius = radius;
            TextLabel = new CCLabel(text, font.FontPath, font.FontSize, font.FontType);
            Text = text;
            HexagoneNode = new CCDrawNode();
            DrawHexagon();

            Schedule();

            AddChild(HexagoneNode);
            AddChild(TextLabel);

        }

        public CCColor4B BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                DrawHexagon();
            }
        }

        public CCColor4B BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                DrawHexagon();
            }
        }

        public float BorderThickness
        {
            get => borderThickness;
            set
            {
                borderThickness = value;
                DrawHexagon();
            }
        }

        public float FontSize
        {
            get => TextLabel.SystemFontSize;
            set => TextLabel.SystemFontSize = value;
        }

        public CCColor3B ForegroundColor
        {
            get => TextLabel.Color;
            set => TextLabel.Color = value;
        }

        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                DrawHexagon();
            }
        }

        public string Text
        {
            get => TextLabel.Text;
            set => TextLabel.Text = value;
        }

        private CCPoint[] Corners { get; set; }

        private CCDrawNode HexagoneNode { get; }
        private CCLabel TextLabel { get; }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var point = location - this.GetGlobalPosition();
            return HexagonHelper.IsPointInsidePolygon(Corners, point);
        }

        private void DrawHexagon()
        {
            HexagoneNode.ZOrder = ZOrder;
            HexagoneNode.Clear();
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, Radius).ToArray();
            HexagoneNode.DrawPolygon(Corners, 6, BackgroundColor, BorderThickness, BorderColor);
            ContentSize = new CCSize(Radius * 2, Radius * 2);
            UpdateTransform();
        }

        protected override void OnIsPressedChanged()
        {
            base.OnIsPressedChanged();
            if (IsPressed)
            {
                Parent.ReorderChild(this, 1);
                BorderThickness = 1 + Radius / 100;
            }
            else
            {
                Parent.ReorderChild(this, 0);
                BorderThickness = 1;
            }

        }
    }
}