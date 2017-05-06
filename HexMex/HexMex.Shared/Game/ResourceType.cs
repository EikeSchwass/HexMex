using System;

namespace HexMex.Game
{
    [Flags]
    public enum ResourceType : long
    {
        Workforce = 1,
        Water = 2,
        Meat = 4,
        Bread = 8,
        Nutrition = Meat | Bread,
        Platinum = 16
    }
}