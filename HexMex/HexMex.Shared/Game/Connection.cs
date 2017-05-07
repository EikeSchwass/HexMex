namespace HexMex.Game
{
    public class Connection
    {
        public HexagonNode Position1 { get; }
        public HexagonNode Position2 { get; }

        public virtual float MovementSpeedFactor => 0.75f;

        public Connection(HexagonNode position1, HexagonNode position2)
        {
            Position1 = position1;
            Position2 = position2;
        }


    }
}