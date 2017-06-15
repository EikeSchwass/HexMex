namespace HexMex.Game.Settings
{
    public class GameSettings
    {
        public GameplaySettings GameplaySettings { get; }
        public LayoutSettings LayoutSettings { get; }
        public VisualSettings VisualSettings { get; }
        public BuildingDescriptionDatabase BuildingDescriptionDatabase { get; }
        public LanguageSettings LanguageSettings { get; }

        public GameSettings(LayoutSettings layoutSettings, VisualSettings visualSettings, GameplaySettings gameplaySettings, BuildingDescriptionDatabase buildingDescriptionDatabase, LanguageSettings languageSettings)
        {
            LayoutSettings = layoutSettings;
            VisualSettings = visualSettings;
            GameplaySettings = gameplaySettings;
            BuildingDescriptionDatabase = buildingDescriptionDatabase;
            LanguageSettings = languageSettings;
        }

        public GameSettings(BuildingDescriptionDatabase buildingDescriptionDatabase, ColorCollectionFile colorCollectionFile, LanguageSettings languageSettings) : this(new LayoutSettings(), new VisualSettings(colorCollectionFile), new GameplaySettings(), buildingDescriptionDatabase, languageSettings)
        {
        }
    }
}