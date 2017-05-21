using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Scenes.Game;
using static System.Math;

namespace HexMex.Controls
{
    public class ExtendedDrawNode : CCNode
    {
        private CCDrawNode DrawNode { get; }
        private List<CCLabel> Labels { get; } = new List<CCLabel>();
        private int UsageIndex { get; set; }

        public ExtendedDrawNode()
        {
            AddChild(DrawNode = new CCDrawNode());
        }

        public void Clear()
        {
            DrawNode.Clear();
            foreach (var label in Labels)
            {
                label.Visible = false;
            }
            UsageIndex = 0;
        }

        public void DrawCircle(CCPoint position, float radius, CCColor4B fillColor, float borderThickness, CCColor4B borderColor, CircleBorderPosition borderPosition)
        {
            float borderOffset = borderPosition == CircleBorderPosition.Inside ? 0 : (borderPosition == CircleBorderPosition.Outside ? borderThickness : borderThickness / 2);
            float radiusOffset = borderPosition == CircleBorderPosition.Inside ? -borderThickness : (borderPosition == CircleBorderPosition.Outside ? 0 : -borderThickness / 2);

            // Border Circle
            DrawNode.DrawCircle(position, radius + borderOffset, borderColor);
            // Normal Circle
            DrawNode.DrawCircle(position, radius + radiusOffset, fillColor);
        }

        public void DrawCircle(CCPoint position, float radius, CCColor4B fillColor) => DrawCircle(position, radius, fillColor, 0, CCColor4B.Transparent, CircleBorderPosition.HalfHalf);

        public void DrawLine(CCPoint from, CCPoint to, float thickness, CCColor4B color)
        {
            DrawNode.DrawLine(from, to, thickness, color);
        }

        public void DrawLine(CCPoint from, CCPoint to) => DrawLine(from, to, 0, CCColor4B.White);

        public void DrawPolygon(CCPoint[] corners, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            DrawNode.DrawPolygon(corners, corners.Length, fillColor, borderThickness, borderColor);
        }

        public void DrawPolygon(CCPoint[] corners, CCColor4B fillColor) => DrawPolygon(corners, fillColor, 0, CCColor4B.Transparent);

        public void DrawRect(CCPoint position, float width, float height, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            DrawNode.DrawRect(new CCRect(position.X - width / 2, position.Y - height / 2, width, height), fillColor, borderThickness, borderColor);
        }

        public void DrawRect(CCPoint position, CCSize size, CCColor4B fillColor, float borderThickness, CCColor4B borderColor) => DrawRect(position, size.Width, size.Height, fillColor, borderThickness, borderColor);
        public void DrawRect(CCPoint position, CCSize size, CCColor4B fillColor) => DrawRect(position, size.Width, size.Height, fillColor, 0, CCColor4B.Transparent);
        public void DrawRect(CCPoint position, float width, float height, CCColor4B fillColor) => DrawRect(position, width, height, fillColor, 0, CCColor4B.Transparent);
        public void DrawRect(CCRect position, CCColor4B fillColor, float borderThickness, CCColor4B borderColor) => DrawRect(position.Center, position.Size.Width, position.Size.Height, fillColor, borderThickness, borderColor);
        public void DrawRect(CCRect position, CCColor4B fillColor) => DrawRect(position.Center, position.Size.Width, position.Size.Height, fillColor, 0, CCColor4B.Transparent);

        public void DrawSolidArc(CCPoint position, float radius, float startAngle, float sweepAngle, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            DrawNode.DrawSolidArc(position, radius + borderThickness, startAngle + (float)PI / 2, -sweepAngle, borderColor);
            DrawNode.DrawSolidArc(position, radius, startAngle + (float)PI / 2, -sweepAngle, fillColor);
        }

        public void DrawSolidArc(CCPoint position, float radius, float startAngle, float sweepAngle, CCColor4B fillColor) => DrawSolidArc(position, radius, startAngle, sweepAngle, fillColor, 0, CCColor4B.Transparent);
        public void DrawSolidArc(CCPoint position, float radius, float angle, CCColor4B fillColor) => DrawSolidArc(position, radius, 0, angle, fillColor, 0, CCColor4B.Transparent);
        public void DrawSolidArc(CCPoint position, float radius, float angle, CCColor4B fillColor, float borderThickness, CCColor4B borderColor) => DrawSolidArc(position, radius, 0, angle, fillColor, borderThickness, borderColor);

        public void DrawText(CCPoint position, string text, Font font, CCSize targetSize, CCColor3B color)
        {
            CCLabel label;
            if (UsageIndex >= Labels.Count)
            {
                label = new CCLabel(text, font.FontPath, font.FontSize, targetSize, font.FontType)
                {
                    HorizontalAlignment = CCTextAlignment.Left,
                    VerticalAlignment = CCVerticalTextAlignment.Center,
                    LineBreak = CCLabelLineBreak.Word
                };
                Labels.Add(label);
                AddChild(label);
            }
            else
            {
                label = Labels[UsageIndex];
            }
            if (label.Text != text)
                label.Text = text;
            if (label.Color != color)
                label.Color = color;
            label.Position = position;
            label.Visible = true;
            UsageIndex++;
        }

        public void DrawText(float x, float y, string text, Font font, CCSize targetSize, CCColor3B color) => DrawText(new CCPoint(x, y), text, font, targetSize, color);
        public void DrawText(float x, float y, string text, Font font, CCSize targetSize) => DrawText(new CCPoint(x, y), text, font, targetSize, CCColor3B.White);

        public void DrawText(CCPoint position, string text, Font font, CCSize targetSize) => DrawText(position, text, font, targetSize, CCColor3B.White);

        public void DrawTriangle(CCV3F_C4B[] corners, float borderThickness, CCColor4B borderColor)
        {
            DrawNode.DrawTriangleList(corners);
            if (borderThickness > 0)
                for (int i = 0; i < 3; i++)
                {
                    var p1 = corners[i].Vertices;
                    var p2 = corners[(i + 1) % 3].Vertices;
                    DrawNode.DrawLine(new CCPoint(p1.X, p1.Y), new CCPoint(p2.X, p2.Y), borderThickness, borderColor);
                }
        }

        public void DrawTriangle(CCV3F_C4B[] corners) => DrawTriangle(corners, 0, CCColor4B.Transparent);
        public void DrawTriangle(CCPoint[] corners, CCColor4B fillColor, float borderThickness, CCColor4B borderColor) => DrawTriangle(corners.Select(c => new CCV3F_C4B(c, fillColor)).ToArray(), borderThickness, borderColor);
        public void DrawTriangle(CCPoint[] corners, CCColor4B fillColor) => DrawTriangle(corners.Select(c => new CCV3F_C4B(c, fillColor)).ToArray(), 0, CCColor4B.Transparent);

        public void DrawTriangle(CCV3F_C4B p1, CCV3F_C4B p2, CCV3F_C4B p3, float borderThickness, CCColor4B borderColor) => DrawTriangle(new[]
        {
            p1,
            p2,
            p3
        }, borderThickness, borderColor);

        public void DrawTriangle(CCV3F_C4B p1, CCV3F_C4B p2, CCV3F_C4B p3) => DrawTriangle(p1, p2, p3, 0, CCColor4B.Transparent);

        public void DrawTriangle(CCPoint p1, CCPoint p2, CCPoint p3, CCColor4B fillColor, float borderThickness, CCColor4B borderColor) => DrawTriangle(new[]
        {
            p1,
            p2,
            p3
        }, fillColor, borderThickness, borderColor);

        public void DrawTriangle(CCPoint p1, CCPoint p2, CCPoint p3, CCColor4B fillColor) => DrawTriangle(p1, p2, p3, fillColor, 0, CCColor4B.Transparent);
    }
}