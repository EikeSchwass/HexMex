using CocosSharp;
using HexMex.Helper;

namespace HexMex.Scenes
{
    public class HexMexScene : CCScene
    {
        public HexMexScene(CCWindow window, DataLoader dataLoader) : base(window)
        {
            DataLoader = dataLoader;
        }

        public DataLoader DataLoader { get; }
    }
}


