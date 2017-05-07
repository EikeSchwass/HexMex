using System;

namespace HexMex.Game
{
    [Flags]
    public enum ResourceType : long
    {
        Nothing = 0,
        Workforce = 1,
        Water = 2,
        Meat = 4,
        Bread = 8,
        Nutrition = Meat | Bread,
        Gold = 16,
        Iron = 32,
        Rainbow = 31,
        Minable = Rainbow | Gold
    }
}