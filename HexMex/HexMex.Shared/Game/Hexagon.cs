using System;

namespace HexMex.Game
{
    public abstract class Hexagon : IRenderable<Hexagon>
    {
        public abstract event Action<Hexagon> RequiresRedraw;

        public HexagonPosition Position { get; }

        protected Hexagon(HexagonPosition position)
        {
            Position = position;
        }
    }
}