using System;
using System.Linq;
using CocosSharp;

namespace HexMex.Game
{
    public class DiceManager : ICCUpdatable
    {
        public event Action<DiceManager, DiceThrowResult> NewDiceThrowResult;
        public float DiceThrowInterval { get; }
        public DiceThrowResult LastDiceThrowResult { get; private set; }
        public float TimeSinceLastDiceThrow { get; private set; }
        public World World { get; }
        public double DiceThrowCount { get; private set; } = 1;

        public DiceManager(World world)
        {
            World = world;
            DiceThrowInterval = World.GameSettings.GameplaySettings.DiceThrowInterval;
        }

        public void Update(float dt)
        {
            TimeSinceLastDiceThrow += dt;
            if (TimeSinceLastDiceThrow > GetDiceThrowInterval())
            {
                TimeSinceLastDiceThrow -= GetDiceThrowInterval();
                ThrowDice();
            }
        }

        private float GetDiceThrowInterval() => DiceThrowInterval;

        private void ThrowDice()
        {
            var dieCount = World.GameSettings.GameplaySettings.DieCount;
            var dieFaceCount = World.GameSettings.GameplaySettings.DieFaceCount;
            int[] results = Enumerable.Repeat(0, dieCount).Select(i => HexMexRandom.Next(1, dieFaceCount + 1)).ToArray();
            DiceThrowCount += World.GameSettings.GameplaySettings.DiamondGuaranteeShrinkage;

            if (World.GameSettings.GameplaySettings.DiamondGuaranteeBase / DiceThrowCount >= HexMexRandom.NextDouble())
                results = Enumerable.Repeat(1, dieCount).ToArray();

            LastDiceThrowResult = new DiceThrowResult(results);
            NewDiceThrowResult?.Invoke(this, LastDiceThrowResult);
            int sum = LastDiceThrowResult.Sum;
            foreach (var structure in World.StructureManager)
            {
                var h1 = World.HexagonManager[structure.Position.Position1];
                var h2 = World.HexagonManager[structure.Position.Position2];
                var h3 = World.HexagonManager[structure.Position.Position3];
                if (h1.PayoutSum == sum || h1.PayoutSum == 0)
                {
                    structure.OnAdjacentHexagonProvidedResource(h1.ResourceType);
                }
                if (h2.PayoutSum == sum || h2.PayoutSum == 0)
                {
                    structure.OnAdjacentHexagonProvidedResource(h2.ResourceType);
                }
                if (h3.PayoutSum == sum || h3.PayoutSum == 0)
                {
                    structure.OnAdjacentHexagonProvidedResource(h3.ResourceType);
                }
            }
        }
    }
}