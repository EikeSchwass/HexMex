using System;
using System.Linq;

namespace HexMex.Game
{
    public class HexagonRevealer
    {
        public GameplaySettings GameplaySettings { get; }
        private HexagonManager HexagonManager { get; }

        public HexagonRevealer(HexagonManager hexagonManager, GameplaySettings gameplaySettings)
        {
            HexagonManager = hexagonManager;
            GameplaySettings = gameplaySettings;
        }

        public Hexagon GenerateHexagonAt(HexagonPosition hexagonPosition)
        {
            var alreadyRevealed = HexagonManager.Count();
            Hexagon hexagon = null;

            if (hexagonPosition == HexagonPosition.Zero)
                hexagon = new Hexagon(ResourceType.DiamondOre, GetPayoutIntervalFor(ResourceType.DiamondOre), hexagonPosition);
            else if (alreadyRevealed == 1)
                hexagon = new Hexagon(ResourceType.PureWater, GetPayoutIntervalFor(ResourceType.PureWater), hexagonPosition);
            else if (alreadyRevealed == 2)
                hexagon = new Hexagon(ResourceType.CoalOre, GetPayoutIntervalFor(ResourceType.CoalOre), hexagonPosition);
            else
            {
                var waterProbability = GameplaySettings.WaterSigmoid(hexagonPosition.DistanceToOrigin);
                if (HexMexRandom.NextDouble() < waterProbability)
                    hexagon = new Hexagon(ResourceType.PureWater, 0, hexagonPosition);
                else
                {
                    var resourceType = GetNextResourceType();
                    var dieSum = GetPayoutIntervalFor(resourceType);
                    hexagon = new Hexagon(resourceType, dieSum, hexagonPosition);
                }
            }
            return hexagon;
        }

        private float GetPayoutIntervalFor(ResourceType resourceType)
        {
            var spawnInfo = GameplaySettings.SpawnInformation[resourceType];
            return spawnInfo.PayoutInterval;
        }

        private ResourceType GetNextResourceType()
        {
            double total = GameplaySettings.SpawnInformation.Values.Sum(s => s.SpawnProbability);
            double p = HexMexRandom.NextDouble() * total;
            double sum = 0;
            foreach (var kvp in GameplaySettings.SpawnInformation)
            {
                sum += kvp.Value.SpawnProbability;
                if (sum >= p)
                    return kvp.Key;
            }
            throw new IndexOutOfRangeException("Never in a thousand years should this happen");
        }
    }
}