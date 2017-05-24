using System;

namespace HexMex.Game
{
    [Flags]
    public enum ResourceType : long
    {
        None = 0,

        // Primitives
        PureWater = 1 << 0,
        Tree = 1 << 1,
        Stone = 1 << 2,
        CoalOre = 1 << 3,
        CopperOre = 1 << 4,
        IronOre = 1 << 5,
        GoldOre = 1 << 6,
        DiamondOre = 1 << 7,

        // Products
        Gold = 1 << 8,
        Copper = 1 << 9,
        Iron = 1 << 10,
        Wood = 1 << 11,
        Coal = 1 << 12,
        Sand = 1 << 13,
        Brick = 1 << 14,
        Paper = 1 << 15,
        Circuit = 1 << 16,
        Tools = 1 << 17,
        Barrel = 1 << 18,
        Pottasche = 1 << 19,
        Glas = 1 << 20,
        WaterBarrel = 1 << 21,
        Diamond=1<<22,

        // Other
        Knowledge1 = 1 << 23,
        Knowledge2 = 1 << 24,
        Knowledge3 = 1 << 25,
        Energy = 1 << 26,

        // Groups
        Water = PureWater | WaterBarrel,
        Degradeable = CoalOre | Stone | IronOre | CopperOre | GoldOre,
        Anything = (1 << 27) - 1 - Energy
    }
}