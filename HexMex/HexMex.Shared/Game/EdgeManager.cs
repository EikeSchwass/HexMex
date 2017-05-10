namespace HexMex.Game
{
    public class EdgeManager
    {
        public WorldSettings WorldSettings { get; }

        public EdgeManager(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
        }

        public float GetTimeForEdge(HexagonNode from, HexagonNode to)
        {
            return WorldSettings.ResourceTimeBetweenNodes;
        }

        public float GetMinTime() => 1;
    }
}