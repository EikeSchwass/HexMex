using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Math;

namespace HexMex.Game.Settings
{
    public class GameplaySettings
    {
        public float DefaultResourceTimeBetweenNodes { get; set; } = 1.33f;
        public float MapSize { get; set; } = 4;
        public float MapSizeStrictness { get; set; } = 1;
        public float PayoutBoostTime { get; set; } = 300;
        public float MaxPayoutBoost { get; set; } = 3;
        public int StartCO2 { get; set; } = 0;
        public int StartO2 { get; set; } = 1000;
        public int StartEnergy { get; set; } = 0;

        public ReadOnlyDictionary<ResourceType, ResourceSpawnInfo> SpawnInformation { get; }

        public GameplaySettings()
        {
            var spawnDictionary = new Dictionary<ResourceType, ResourceSpawnInfo>
            {
                {ResourceType.CoalOre, new ResourceSpawnInfo(1, 2.5f)},
                {ResourceType.CopperOre, new ResourceSpawnInfo(0.8, 3.5f)},
                {ResourceType.DiamondOre, new ResourceSpawnInfo(0.05, 15)},
                {ResourceType.GoldOre, new ResourceSpawnInfo(0.25, 10)},
                {ResourceType.IronOre, new ResourceSpawnInfo(1.1, 3)},
                {ResourceType.PureWater, new ResourceSpawnInfo(0.5,1)},
                {ResourceType.Stone, new ResourceSpawnInfo(1, 3.25f)},
                {ResourceType.Tree, new ResourceSpawnInfo(1.25, 1.5f)}
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