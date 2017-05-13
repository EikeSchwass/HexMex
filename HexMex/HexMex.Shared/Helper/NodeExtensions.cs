using CocosSharp;

namespace HexMex.Helper
{
    public static class NodeExtensions
    {
        public static CCPoint GetGlobalPosition(this CCNode node)
        {
            CCPoint position = node.Position;
            while ((node = node.Parent) != null)
            {
                position += node.Position;
            }
            return position;
        }

        public static void DrawCircle(this CCDrawNode node, CCPoint position, float radius, CCColor4B fillColor, float borderThickness, CCColor4B borderColor)
        {
            node.DrawSolidCircle(position, radius, borderColor);
            node.DrawSolidCircle(position, radius - borderThickness, fillColor);
        }
    }
}