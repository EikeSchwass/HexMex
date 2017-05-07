using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Helper;

namespace HexMex.Controls
{
    public class HexButton : Button
    {
        private CCColor4B backgroundColor = ColorCollection.DefaultHexagonBackgroundColor;
        private CCColor4B borderColor = ColorCollection.DefaultHexagonBorderColor;
        private float borderThickness = 1;
        private float radius;

        public HexButton(string text, float radius)
        {
            this.radius = radius;
            TextLabel = new CCLabel(text, "fonts/MarkerFelt-22.xnb", 100, CCLabelFormat.SystemFont);
            Text = text;
            HexagoneNode = new CCDrawNode();
            DrawHexagon();

            Schedule();

            AddChild(HexagoneNode);
            AddChild(TextLabel);

            Touching += HexButton_Touching;
            Touched += HexButton_Touched;
            TouchCancelled += HexButton_Touched;
        }

        private void HexButton_Touched(CCTouch obj)
        {
            Parent.ReorderChild(this, 0);
            Radius = 150;
        }

        private void HexButton_Touching(CCTouch obj)
        {
            Parent.ReorderChild(this, 1);
            Radius = 164;
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
            var point = position.Location - Position;
            return HexagonHelper.IsPointInsideHexagon(Corners, point);
        }

        private void DrawHexagon()
        {
            HexagoneNode.ZOrder = ZOrder;
            HexagoneNode.Clear();
            Corners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, Radius)
                                   .ToArray();
            HexagoneNode.DrawPolygon(Corners, 6, BackgroundColor, BorderThickness, BorderColor);
            ContentSize = new CCSize(Radius * 2, Radius * 2);
            UpdateTransform();
        }
    }
}