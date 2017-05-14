using System;
using System.Linq;
using CocosSharp;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Game
{
    public class ResourcePackage : ICCUpdatable, IRenderable<ResourcePackage>
    {
        public event Action<ResourcePackage> ArrivedAtDestination;
        public event Action<ResourcePackage> RequiresRedraw;
        public event Action<ResourcePackage> StartedMoving;

        public HexagonNode CurrentNode { get; private set; }
        public Structure DestinationStructure { get; set; }
        public EdgeManager EdgeManager { get; }
        public HexagonNode NextNode { get; private set; }
        public HexagonNode[] Path { get; private set; }
        public PathFinder PathFinder { get; }
        public ResourceRequestState ResourceRequestState { get; private set; } = ResourceRequestState.Pending;
        public ResourceType ResourceType { get; private set; }
        public Structure StartStructure { get; set; }

        private float Progress { get; set; }

        public ResourcePackage(ResourceType resourceType, PathFinder pathFinder, EdgeManager edgeManager)
        {
            ResourceType = resourceType;
            PathFinder = pathFinder;
            EdgeManager = edgeManager;
            PathFinder.NodeRemoved += PathsChanged;
        }

        public CCPoint GetWorldPosition(float hexRadius, float hexMargin)
        {
            var startPos = CurrentNode.GetWorldPosition(hexRadius, hexMargin);
            var nextPos = NextNode.GetWorldPosition(hexRadius, hexMargin);
            var deltaPos = nextPos - startPos;
            var interpoled = deltaPos * Progress;
            return startPos + interpoled;
        }

        public void Move()
        {
            if (StartStructure == null || DestinationStructure == null)
                throw new InvalidOperationException("The start and destination structures have to be set before the package can start moving");
            if (ResourceRequestState != ResourceRequestState.Pending)
                throw new InvalidOperationException($"{nameof(Move)} can only be called, if the the {nameof(Game.ResourceRequestState)} is still {nameof(ResourceRequestState.Pending)}");
            Path = PathFinder.FindPath(StartStructure.Position, DestinationStructure.Position);
            CurrentNode = Path[0];
            NextNode = Path.Length > 1 ? Path[1] : Path[0];
            ResourceRequestState = ResourceRequestState.OnItsWay;
            StartedMoving?.Invoke(this);
        }

        public void SpecifyResourceType(ResourceType resourceType)
        {
            ResourceType = resourceType & ResourceType;
            RequiresRedraw?.Invoke(this);
        }

        public void Update(float dt)
        {
            if (ResourceRequestState == ResourceRequestState.Pending || ResourceRequestState == ResourceRequestState.Completed)
                return;
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

        private void PathsChanged(HexagonNode removedNode)
        {
            if (DestinationStructure != null)
                Path = PathFinder.FindPath(StartStructure.Position, DestinationStructure.Position);
        }
    }
}