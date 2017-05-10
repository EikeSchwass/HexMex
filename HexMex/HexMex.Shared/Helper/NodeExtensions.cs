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
    }
}