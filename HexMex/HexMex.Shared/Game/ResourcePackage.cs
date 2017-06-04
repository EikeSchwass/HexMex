using System;
using CocosSharp;
using HexMex.Game.Settings;

namespace HexMex.Game
{
    public class ResourcePackage : ICCUpdatable, IRenderable<ResourcePackage>
    {
        public event Action<ResourcePackage> ArrivedAtDestination;
        public event Action<ResourcePackage> RequiresRedraw;
        public event Action<ResourcePackage> StartedMoving;

        public HexagonNode CurrentNode { get; private set; }
        public Structure DestinationStructure { get; set; }
        public HexagonNode NextNode { get; private set; }
        public Path Path { get; private set; }
        public CachedPathFinder PathFinder { get; }
        public GameplaySettings GameplaySettings { get; }
        public ResourceRequestState ResourceRequestState { get; private set; } = ResourceRequestState.Pending;
        public ResourceType ResourceType { get; private set; }
        public Structure StartStructure { get; set; }

        private float Progress { get; set; }

        public ResourcePackage(ResourceType resourceType, CachedPathFinder pathFinder, GameplaySettings gameplaySettings)
        {
            ResourceType = resourceType;
            PathFinder = pathFinder;
            GameplaySettings = gameplaySettings;
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
            Path = PathFinder.GetPath(StartStructure.Position, DestinationStructure.Position);
            Path.Invalidated += PathsChanged;
            CurrentNode = Path.Start;
            NextNode = Path.AllHops.Count > 1 ? Path.AllHops[1] : Path.AllHops[0];
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
                Progress += dt / GameplaySettings.DefaultResourceTimeBetweenNodes;
            while (Progress >= 1)
            {
                CurrentNode = NextNode;
                if (NextNode == Path.Destination)
                {
                    DestinationStructure.OnResourceArrived(this);
                    ArrivedAtDestination?.Invoke(this);
                    if (Path != null)
                        Path.Invalidated -= PathsChanged;
                }
                else
                {
                    NextNode = Path.GetElementAfter(NextNode);
                }
                Progress--;
            }
            RequiresRedraw?.Invoke(this);
        }

        private void PathsChanged(Path path, Path newPath)
        {
            if (DestinationStructure != null)
                Path = newPath;
        }
    }
}