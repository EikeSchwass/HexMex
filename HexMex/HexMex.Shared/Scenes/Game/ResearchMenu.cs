using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class ResearchMenu : Menu
    {
        private ResearchMenuEntry selectedReseachMenuEntry;
        public BuildingDescriptionDatabase BuildingDescriptionDatabase { get; }
        public UnlockManager UnlockManager { get; }
        public LanguageSettings LanguageSettings { get; }

        private CCSprite Knowledge1Sprite { get; }
        private CCSprite Knowledge2Sprite { get; }
        private CCSprite Knowledge3Sprite { get; }

        private bool IsUnlockButtonPressed { get; set; }
        private ResearchMenuEntry SelectedReseachMenuEntry
        {
            get => selectedReseachMenuEntry;
            set
            {
                if (selectedReseachMenuEntry != null)
                    selectedReseachMenuEntry.IsSelected = false;
                selectedReseachMenuEntry = value;
                if (selectedReseachMenuEntry != null)
                    selectedReseachMenuEntry.IsSelected = true;
                Render();
            }
        }
        private ResearchMenuEntry[] ResearchMenuEntries { get; set; }
        private CCRect UnlockArea { get; set; }

        public ResearchMenu(BuildingDescriptionDatabase buildingDescriptionDatabase, UnlockManager unlockManager, VisualSettings visualSettings, LanguageSettings languageSettings) : base(visualSettings)
        {
            BuildingDescriptionDatabase = buildingDescriptionDatabase;
            UnlockManager = unlockManager;
            LanguageSettings = languageSettings;
            Knowledge1Sprite = new CCSprite("research.png") { Color = visualSettings.ColorCollection.Knowledge1 };
            Knowledge2Sprite = new CCSprite("research.png") { Color = visualSettings.ColorCollection.Knowledge2 };
            Knowledge3Sprite = new CCSprite("research.png") { Color = visualSettings.ColorCollection.Knowledge3 };
        }

        public override void TouchCancel(CCPoint position, TouchCancelReason reason)
        {
            base.TouchCancel(position, reason);
            IsUnlockButtonPressed = false;
            foreach (var researchMenuEntry in ResearchMenuEntries)
            {
                researchMenuEntry.IsPressed = false;
            }
            Render();
        }

        public override void TouchDown(CCPoint position)
        {
            base.TouchDown(position);
            IsUnlockButtonPressed = UnlockArea.ContainsPoint(position);
            foreach (var researchMenuEntry in ResearchMenuEntries)
            {
                researchMenuEntry.IsPressed = researchMenuEntry.Area.ContainsPoint(position);
            }
            Render();
        }
        public override void TouchUp(CCPoint position)
        {
            base.TouchUp(position);
            if (IsUnlockButtonPressed && UnlockArea.ContainsPoint(position) && SelectedReseachMenuEntry != null)
            {
                if (UnlockManager.GlobalResourceManager.EnoughKnowledgeFor(SelectedReseachMenuEntry.BuildingDescription.UnlockCost))
                {
                    UnlockManager.Unlock(SelectedReseachMenuEntry.BuildingDescription);
                    ReloadBuildingDescriptions();
                }
            }

            foreach (var researchMenuEntry in ResearchMenuEntries)
            {
                if (researchMenuEntry.IsPressed && researchMenuEntry.Area.ContainsPoint(position))
                {
                    SelectedReseachMenuEntry = researchMenuEntry;
                }
                researchMenuEntry.IsPressed = false;
            }

            IsUnlockButtonPressed = false;
            Render();
        }

        protected override void OnAddedToScene()
        {
            base.OnAddedToScene();
            ReloadBuildingDescriptions();
        }

        private BuildingDescription[] GetLockedBuildingDescriptions()
        {
            return BuildingDescriptionDatabase.BuildingDescriptions.Where(bd => !UnlockManager[bd]).OrderBy(bd => bd.UnlockCost).ThenBy(bd => bd.VerbalStructureDescription.NameID.Translate(LanguageSettings)).ToArray();
        }

        private void ReloadBuildingDescriptions()
        {
            var researchMenuEntries = new List<ResearchMenuEntry>();
            var buildingDescriptions = GetLockedBuildingDescriptions();
            float margin = VisualSettings.ResearchMenuButtonMargin;
            int buttonsPerRow = VisualSettings.ResearchMenuButtonsPerRow;
            var fontSize = VisualSettings.ResearchMenuButtonFontSize;
            float buttonWidth = ClientArea.Size.Width / buttonsPerRow - margin * 2;
            var buttonHeight = fontSize * 3f - margin * 2;
            for (int i = 0; i < buildingDescriptions.Length; i++)
            {
                var buildingDescription = buildingDescriptions[i];
                int x = i % buttonsPerRow;
                int y = i / buttonsPerRow;

                float posX = ClientArea.Size.Width / buttonsPerRow * x;
                posX += ClientArea.Size.Width / buttonsPerRow / 2;
                float posY = -(buttonHeight + margin * 2) * y;
                posY -= (buttonHeight + margin * 2) / 2;
                var area = new CCRect(posX - buttonWidth / 2, posY - buttonHeight / 2, buttonWidth, buttonHeight);
                var researchMenuEntry = new ResearchMenuEntry(this, buildingDescription, area);
                researchMenuEntries.Add(researchMenuEntry);
            }
            ResearchMenuEntries = researchMenuEntries.ToArray();
            SelectedReseachMenuEntry = ResearchMenuEntries.FirstOrDefault();
            if (SelectedReseachMenuEntry != null)
                SelectedReseachMenuEntry.IsSelected = true;
            Render();
        }

        private void Render()
        {
            var colorCollection = VisualSettings.ColorCollection;
            DrawNode.Clear();
            DrawNode.DrawRect(ClientArea.Size.Center.InvertY, ClientArea.Size, colorCollection.ResearchMenuBackground, 1, colorCollection.ResearchMenuBorder);

            foreach (var researchMenuEntry in ResearchMenuEntries)
            {
                researchMenuEntry.Render();
            }

            var descriptionFontSize = VisualSettings.ResearchDescriptionFontSize;

            if (SelectedReseachMenuEntry == null)
                return;

            // Unlock Button
            var unlockFontSize = VisualSettings.ResearchUnlockButtonFontSize;
            var unlockAreaHeight = ClientArea.Size.Height / 6;
            var fontColor = IsUnlockButtonPressed ? colorCollection.ResearchUnlockButtonIsPressedForeground : colorCollection.ResearchUnlockButtonIsReleasedForeground;
            UnlockArea = new CCRect(0, -ClientArea.Size.Height, ClientArea.Size.Width, unlockAreaHeight);

            var unlockCost = SelectedReseachMenuEntry.BuildingDescription.UnlockCost;
            fontColor = !UnlockManager.GlobalResourceManager.EnoughKnowledgeFor(unlockCost) ? colorCollection.ResearchButtonDisabledForeground : fontColor;
            DrawNode.DrawRect(UnlockArea.Center, UnlockArea.Size, colorCollection.ResearchButtonBackground, 1, colorCollection.ResearchButtonBorder);
            DrawNode.DrawText(UnlockArea.Center, LanguageSettings.GetByKey(new TranslationKey("unlock")), Font.ArialFonts[unlockFontSize], UnlockArea.Size, fontColor);

            // UnlockCost
            var unlockCostArea = new CCRect(0, -ClientArea.Size.Height + unlockAreaHeight, ClientArea.Size.Width, ClientArea.Size.Height / 16);
            var unlockCostFontSize = VisualSettings.ResearchUnlockCostFontSize;
            var margin = 10 * Font.FontScaleFactor;
            var spacing = 150 * Font.FontScaleFactor;
            var radius = unlockCostArea.Size.Height / 2f - margin;
            var actualWidth = margin + spacing * 3;

            DrawNode.DrawRect(unlockCostArea.Center, unlockCostArea.Size, colorCollection.ResearchUnlockCostRectBackground, 1, colorCollection.ResearchUnlockCostRectBorder);

            DrawNode.DrawCircle(new CCPoint(margin + radius + spacing * 0 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y), radius, colorCollection.ResearchUnlockCostBackground, 1, colorCollection.Knowledge1.ToColor4B());
            DrawNode.DrawCircle(new CCPoint(margin + radius + spacing * 1 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y), radius, colorCollection.ResearchUnlockCostBackground, 1, colorCollection.Knowledge1.ToColor4B());
            DrawNode.DrawCircle(new CCPoint(margin + radius + spacing * 2 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y), radius, colorCollection.ResearchUnlockCostBackground, 1, colorCollection.Knowledge1.ToColor4B());

            DrawNode.DrawText(margin + spacing * 0 + radius * 2 + margin * 4 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, unlockCost.Knowledge1 + "", Font.ArialFonts[unlockCostFontSize], CCSize.Zero);
            DrawNode.DrawText(margin + spacing * 1 + radius * 2 + margin * 4 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, unlockCost.Knowledge2 + "", Font.ArialFonts[unlockCostFontSize], CCSize.Zero);
            DrawNode.DrawText(margin + spacing * 2 + radius * 2 + margin * 4 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, unlockCost.Knowledge3 + "", Font.ArialFonts[unlockCostFontSize], CCSize.Zero);
            DrawNode.DrawTexture("research.png", margin + radius + spacing * 0 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, radius * 1.5f, radius * 1.5f, colorCollection.Knowledge1);
            DrawNode.DrawTexture("research.png", margin + radius + spacing * 1 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, radius * 1.5f, radius * 1.5f, colorCollection.Knowledge2);
            DrawNode.DrawTexture("research.png", margin + radius + spacing * 2 - actualWidth / 2 + unlockCostArea.Center.X, unlockCostArea.Center.Y, radius * 1.5f, radius * 1.5f, colorCollection.Knowledge3);

            // Footer
            var footerHeight = ClientArea.Size.Height / 4;
            var footerArea = new CCRect(0, -ClientArea.Size.Height + unlockAreaHeight + unlockCostArea.Size.Height, ClientArea.Size.Width, footerHeight);
            DrawNode.DrawRect(footerArea.Center, footerArea.Size, colorCollection.ResearchFooterBackground, 1, colorCollection.ResearchFooterBorder);
            var buildingDescriptionText = SelectedReseachMenuEntry.BuildingDescription.VerbalStructureDescription.DescriptionID.Translate(LanguageSettings);
            DrawNode.DrawText(footerArea.Center, buildingDescriptionText, Font.ArialFonts[descriptionFontSize], footerArea.Size);
        }

        private class ResearchMenuEntry
        {
            public bool IsSelected { get; set; }
            public bool IsPressed { get; set; }
            public CCRect Area { get; }
            public ResearchMenu ResearchMenu { get; }
            public BuildingDescription BuildingDescription { get; }

            public ResearchMenuEntry(ResearchMenu researchMenu, BuildingDescription buildingDescription, CCRect area)
            {
                ResearchMenu = researchMenu;
                BuildingDescription = buildingDescription;
                Area = area;
            }

            public void Render()
            {
                var colorCollection = ResearchMenu.VisualSettings.ColorCollection;
                var backColor = IsPressed ? colorCollection.ResearchMenuEntryPressedBackground : colorCollection.ResearchMenuEntryReleasedBackground;
                var borderColor = IsSelected ? colorCollection.ResearchMenuEntrySelectedBorder : colorCollection.ResearchMenuEntryNotSelectedBorder;
                var borderThickness = ResearchMenu.VisualSettings.ResearchMenuButtonBorderThickness;
                ResearchMenu.DrawNode?.DrawRect(Area, backColor, borderThickness, borderColor);
                ResearchMenu.DrawNode?.DrawText(Area.Center, BuildingDescription.VerbalStructureDescription.NameID.Translate(ResearchMenu.LanguageSettings), Font.ArialFonts[ResearchMenu.VisualSettings.ResearchMenuButtonFontSize], Area.Size);
            }
        }
    }
}