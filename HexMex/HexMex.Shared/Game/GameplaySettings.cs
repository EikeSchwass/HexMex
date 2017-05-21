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
        public float PayoutBoostTime { get; set; } = 300;
        public float MaxPayoutBoost { get; set; } = 3;

        public ReadOnlyDictionary<ResourceType, ResourceSpawnInfo> SpawnInformation { get; }

        public ResourceType[] SpawnResourceTypes => SpawnInformation.Keys.ToArray();

        public GameplaySettings()
        {
            var spawnDictionary = new Dictionary<ResourceType, ResourceSpawnInfo>
            {
                {ResourceType.CoalOre, new ResourceSpawnInfo(1, 5)},
                {ResourceType.CopperOre, new ResourceSpawnInfo(0.8, 7)},
                {ResourceType.DiamondOre, new ResourceSpawnInfo(0.05, 30)},
                {ResourceType.GoldOre, new ResourceSpawnInfo(0.25, 20)},
                {ResourceType.IronOre, new ResourceSpawnInfo(1.1, 6)},
                {ResourceType.PureWater, new ResourceSpawnInfo(0.5,1)},
                {ResourceType.Stone, new ResourceSpawnInfo(1, 6.5f)},
                {ResourceType.Tree, new ResourceSpawnInfo(1.25, 3)}
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