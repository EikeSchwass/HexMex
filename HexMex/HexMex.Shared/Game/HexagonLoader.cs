namespace HexMex.Game
{
    public class HexagonLoader
    {
        public Hexagon RevealHexagon(HexagonPosition position)
        {
            return new ResourceHexagon(ResourceType.Water, 100, position);
        }
    }
}