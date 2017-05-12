using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;

namespace HexMex.Controls.Buildings
{
    public abstract class StructureControl : CCNode
    {
        protected StructureControl(HexagonNode location, Structure structure)
        {
            Location = location;
            RootNode = new CCNode();
            structure.RequiresRedraw += OnRequiresRedraw;
            AddChild(RootNode);
        }

        public HexagonNode Location { get; }
        protected CCNode RootNode { get; }

        public virtual void OnRequiresRedraw(Structure structure)
        {
        }
    }
}