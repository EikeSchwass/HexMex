using CocosSharp;

namespace HexMex.Scenes
{
    public class TouchEventArgs
    {
        public bool Handled { get; set; }
        public CCTouch Touch { get; }

        public TouchEventArgs(CCTouch touch)
        {
            Touch = touch;
        }
    }
}