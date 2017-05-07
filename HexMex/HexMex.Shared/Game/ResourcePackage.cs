using System;
using CocosSharp;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public class ResourcePackage : IRenderable<ResourcePackage>
    {
        public ResourcePackage(ResourceType resourceType, Structure startStructure, Structure destinationStructure)
        {
            ResourceType = resourceType;
            StartStructure = startStructure;
            DestinationStructure = destinationStructure;
        }

        public event Action<ResourcePackage> RequiresRedraw;
        public Structure DestinationStructure { get; }
        public ResourceType ResourceType { get; }

        public Structure StartStructure { get; }

        public CCPoint GetWorldPosition(HexagonNode from, HexagonNode to, float progress, float hexRadius, float hexMargin)
        {
            var startPos = from.GetWorldPosition(hexRadius, hexMargin);
            var nextPos = to.GetWorldPosition(hexRadius, hexMargin);
            var deltaPos = nextPos - startPos;
            var interpoled = deltaPos * progress;
            return startPos + interpoled;
        }
    }
}