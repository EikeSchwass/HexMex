namespace HexMex.Game
{
    public class Connection
    {
        public HexagonCornerPosition Position1 { get; }
        public HexagonCornerPosition Position2 { get; }

        public virtual float MovementSpeedFactor => 0.75f;

        public Connection(HexagonCornerPosition position1, HexagonCornerPosition position2)
        {
            Position1 = position1;
            Position2 = position2;
        }


    }
}