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
                hexagon = new ResourceHexagon(ResourceType.DiamondOre, (int)(GameplaySettings.DiamondStartAmountFactor * GameplaySettings.MaxNumberOfResourcesInHexagon), hexagonPosition);
            else if (alreadyRevealed == 1)
                hexagon = new ResourceHexagon(ResourceType.PureWater, GameplaySettings.MaxNumberOfResourcesInHexagon, hexagonPosition);
            else if (alreadyRevealed == 2)
                hexagon = new ResourceHexagon(ResourceType.CoalOre, GameplaySettings.MaxNumberOfResourcesInHexagon, hexagonPosition);
            else
            {
                var waterProbability = GameplaySettings.WaterSigmoid(hexagonPosition.DistanceToOrigin);
                if (HexMexRandom.NextDouble() < waterProbability)
                    hexagon = new ResourceHexagon(ResourceType.Water, GameplaySettings.MaxNumberOfResourcesInHexagon, hexagonPosition);
                else
                {
                    var resourceType = GetNextResourceType();
                    var resourceAmount = GetNextResourceAmount(resourceType);
                    hexagon = new ResourceHexagon(resourceType, resourceAmount, hexagonPosition);
                }
            }
            return hexagon;
        }

        private int GetNextResourceAmount(ResourceType resourceType)
        {
            var spawnInfo = GameplaySettings.SpawnInformation[resourceType];
            var nextGaussian = HexMexRandom.GetNextGaussian(spawnInfo.AmountMean, spawnInfo.AmountDeviation, 0, GameplaySettings.MaxNumberOfResourcesInHexagon + 1);
            return (int)nextGaussian;
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