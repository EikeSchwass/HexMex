using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Game.Buildings
{
    public abstract class Structure : IRenderable<Structure>, ICCUpdatable
    {
        protected Structure(HexagonNode position, World world, IEnumerable<ResourceType> inputs, IEnumerable<ResourceType> outputs)
        {
            Position = position;
            ResourceManager = world.ResourceManager;
            HexagonManager = world.HexagonManager;
            RenderInformation = new StructureRenderInformation(this);
            InputSlots = new FixedSizeDictionary<ResourceSlot, ResourceRequest>(inputs.Select(t => new ResourceSlot(t)));
            OutputSlots = new FixedSizeDictionary<ResourceSlot, ResourceProvision>(outputs.Select(t => new ResourceSlot(t)));
        }

        protected Structure(HexagonNode position, World world) : this(position, world, Enumerable.Empty<ResourceType>(), Enumerable.Empty<ResourceType>())
        {
        }

        public event Action<Structure> RequiresRedraw;

        public HexagonNode Position { get; }

        public RequestPriority Priority { get; set; }
        public StructureRenderInformation RenderInformation { get; }

        public HexagonManager HexagonManager { get; }
        private FixedSizeDictionary<ResourceSlot, ResourceRequest> InputSlots { get; }
        private FixedSizeDictionary<ResourceSlot, ResourceProvision> OutputSlots { get; }

        public ResourceManager ResourceManager { get; }

        public IEnumerable<ResourceSlot> GetInputSlots()
        {
            return InputSlots.Select(i => new ResourceSlot(i.Key.ResourceType)
            {
                HasResource = i.Key.HasResource
            });
        }

        public IEnumerable<ResourceSlot> GetOutputSlots()
        {
            return OutputSlots.Select(o => new ResourceSlot(o.Key.ResourceType)
            {
                HasResource = o.Key.HasResource
            });
        }

        /// <summary>
        /// Get's called everytime a Resource arrives which destination was this Building.
        /// </summary>
        /// <param name="resource">The Resource that arrived.</param>
        public virtual void OnResourceArrived(ResourcePackage resource)
        {
            foreach (var inputSlot in InputSlots)
            {
                if (inputSlot.Key.HasResource)
                    continue;
                if (inputSlot.Value != resource.ResourceRequest)
                    continue;
                inputSlot.Key.HasResource = true;
                InputSlots[inputSlot.Key] = null;
                break;
            }
        }

        /// <summary>
        /// Get's called everytime a Resource passes the Node the Building is located at.
        /// </summary>
        /// <param name="resource">The Resource that passes through.</param>
        public virtual void OnResourcePassesThrough(ResourcePackage resource)
        {
        }

        public virtual void Update(float dt)
        {
            if (InputSlots.All(s => s.Key.HasResource) && OutputSlots.All(s => !s.Key.HasResource))
            {
                if (InputSlots.Any() || OutputSlots.Any())
                {
                    StartProduction();
                    return;
                }
            }
            foreach (var inputSlot in InputSlots.ToList())
            {
                if (!inputSlot.Key.HasResource && inputSlot.Value == null)
                {
                    RequestInputFor(inputSlot);
                }
            }
        }

        protected internal void OnRequiresRedraw()
        {
            RequiresRedraw?.Invoke(this);
        }

        protected virtual bool AcceptsResourceForInput(ResourceType typeOfTheResource, ResourceType actualRequestedResourceType) => typeOfTheResource.CanBeUsedFor(actualRequestedResourceType);

        protected virtual bool CanExtractResourceFromHexagon(ResourceType resourceType) => false;

        protected void OnProductionFinished()
        {
            foreach (var inputSlot in InputSlots.ToArray())
            {
                inputSlot.Key.HasResource = false;
                InputSlots[inputSlot.Key] = null;
            }
            foreach (var outputSlot in OutputSlots.ToArray())
            {
                outputSlot.Key.HasResource = true;

                var resourceProvision = ResourceManager.ProvideResource(outputSlot.Key.ResourceType, this, Priority);
                OutputSlots[outputSlot.Key] = resourceProvision;
                resourceProvision.ProvisionAccepted += sender =>
                                                       {
                                                           outputSlot.Key.HasResource = false;
                                                           OutputSlots[outputSlot.Key] = null;
                                                       };
            }
        }

        protected virtual bool AllowsRequestingResource(ResourceType resourceType) => true;

        protected virtual void StartProduction()
        {
        }

        private void RequestInputFor(KeyValuePair<ResourceSlot, ResourceRequest> inputSlot)
        {
            var adjacentHexagons = HexagonManager.GetAdjacentHexagons(Position);
            foreach (var adjacentHexagon in adjacentHexagons.OfType<ResourceHexagon>())
            {
                if (adjacentHexagon.RemainingResources <= 0)
                    continue;
                if (AcceptsResourceForInput(adjacentHexagon.ResourceType, inputSlot.Key.ResourceType))
                {
                    if (CanExtractResourceFromHexagon(adjacentHexagon.ResourceType))
                    {
                        inputSlot.Key.HasResource = true;
                        adjacentHexagon.RemainingResources--;
                        return;
                    }
                }
            }
            if (AllowsRequestingResource(inputSlot.Key.ResourceType))
            {
                var resourceRequest = ResourceManager.RequestResource(inputSlot.Key.ResourceType, this, Priority);
                InputSlots[inputSlot.Key] = resourceRequest;
            }
        }
    }
}