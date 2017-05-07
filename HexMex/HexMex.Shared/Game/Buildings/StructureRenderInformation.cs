using CocosSharp;

namespace HexMex.Game.Buildings
{
    public class StructureRenderInformation
    {
        private CCColor4B backgroundColor = ColorCollection.DefaultStructureBackgroundColor;
        private CCColor4B borderColor = ColorCollection.DefaultStructureBorderColor;
        private float hexagonRadius = 64;

        public StructureRenderInformation(Structure structure)
        {
            Structure = structure;
        }

        public CCColor4B BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Structure.OnRequiresRedraw();
            }
        }

        public CCColor4B BorderColor
        {
            get => borderColor;
            set
            {
                borderColor = value;
                Structure.OnRequiresRedraw();
            }
        }

        public float HexagonRadius
        {
            get => hexagonRadius;
            set
            {
                hexagonRadius = value;
                Structure.OnRequiresRedraw();
            }
        }

        public Structure Structure { get; }
    }
}