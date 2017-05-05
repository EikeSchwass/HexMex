namespace HexMex.Game
{
    public abstract class Structure
    {
        protected Structure(HexagonCornerPosition position)
        {
            Position = position;
        }

        public HexagonCornerPosition Position { get; }
    }
}