using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Math;

namespace HexMex.Game
{
    public class GameplaySettings
    {
        public float DefaultResourceTimeBetweenNodes { get; set; } = 1.33f;
        public float MapSize { get; set; } = 4;
        public float MapSizeStrictness { get; set; } = 1;
        public int DieCount { get; set; } = 2;
        public int DieFaceCount { get; set; } = 6;
        public float DiceThrowInterval { get; set; } = 3;
        public double DiamondGuaranteeShrinkage { get; set; } = 0.1;
        public double DiamondGuaranteeBase { get; set; } = 1.5;

        public ReadOnlyDictionary<ResourceType, ResourceSpawnInfo> SpawnInformation { get; }

        public ResourceType[] SpawnResourceTypes => SpawnInformation.Keys.ToArray();

        public GameplaySettings()
        {
            var probabilities = HexMexRandom.CalculateDieProbabilities(DieCount, DieFaceCount);
            var maxProbability = probabilities.Values.Max();
            var spawnDictionary = new Dictionary<ResourceType, ResourceSpawnInfo>
            {
                {ResourceType.CoalOre, new ResourceSpawnInfo(1, 0.8 * maxProbability, maxProbability / 8)},
                {ResourceType.CopperOre, new ResourceSpawnInfo(0.8, 0.8 * maxProbability, maxProbability / 8)},
                {ResourceType.DiamondOre, new ResourceSpawnInfo(0.05, 0.05 * maxProbability, maxProbability / 8)},
                {ResourceType.GoldOre, new ResourceSpawnInfo(0.25, 0.25 * maxProbability, maxProbability / 8)},
                {ResourceType.IronOre, new ResourceSpawnInfo(1.1, 1.1 * maxProbability, maxProbability / 8)},
                {ResourceType.PureWater, new ResourceSpawnInfo(0.5, 1, 0)},
                {ResourceType.Stone, new ResourceSpawnInfo(1, 1 * maxProbability, maxProbability / 8)},
                {ResourceType.Tree, new ResourceSpawnInfo(1.25, 1.25 * maxProbability, maxProbability / 8)}
            };

            SpawnInformation = new ReadOnlyDictionary<ResourceType, ResourceSpawnInfo>(spawnDictionary);
        }

        public double WaterSigmoid(int x)
        {
            double value = 1 / (1 + Exp(-MapSizeStrictness * (x - MapSize)));
            return value;
        }
    }
}