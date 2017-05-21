using System;
using System.Collections;
using System.Collections.Generic;
using CocosSharp;
using HexMex.Game.Settings;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Game
{
    public class HexagonManager : IEnumerable<Hexagon>, ICCUpdatable
    {
        public event Action<HexagonManager, Hexagon> HexagonRevealed;
        public GameplaySettings GameplaySettings { get; }

        public Hexagon this[HexagonPosition hexagonPosition] => GetHexagonAtPosition(hexagonPosition);
        private HexagonRevealer HexagonRevealer { get; }

        private Dictionary<HexagonPosition, Hexagon> Hexagons { get; } = new Dictionary<HexagonPosition, Hexagon>();

        private float PassedTime { get; set; }

        public HexagonManager(GameplaySettings gameplaySettings)
        {
            GameplaySettings = gameplaySettings;
            HexagonRevealer = new HexagonRevealer(this, gameplaySettings);
        }

        public bool CanReachThrough(HexagonPosition pos1, HexagonPosition pos2)
        {
            var h1 = this[pos1];
            var h2 = this[pos2];
            if (h1 == null || h2 == null)
                return false;

            if (h1.ResourceType.IsPassable() || h2.ResourceType.IsPassable())
                return true;
            return false;
        }

        public IEnumerable<Hexagon> GetAdjacentHexagons(HexagonNode position)
        {
            var hexagon1 = GetHexagonAtPosition(position.Position1);
            var hexagon2 = GetHexagonAtPosition(position.Position2);
            var hexagon3 = GetHexagonAtPosition(position.Position3);
            if (hexagon1 != null)
                yield return hexagon1;
            if (hexagon2 != null)
                yield return hexagon2;
            if (hexagon3 != null)
                yield return hexagon3;
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
#if DEBUG
            dt *= 2;
#endif
            PassedTime += dt;
            float payoutBoost = InitialDiamondPayoutFactor(PassedTime);
            foreach (var hexagon in this)
            {
                hexagon.Update(dt * payoutBoost);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private float InitialDiamondPayoutFactor(float passedTime)
        {
            double maxFactor = GameplaySettings.MaxPayoutBoost;
            double payoutSpan = GameplaySettings.PayoutBoostTime;
            if (passedTime > payoutSpan)
                return 1;
            double t = passedTime;
            var result = (-1 / (1 + Exp(-10 / payoutSpan * (t - payoutSpan / 2))) + 1) * (maxFactor - 1) + 1;
            return (float)result;
        }
    }
}