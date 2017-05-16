using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace HexMex.Game
{
    public static class HexMexRandom
    {
        static int seed = Environment.TickCount;

        private static ThreadLocal<Random> Random { get; } = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

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

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public static double GetNextGaussian(double mean, double deviation, double min = double.MinValue, double max = double.MaxValue)
        {
            double v1, v2, s;
            do
            {
                v1 = 2 * NextDouble() - 1;
                v2 = 2 * NextDouble() - 1;
                s = v1 * v1 + v2 * v2;
            }
            while (s >= 1 || s == 0);
            s = Math.Sqrt(-2 * Math.Log(s) / s);
            s = v1 * s;
            s = mean + s * deviation;
            if (s <= min || s >= max)
                return GetNextGaussian(mean, deviation, min, max);
            return (int)s;
        }
    }
}