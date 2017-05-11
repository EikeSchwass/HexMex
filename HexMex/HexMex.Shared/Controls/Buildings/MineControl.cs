using CocosSharp;
using HexMex.Game.Buildings;

namespace HexMex.Controls.Buildings
{
    public class MineControl : StructureControl
    {
        public float Radius { get; }

        public MineControl(MineBuilding mineBuilding, float radius) : base(mineBuilding.Position, mineBuilding)
        {
            Radius = radius;
            DrawNode = new CCDrawNode();
            RootNode.AddChild(DrawNode);
            MineBuilding = mineBuilding;
        }

        private MineBuilding MineBuilding { get; }

        public CCDrawNode DrawNode { get; }

        public override void OnRequiresRedraw(Structure structure)
        {
            base.OnRequiresRedraw(structure);
            DrawNode.Clear();
            DrawNode.DrawSolidCircle(CCPoint.Zero, Radius, CCColor4B.Orange);
        }
    }
}