using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class GameLayer : HexMexLayer
    {
        public World World { get; }

        public GameLayer(CCColor4B color, World world) : base(color)
        {
            World = world;
            AddChild(new HexButton("Hallo", 100));
        }
    }
}