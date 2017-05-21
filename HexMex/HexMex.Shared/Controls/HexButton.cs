using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;
using HexMex.Scenes.Game;

namespace HexMex.Controls
{
    public class HexButton : Button
    {
        private CCColor4B backgroundColor = new ColorCollection().GrayVeryDark;
        private CCColor4B borderColor = new ColorCollection().White;
        private float borderThickness = 1;
        private CCColor3B foregroundColor = new CCColor3B(new ColorCollection().White);
        private float radius;
        private string text;
        private float fontSize = 30;

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
            get => fontSize;
            set
            {
                fontSize = value;
                DrawHexagon();
            }
        }

        public CCColor3B ForegroundColor
        {
            get => foregroundColor;
            set
            {
                foregroundColor = value;
                DrawHexagon();
            }
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
            get => text;
            set
            {
                text = value;
                DrawHexagon();
            }
        }

        private CCPoint[] Corners { get; set; }

        private ExtendedDrawNode DrawNode { get; }

        public HexButton(string text, float radius)
        {
            DrawNode = new ExtendedDrawNode();
            this.radius = radius;
            Text = text;
            DrawHexagon();
            AddChild(DrawNode);

            Schedule();
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            var location = ScreenToWorldspace(position.LocationOnScreen);
            var point = location - this.GetGlobalPosition();
            return HexagonHelper.IsPointInsidePolygon(Corners, point);
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

        private void DrawHexagon()
        {
            DrawNode.ZOrder = ZOrder;
            DrawNode.Clear();
            ContentSize = new CCSize(Radius * 2, Radius * 2);
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, Radius).ToArray();
            DrawNode.DrawPolygon(Corners, BackgroundColor, BorderThickness, BorderColor);
            DrawNode.DrawText(CCPoint.Zero, Text, Font.ArialFonts[30], ContentSize, ForegroundColor);
            UpdateTransform();
        }
    }
}