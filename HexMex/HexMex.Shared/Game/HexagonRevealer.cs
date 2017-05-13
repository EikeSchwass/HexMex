using System.Linq;

namespace HexMex.Game
{
    public class HexagonRevealer
    {
        private HexagonManager HexagonManager { get; }
        public WorldSettings WorldSettings { get; }

        public HexagonRevealer(HexagonManager hexagonManager, WorldSettings worldSettings)
        {
            HexagonManager = hexagonManager;
            WorldSettings = worldSettings;
        }

        public Hexagon GenerateHexagonAt(HexagonPosition hexagonPosition)
        {
            var worldSettings = WorldSettings;
            var alreadyRevealed = HexagonManager.Count();
            Hexagon hexagon = null;

            if (hexagonPosition == HexagonPosition.Zero)
                hexagon = new ResourceHexagon(ResourceType.Any, (int)(worldSettings.UniversalResourceStartFactor * worldSettings.MaxNumberOfResourcesInHexagon), hexagonPosition);
            else if (alreadyRevealed <= 2)
                hexagon = new ResourceHexagon(ResourceType.Water, worldSettings.MaxNumberOfResourcesInHexagon, hexagonPosition);
            else
            {
                var f = 1.0 / (-hexagonPosition.DistanceToOrigin / WorldSettings.MapSizeFactor) + 1;
                if (HexMexRandom.NextDouble() < f)
                    hexagon = new ResourceHexagon(ResourceType.Water, HexMexRandom.Next(worldSettings.MaxNumberOfResourcesInHexagon + 1), hexagonPosition);
                else
                    hexagon = new ResourceHexagon(ResourceType.None, HexMexRandom.Next(worldSettings.MaxNumberOfResourcesInHexagon + 1), hexagonPosition);
            }
            return hexagon;
        }
    }
}