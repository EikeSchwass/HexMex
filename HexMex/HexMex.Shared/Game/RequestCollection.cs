using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HexMex.Helper;

namespace HexMex.Game
{
    public class RequestCollection : ICollection<ResourceRequest.ResourceRequestChanger>
    {
        public int Count => Requests.Count;
        public bool IsReadOnly => false;
        private Dictionary<Type, Resource> Resources { get; } = new Dictionary<Type, Resource>();
        private List<ResourceRequest.ResourceRequestChanger> Requests { get; } = new List<ResourceRequest.ResourceRequestChanger>();

        public void Add(ResourceRequest.ResourceRequestChanger item)
        {
            if (Requests.Count == 0)
            {
                Requests.Add(item);
                return;
            }
            var priority = item.ResourceRequest.Priority;
            int index = 0;
            for (; Requests[index].ResourceRequest.Priority >= priority; index++)
            {
            }
            Requests.Insert(index, item);
        }

        public void Clear()
        {
            Requests.Clear();
        }

        public bool Contains(ResourceRequest.ResourceRequestChanger item) => Requests.Contains(item);

        public void CopyTo(ResourceRequest.ResourceRequestChanger[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ResourceRequest.ResourceRequestChanger> GetEnumerator() => Requests.GetEnumerator();

        public ResourceRequest.ResourceRequestChanger GetNext()
        {
            return Requests.FirstOrDefault(mr => mr.ResourceRequest.RequestState == ResourceRequestState.Pending);
        }

        public ResourceRequest.ResourceRequestChanger GetNextRequestThatWishes<T>() where T : Resource
        {
            return GetNextRequestThatWishes(typeof(T));
        }

        public ResourceRequest.ResourceRequestChanger GetNextRequestThatWishes(Type t)
        {
            var resourceOfType = t.GetResource();
            return Requests.FirstOrDefault(mr => resourceOfType.CanBeUsedFor(mr.ResourceRequest.Type) && mr.ResourceRequest.RequestState == ResourceRequestState.Pending);
        }

        public bool HasPendingRequests()
        {
            return GetNext() != null;
        }

        public bool HasPendingRequests(Type t)
        {
            return GetNextRequestThatWishes(t) != null;
        }

        public bool HasPendingRequests<T>() where T : Resource
        {
            return HasPendingRequests(typeof(T));
        }

        public bool Remove(ResourceRequest.ResourceRequestChanger item) => Requests.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}