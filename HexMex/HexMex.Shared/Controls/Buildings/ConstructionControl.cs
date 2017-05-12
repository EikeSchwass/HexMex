using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Controls.Buildings
{
    public class ConstructionControl : StructureControl
    {
        public ConstructionControl(Construction construction, float radius) : base(construction.Position, construction)
        {
            Construction = construction;
            Radius = radius;
            DrawNode = new CCDrawNode();
            RootNode.AddChild(DrawNode);
        }

        private Construction Construction { get; }
        public float Radius { get; }
        private CCDrawNode DrawNode { get; }

        public override void OnRequiresRedraw(Structure structure)
        {
            DrawNode.Clear();
            DrawNode.DrawSolidCircle(CCPoint.Zero, Radius, ColorCollection.ConstructionBackgroundColor);
            DrawNode.DrawSolidArc(CCPoint.Zero, Radius, 0, (float)(Construction.Progress * PI * 2), ColorCollection.ConstructionProgressColor);
            DrawNode.DrawSolidCircle(CCPoint.Zero, Radius * 3 / 4, ColorCollection.ConstructionBackgroundColor);
        }
    }
}