﻿using System;

namespace HexMex.Game
{
    public abstract class Hexagon : IRenderable<Hexagon>
    {
        protected Hexagon(HexagonPosition position)
        {
            Position = position;
        }

        public abstract event Action<Hexagon> RequiresRedraw;

        public HexagonPosition Position { get; }
    }
}