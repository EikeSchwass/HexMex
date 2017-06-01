namespace HexMex.Game.Settings
{
    public class VisualSettings
    {
        public float HexagonOuterBorderThickness { get; } = 2;
        public float HexagonInnerBorderThickness { get; } = 2;
        public float StructureBorderThickness { get; } = 2;
        public ColorCollection ColorCollection { get; } = new DarkerColors();
        public float PlusCrossRadius { get; } = 4;
        public float DiceAnimationTime { get; } = 1f;
        public float DiceAnimationSize { get; } = 171;
        public int BuildMenuButtonsPerRow { get; } = 3;
        public int BuildMenuButtonFontSize { get; } = 18;
        public int BuildMenuButtonMargin { get; } = 4;
        public float BuildMenuButtonBorderThickness { get; } = 1;
        public float BuildMenuBorderThickness { get; } = 1;
        public float BuildingRadius { get; } = 48;
        public float ConstructionRadius { get; } = 32;
        public int BuildButtonRadius { get; } = 48;
        public float EdgeThickness { get; } = 6;
        public float ResourcePackageRadius { get; } = 24;
        public float ProgressRadiusFactor { get; } = 0.75f;
        public int StructureMenuHeaderFontSize { get; } = 24;
        public int StructureMenuDescriptionFontSize { get; } = 18;
        public int StructureMenuFooterFontSize { get; } = 24;
    }
}