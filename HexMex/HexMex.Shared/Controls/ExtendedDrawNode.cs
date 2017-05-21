using System.Collections.Generic;
using CocosSharp;
using HexMex.Scenes.Game;
using static System.Math;

namespace HexMex.Controls
{
    public class ExtendedDrawNode : CCNode
    {
        private CCDrawNode DrawNode { get; }
        private List<CCLabel> Labels { get; } = new List<CCLabel>();

        public ExtendedDrawNode()
        {
            AddChild(DrawNode = new CCDrawNode());
        }

        public void Clear()
        {
            DrawNode.Clear();
            foreach (var label in Labels)
            {
                RemoveChild(label, false);
            }
            Cleanup();
            Labels.Clear();
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
            CCLabel label = new CCLabel(text, font.FontPath, font.FontSize, targetSize, font.FontType)
            {
                Color = color,
                HorizontalAlignment = CCTextAlignment.Left,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                LineBreak = CCLabelLineBreak.Word,
                Position = position,
            };
            Labels.Add(label);
            AddChild(label);
        }

        public void DrawText(float x, float y, string text, Font font, CCSize targetSize, CCColor3B color) => DrawText(new CCPoint(x, y), text, font, targetSize, color);
        public void DrawText(float x, float y, string text, Font font, CCSize targetSize) => DrawText(new CCPoint(x, y), text, font, targetSize, CCColor3B.White);

        public void DrawText(CCPoint position, string text, Font font, CCSize targetSize) => DrawText(position, text, font, targetSize, CCColor3B.White);

        public void DrawPolygon(CCPoint[] corners, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            DrawNode.DrawPolygon(corners, corners.Length, fillColor, borderThickness, borderColor);
        }

        public void DrawPolygon(CCPoint[] corners, CCColor4B fillColor) => DrawPolygon(corners, fillColor, 0, CCColor4B.Transparent);
    }
}