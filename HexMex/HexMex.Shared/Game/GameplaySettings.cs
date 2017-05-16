using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Math;

namespace HexMex.Game
{
    public class GameplaySettings
    {
        public float DefaultResourceTimeBetweenNodes { get; set; } = 1.33f;
        public float DiamondStartAmountFactor { get; set; } =
#if DEBUG
            1;
#else
            0.25f;
#endif
        public float MapSize { get; set; } = 4;
        public float MapSizeStrictness { get; set; } = 1;
        public int MaxNumberOfResourcesInHexagon { get; set; } = 200;

        public GameplaySettings()
        {
            var spawnDictionary = new Dictionary<ResourceType, ResourceSpawnInfo>
            {
                {ResourceType.CoalOre, new ResourceSpawnInfo(1, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.CopperOre, new ResourceSpawnInfo(0.8, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.DiamondOre, new ResourceSpawnInfo(0.05, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.GoldOre, new ResourceSpawnInfo(0.25, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.IronOre, new ResourceSpawnInfo(1.5, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.PureWater, new ResourceSpawnInfo(0.5, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.Stone, new ResourceSpawnInfo(1, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)},
                {ResourceType.Tree, new ResourceSpawnInfo(1.25, MaxNumberOfResourcesInHexagon / 2.0, MaxNumberOfResourcesInHexagon / 10.0)}
            };

            SpawnInformation = new ReadOnlyDictionary<ResourceType, ResourceSpawnInfo>(spawnDictionary);
        }

        public ReadOnlyDictionary<ResourceType, ResourceSpawnInfo> SpawnInformation { get; }

        public ResourceType[] SpawnResourceTypes => SpawnInformation.Keys.ToArray();

        public double WaterSigmoid(int x)
        {
            double value = 1 / (1 + Exp(-MapSizeStrictness * (x - MapSize)));
            return value;
        }
    }
}