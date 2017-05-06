using CocosSharp;

namespace HexMex.Scenes
{
    public abstract class HexMexLayer : CCLayerColor
    {
        protected HexMexLayer(CCColor4B color) : base(color)
        {
        }

        public HexMexScene HexMexScene => Scene as HexMexScene;


    }
}