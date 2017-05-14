using System;
using CocosSharp;

namespace HexMex.Game.Buildings
{
    public abstract class Structure : IRenderable<Structure>, ICCUpdatable
    {
        public event Action<Structure> RequiresRedraw;

        public HexagonNode Position { get; }
        public World World { get; }

        protected ResourceDirector ResourceDirector { get; }

        protected Structure(HexagonNode position, World world)
        {
            World = world;
            Position = position;
            ResourceDirector = new ResourceDirector(this);
        }

        /// <summary>
        /// Get's called everytime a Resource arrives which destination was this Building.
        /// </summary>
        /// <param name="resource">The Resource that arrived.</param>
        public void OnResourceArrived(ResourcePackage resource)
        {
            ResourceDirector.ResourceArrived(resource);
        }

        /// <summary>
        /// Get's called everytime a Resource passes the Node the Building is located at.
        /// </summary>
        /// <param name="resource">The Resource that passes through.</param>
        public virtual void OnResourcePassesThrough(ResourcePackage resource)
        {
            ResourceDirector.ResourcePassesThrough(resource);
        }

        public abstract void Render(CCDrawNode drawNode);

        public virtual void Update(float dt)
        {
        }

        protected internal void OnRequiresRedraw()
        {
            RequiresRedraw?.Invoke(this);
        }
    }
}