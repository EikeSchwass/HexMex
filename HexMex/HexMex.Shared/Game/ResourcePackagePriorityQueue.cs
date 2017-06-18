using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HexMex.Game
{
    public class ResourcePriorityQueue<TItem, TFilter> : IEnumerable<TItem> where TItem : class
    {
        private Dictionary<TItem, RequestPriority> Priorities { get; } = new Dictionary<TItem, RequestPriority>();
        private List<TItem> Packages { get; } = new List<TItem>();
        private DequeueFilter Filter { get; }

        public ResourcePriorityQueue(DequeueFilter dequeueFilter)
        {
            Filter = dequeueFilter;
        }

        public void Enqueue(TItem resourcePackage, RequestPriority priority)
        {
            Priorities.Add(resourcePackage, priority);
            if (Packages.Count == 0)
            {
                Packages.Add(resourcePackage);
                return;
            }
            int insertIndex = 0;
            for (; insertIndex < Packages.Count; insertIndex++)
            {
                var currentPriority = Priorities[Packages[insertIndex]];
                if (priority > currentPriority)
                    break;
            }
            Packages.Insert(insertIndex, resourcePackage);
        }

        public TItem Dequeue(TFilter resourceType) => Dequeue(resourceType, rb => true);

        public delegate bool DequeueFilter(TItem item, TFilter passedFilterItem);

        public void UpdatePriority(TItem resourcePackage, RequestPriority newPriority)
        {
            if (Priorities[resourcePackage] == newPriority)
                return;
            Packages.Remove(resourcePackage);
            Enqueue(resourcePackage, newPriority);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return Enumerable.Reverse(Packages).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public TItem Dequeue(TFilter resourceType, Func<TItem, bool> filter)
        {
            TItem resourcePackage = null;
            foreach (TItem package in Packages)
            {
                if (Filter(package, resourceType) && filter(package))
                {
                    resourcePackage = package;
                    break;
                }
            }
            if (resourcePackage != null)
                Packages.Remove(resourcePackage);
            return resourcePackage;
        }
    }
}