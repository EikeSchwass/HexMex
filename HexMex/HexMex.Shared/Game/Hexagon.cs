namespace HexMex.Game
{
    public abstract class Hexagon
    {
        protected Hexagon(HexagonPosition position)
        {
            Position = position;
        }

        public HexagonPosition Position { get; }
    }
}