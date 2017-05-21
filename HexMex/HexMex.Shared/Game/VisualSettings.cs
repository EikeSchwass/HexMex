namespace HexMex.Game
{
    public class VisualSettings
    {
        public float HexagonOuterBorderThickness { get; } = 2;
        public float HexagonInnerBorderThickness { get; } = 2;
        public float StructureBorderThickness { get; } = 2;
        public ColorCollection ColorCollection { get; } = new DarkerColors();
        public float PlusCrossRadius { get; } = 8;
        public float DiceAnimationTime { get; } = 1f;
        public float DiceAnimationSize { get; } = 171;
        public int BuildMenuButtonsPerRow { get; } = 3;
        public int BuildMenuButtonFontSize { get; } = 20;
        public int BuildMenuButtonMargin { get; } = 4;
        public float BuildMenuButtonBorderThickness { get; } = 1;
        public float BuildMenuBorderThickness { get; } = 1;
    }
}