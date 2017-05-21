namespace HexMex.Game.Settings
{
    public class GameSettings
    {
        public GameplaySettings GameplaySettings { get; }
        public LayoutSettings LayoutSettings { get; }
        public VisualSettings VisualSettings { get; }

        public GameSettings(LayoutSettings layoutSettings, VisualSettings visualSettings, GameplaySettings gameplaySettings)
        {
            LayoutSettings = layoutSettings;
            VisualSettings = visualSettings;
            GameplaySettings = gameplaySettings;
        }

        public GameSettings() : this(new LayoutSettings(), new VisualSettings(), new GameplaySettings())
        {
        }
    }
}