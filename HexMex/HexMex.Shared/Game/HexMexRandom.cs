using System;
using System.Threading;

namespace HexMex.Game
{
    public static class HexMexRandom
    {
        static int seed = Environment.TickCount;

        private static ThreadLocal<Random> Random { get; } =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        public static int Next(int max)
        {
            return Random.Value.Next(max);
        }

        public static int Next(int min, int max)
        {
            return Random.Value.Next(min, max);
        }

        public static double NextDouble()
        {
            return Random.Value.NextDouble();
        }
    }
}