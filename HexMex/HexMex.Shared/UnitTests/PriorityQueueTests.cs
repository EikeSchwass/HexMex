using System;
using System.Diagnostics;
using System.Linq;
using HexMex.Game;
using HexMex.Game.Buildings;
using Priority_Queue;

namespace HexMex.UnitTests
{
    [TestClass]
    public class PriorityQueueTests
    {
        [TestMethod]
        public void OrderTest()
        {
            var queue = new ResourcePriorityQueue<string, int>((r, p) => true);
            for (int i = 0; i < 100; i++)
            {
                var random = HexMexRandom.Next(5);
                queue.Enqueue($"Element #{i}, Prio:{random}", (RequestPriority)random);
            }
            var results = queue.Reverse().ToArray();
            int currentPrio = Int32.MaxValue;
            for (int i = 0; i < results.Length; i++)
            {
                Debug.WriteLine(results[i]);
                int prio = Convert.ToInt32(results[i].Split(':')[1]);
                if (prio > currentPrio)
                    throw new AssertException($"{nameof(SimplePriorityQueue<string, int>)} has wrong priority order");
                currentPrio = prio;

            }
        }
    }
}