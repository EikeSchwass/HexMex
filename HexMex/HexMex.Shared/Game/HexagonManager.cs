using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game
{
    public class HexagonManager : IEnumerable<Hexagon>, ICCUpdatable
    {
        public HexagonManager(World world)
        {
            World = world;
            HexagonRevealer = new HexagonRevealer(this);
        }

        public event Action<HexagonManager, Hexagon> HexagonRevealed;

        public Hexagon this[HexagonPosition hexagonPosition] => GetHexagonAtPosition(hexagonPosition);
        public World World { get; }
        private HexagonRevealer HexagonRevealer { get; }

        private Dictionary<HexagonPosition, Hexagon> Hexagons { get; } = new Dictionary<HexagonPosition, Hexagon>();

        public bool CanReachThrough(HexagonPosition pos1, HexagonPosition pos2)
        {
            var h1 = this[pos1];
            var h2 = this[pos2];
            if (h1 == null || h2 == null)
                return false;
            if (h1 is ResourceHexagon resourceHexagon1 && h2 is ResourceHexagon resourceHexagon2)
            {
                if (resourceHexagon1.ResourceType.IsPassable() || resourceHexagon2.ResourceType.IsPassable())
                    return true;
            }
            return false;
        }

        public IEnumerator<Hexagon> GetEnumerator()
        {
            return Hexagons.Values.GetEnumerator();
        }

        public Hexagon GetHexagonAtPosition(HexagonPosition hexagonPosition)
        {
            if (!Hexagons.ContainsKey(hexagonPosition))
                return null;
            return Hexagons[hexagonPosition];
        }

        public Hexagon RevealHexagonAt(HexagonPosition hexagonPosition)
        {
            if (Hexagons.ContainsKey(hexagonPosition))
                throw new InvalidOperationException($"The hexagon was already revealed. Use Indexer or {nameof(GetHexagonAtPosition)} instead.");
            var hexagon = HexagonRevealer.GenerateHexagonAt(hexagonPosition);
            Hexagons.Add(hexagonPosition, hexagon);
            HexagonRevealed?.Invoke(this, hexagon);
            return hexagon;
        }

        public void Update(float dt)
        {
            foreach (var hexagon in this.OfType<ICCUpdatable>())
            {
                hexagon.Update(dt);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}