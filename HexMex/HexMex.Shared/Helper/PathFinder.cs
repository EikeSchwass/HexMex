using System;
using System.Collections.Generic;
using Priority_Queue;

namespace HexMex.Helper
{
    public delegate IEnumerable<T> AdjacentNodesDelegate<T>(T node);

    public delegate TCost CostOfEdgeDelegate<in TNode, out TCost>(TNode node1, TNode node2);

    public delegate TCost CostAdditionDelegate<TCost>(TCost cost1, TCost cost2);

    public delegate TCost HeuristikDelegate<in TNode, out TCost>(TNode node, TNode destination);

    public static class PathFinding
    {
        /// <summary>
        /// Finds the shortest path between a given start and destination node using the A*-Algorithm. The algorithm is not restricted to grid-based graphs, but works for arbitrary graphs as well.
        /// </summary>
        /// <typeparam name="TNode">The type of each node. Be aware that the type is used as a key in dictionaries. Make sure GetHashCode() and Equals() are overwritten correctly.</typeparam>
        /// <typeparam name="TCost">The type of the costs of the path. It is recommomanded to use value types for this, but any kind of object will work. If you use a reference type and change the value during the execution of this function the results are unpredictable.</typeparam>
        /// <param name="start">The start node.</param>
        /// <param name="destination">The destination node.</param>
        /// <param name="costAddition">A function that adds to costs together and returns the result. Make sure to not actually change the value of each cost-object.</param>
        /// <param name="adjacentNodes">A function that returns all adjacent nodes for a given node.</param>
        /// <param name="heuristik">A function that estimates the cost of the given node to the destination node. Make sure to never overestimate the cost, since the algorithm may not find the optimal solution in that case. Furthermore the costs may not be negative.</param>
        /// <param name="edgeCost">A function that returns the actual costs of an edge between to given adjacent nodes.</param>
        /// <returns>A path of nodes from the start node to the destination node. The start node as well as the destination node are included and the start node is the first element (A* usually returns the path in inverted order).</returns>
        public static List<TNode> AStar<TNode, TCost>(TNode start, TNode destination, CostAdditionDelegate<TCost> costAddition, AdjacentNodesDelegate<TNode> adjacentNodes, HeuristikDelegate<TNode, TCost> heuristik, CostOfEdgeDelegate<TNode, TCost> edgeCost) where TCost : IComparable<TCost>
        {
            Dictionary<TNode, TCost> g = new Dictionary<TNode, TCost>();
            Dictionary<TNode, TNode> predecessor = new Dictionary<TNode, TNode>();

            List<TNode> closedList = new List<TNode>();
            SimplePriorityQueue<TNode, TCost> openList = new SimplePriorityQueue<TNode, TCost>();
            openList.Enqueue(start, default(TCost));

            do
            {
                var currentNode = openList.Dequeue();
                if (Equals(currentNode, destination))
                {
                    List<TNode> path = new List<TNode>();
                    var node = destination;
                    do
                    {
                        path.Add(node);
                        node = predecessor[node];
                    }
                    while (!Equals(node, start));
                    path.Add(start);
                    path.Reverse();
                    return path;
                }
                closedList.Add(currentNode);

                foreach (var successor in adjacentNodes(currentNode))
                {
                    if (closedList.Contains(successor))
                        continue;
                    TCost costOfCurrentNode = g.Get(currentNode);
                    TCost costFromCurrentToSuccessor = edgeCost(currentNode, successor);
                    TCost costToSuccessor = costAddition(costOfCurrentNode, costFromCurrentToSuccessor);

                    if (openList.Contains(successor) && costToSuccessor.CompareTo(g.Get(successor)) >= 0)
                        continue;
                    predecessor.Set(successor, currentNode);
                    g.Set(successor, costToSuccessor);

                    TCost estimatedCostToDestination = costAddition(costToSuccessor, heuristik(successor, destination));
                    if (openList.Contains(successor))
                        openList.UpdatePriority(successor, estimatedCostToDestination);
                    else
                        openList.Enqueue(successor, estimatedCostToDestination);
                }
            }
            while (openList.Count > 0);

            throw new NoPathFoundException<TNode>($"No Path was found from: {start} to {destination}", start, destination);
        }

        private static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.ContainsKey(key))
                dictionary.Add(key, default(TValue));
            return dictionary[key];
        }

        private static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (!dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
        }
    }
}