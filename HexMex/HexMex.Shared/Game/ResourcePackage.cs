using System;
using System.Linq;
using CocosSharp;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Game
{
    public class ResourcePackage : ICCUpdatable, IRenderable<ResourcePackage>
    {
        public ResourcePackage(ResourceType resourceType, PathFinder pathFinder, EdgeManager edgeManager, Structure startStructure, Structure destinationStructure, ResourceRequest resourceRequest)
        {
            ResourceType = resourceType;
            PathFinder = pathFinder;
            EdgeManager = edgeManager;
            StartStructure = startStructure;
            DestinationStructure = destinationStructure;
            ResourceRequest = resourceRequest;
            Path = PathFinder.FindPath(startStructure.Position, destinationStructure.Position);
            CurrentNode = Path[0];
            NextNode = Path.Length > 1 ? Path[1] : Path[0];
            PathFinder.NodeRemoved += PathsChanged;
        }

        private void PathsChanged(HexagonNode removedNode)
        {
            if (Path.Contains(removedNode))
                Path = PathFinder.FindPath(CurrentNode, DestinationStructure.Position);
        }
        public Structure DestinationStructure { get; }
        public ResourceType ResourceType { get; }
        public PathFinder PathFinder { get; }
        public EdgeManager EdgeManager { get; }

        public Structure StartStructure { get; }
        public ResourceRequest ResourceRequest { get; set; }

        public HexagonNode CurrentNode { get; private set; }
        public HexagonNode NextNode { get; private set; }
        public float Progress { get; private set; }

        public HexagonNode[] Path { get; set; }



        public CCPoint GetWorldPosition(float hexRadius, float hexMargin)
        {
            var startPos = CurrentNode.GetWorldPosition(hexRadius, hexMargin);
            var nextPos = NextNode.GetWorldPosition(hexRadius, hexMargin);
            var deltaPos = nextPos - startPos;
            var interpoled = deltaPos * Progress;
            return startPos + interpoled;
        }

        public void Update(float dt)
        {
            if (CurrentNode == NextNode)
                Progress = 1;
            else
                Progress += dt / EdgeManager.GetTimeForEdge(CurrentNode, NextNode);
            while (Progress >= 1)
            {
                CurrentNode = NextNode;
                if (NextNode == Path.Last())
                {
                    DestinationStructure.OnResourceArrived(this);
                    ArrivedAtDestination?.Invoke(this);
                }
                else
                {
                    NextNode = Path.GetElementAfter(NextNode);
                }
                Progress--;
            }
            RequiresRedraw?.Invoke(this);
        }

        public event Action<ResourcePackage> ArrivedAtDestination;

        public event Action<ResourcePackage> RequiresRedraw;
    }
}