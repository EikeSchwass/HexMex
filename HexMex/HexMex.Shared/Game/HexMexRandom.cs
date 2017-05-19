using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace HexMex.Game
{
    public static class HexMexRandom
    {
        static int seed = Environment.TickCount;

        private static ThreadLocal<Random> Random { get; } = new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref seed)));

        private static Dictionary<int, Dictionary<int, Dictionary<int, double>>> DieProbabilityCache { get; } = new Dictionary<int, Dictionary<int, Dictionary<int, double>>>();

        public static double GetNextGaussian(double mean, double deviation, double min = double.MinValue, double max = double.MaxValue)
        {
            double v1, v2, s;
            do
            {
                do
                {
                    v1 = 2 * NextDouble() - 1;
                    v2 = 2 * NextDouble() - 1;
                    s = v1 * v1 + v2 * v2;
                }
                while (s > 1.0001f || s <= 0);
                s = Math.Sqrt(-2 * Math.Log(s) / s);
                s = v1 * s;
                s = mean + s * deviation;
            }
            while (s <= min || s >= max);
            return s;
        }

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

        private static void CalculateDieSums(int sum, int dieCount, int dieFaceCount, IDictionary<int, double> dictionary)
        {
            if (dieCount == 0)
            {
                if (dictionary.ContainsKey(sum))
                    dictionary[sum]++;
                else
                    dictionary.Add(sum, 1);
                return;
            }
            for (int i = 1; i <= dieFaceCount; i++)
            {
                CalculateDieSums(sum + i, dieCount - 1, dieFaceCount, dictionary);
            }
        }

        public static IDictionary<int, double> CalculateDieProbabilities(int dieCount, int dieFaceCount)
        {
            var cachedDieProbabilities = GetCachedDieProbabilities(dieCount, dieFaceCount);
            if (cachedDieProbabilities != null)
                return cachedDieProbabilities;
            Dictionary<int, double> result = new Dictionary<int, double>();
            CalculateDieSums(0, dieCount, dieFaceCount, result);
            double sum = result.Values.Sum();
            foreach (var key in result.Keys.ToArray())
            {
                result[key] /= sum;
            }
            SaveDieProbabilitiesInCache(dieCount, dieFaceCount, result);
            return result;
        }

        private static void SaveDieProbabilitiesInCache(int dieCount, int dieFaceCount, Dictionary<int, double> dictionary)
        {
            if (!DieProbabilityCache.ContainsKey(dieCount))
                DieProbabilityCache.Add(dieCount, new Dictionary<int, Dictionary<int, double>>());
            if (!DieProbabilityCache[dieCount].ContainsKey(dieFaceCount))
                DieProbabilityCache[dieCount].Add(dieFaceCount, dictionary);
        }

        private static Dictionary<int, double> GetCachedDieProbabilities(int dieCount, int dieFaceCount)
        {
            if (!DieProbabilityCache.ContainsKey(dieCount))
                return null;
            if (!DieProbabilityCache[dieCount].ContainsKey(dieFaceCount))
                return null;
            return DieProbabilityCache[dieCount][dieFaceCount];
        }
    }
}