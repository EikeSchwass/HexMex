using CocosSharp;
using HexMex.Game.Buildings;

namespace HexMex.Controls.Buildings
{
    public class VillageControl : StructureControl
    {
        public float Radius { get; }

        public VillageControl(VillageBuilding villageBuilding, float radius) : base(villageBuilding.Position, villageBuilding)
        {
            Radius = radius;
            DrawNode = new CCDrawNode();
            RootNode.AddChild(DrawNode);
            VillageBuilding = villageBuilding;
        }

        private VillageBuilding VillageBuilding { get; }

        public CCDrawNode DrawNode { get; }

        public override void OnRequiresRedraw(Structure structure)
        {
            base.OnRequiresRedraw(structure);
            DrawNode.Clear();
            DrawNode.DrawSolidCircle(CCPoint.Zero, Radius, CCColor4B.Yellow);
        }
    }
}