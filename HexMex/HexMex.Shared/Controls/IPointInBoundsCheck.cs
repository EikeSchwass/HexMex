using CocosSharp;

namespace HexMex.Controls
{
    public interface IPointInBoundsCheck
    {
        bool IsPointInBounds(CCTouch touch);
    }
}