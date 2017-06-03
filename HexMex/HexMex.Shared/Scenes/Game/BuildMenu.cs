using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class BuildMenu : Menu
    {
        private BuildMenuEntry selectedEntry;
        public event Action<BuildMenu, BuildingConstructionFactory> ConstructionRequested;
        private List<BuildMenuEntry> BuildMenuEntries { get; } = new List<BuildMenuEntry>();
        private CCRect ConstructButtonRect { get; set; }
        private bool ConstructButtonIsPressed { get; set; }

        private BuildMenuEntry SelectedEntry
        {
            get => selectedEntry;
            set
            {
                if (selectedEntry != null)
                    selectedEntry.IsSelected = false;
                selectedEntry = value;
                if (selectedEntry != null)
                    selectedEntry.IsSelected = true;
                Render();
            }
        }

        public HexagonNode TargetNode { get; set; }

        public BuildMenu(VisualSettings visualSettings) : base(visualSettings)
        {
        }

        public override void TouchCancel(CCPoint position, TouchCancelReason reason)
        {
            base.TouchCancel(position, reason);
            foreach (var buildMenuEntry in BuildMenuEntries)
            {
                buildMenuEntry.IsPressed = false;
            }
            ConstructButtonIsPressed = false;
            Render();
        }

        public override void TouchDown(CCPoint position)
        {
            base.TouchDown(position);
            foreach (var buildMenuEntry in BuildMenuEntries)
            {
                buildMenuEntry.IsPressed = buildMenuEntry.Position.ContainsPoint(position);
            }
            ConstructButtonIsPressed = ConstructButtonRect.ContainsPoint(position);
            Render();
        }

        public override void TouchUp(CCPoint position)
        {
            base.TouchUp(position);
            foreach (var buildMenuEntry in BuildMenuEntries)
            {
                if (buildMenuEntry.Position.ContainsPoint(position))
                {
                    SelectedEntry = buildMenuEntry;
                }
                buildMenuEntry.IsPressed = false;
            }
            if (ConstructButtonRect.ContainsPoint(position) && ConstructButtonIsPressed)
            {
                Construct();
            }
            else
                ConstructButtonIsPressed = false;
            Render();
        }

        private void Construct()
        {
            Host.Close();
            ConstructionRequested?.Invoke(this, SelectedEntry.Factory);
        }

        protected override void OnAddedToScene()
        {
            DrawNode.Clear();
            base.OnAddedToScene();
            BuildMenuEntries.Clear();
            var factories = BuildingConstructionFactory.Factories.Values.OrderBy(k => k.StructureDescription.Name).ToArray();
            var buttonsPerRow = VisualSettings.BuildMenuButtonsPerRow;
            var fontSize = VisualSettings.BuildMenuButtonFontSize;
            var margin = VisualSettings.BuildMenuButtonMargin;
            var buttonWidth = ClientArea.Size.Width / buttonsPerRow;
            var buttonHeight = fontSize * 2.5f;
            var buttonSize = new CCSize(buttonWidth - margin * 2, buttonHeight - margin * 2);
            for (int i = 0; i < factories.Length; i++)
            {
                var factory = factories[i];
                float centerX = i % buttonsPerRow * buttonWidth + buttonWidth / 2;
                // ReSharper disable once PossibleLossOfFraction
                float centerY = -i / buttonsPerRow * buttonHeight - buttonHeight / 2;
                var rect = new CCRect(centerX - buttonSize.Width / 2, centerY - buttonSize.Height / 2, buttonWidth - margin * 2, buttonHeight - margin * 2);
                BuildMenuEntries.Add(new BuildMenuEntry(factory, rect, this));
            }
            SelectedEntry = BuildMenuEntries.FirstOrDefault(bme => bme.Factory == SelectedEntry?.Factory) ?? BuildMenuEntries.FirstOrDefault();
            Render();
        }

        private void Render()
        {
            if (Host == null)
                return;
            var colorCollection = VisualSettings.ColorCollection;
            DrawNode.Clear();
            DrawNode.DrawRect(ClientArea.Size.Center.InvertY, ClientArea.Size, CCColor4B.Lerp(colorCollection.GrayVeryDark, colorCollection.Transparent, 0.5f), VisualSettings.BuildMenuBorderThickness, colorCollection.White);
            RenderMenuEntries();
            if (SelectedEntry != null)
                RenderSelectedEntryArea();
        }

        private void RenderMenuEntries()
        {
            foreach (var buildMenuEntry in BuildMenuEntries)
            {
                buildMenuEntry.Render();
            }
        }

        private void RenderSelectedEntryArea()
        {
            var colorCollection = VisualSettings.ColorCollection;
            DrawNode.DrawRect(new CCPoint(ClientArea.Size.Width / 2, -ClientArea.Size.Height + ClientArea.Size.Height / 4), new CCSize(ClientArea.Size.Width, ClientArea.Size.Height / 2), colorCollection.GrayVeryDark, 1, colorCollection.White);

            var y = -ClientArea.Size.Height / 2;
            var structureDescription = SelectedEntry.Factory.StructureDescription;
            var newLine = Environment.NewLine;

            float totalHeight = ClientArea.Size.Height / 2;
            float columnWidth = ClientArea.Size.Width / (structureDescription.IsProducer ? 3 : 2);

            var headerHeight = totalHeight / 8;
            var headerSize = new CCSize(columnWidth, headerHeight);

            var contentHeight = totalHeight / 2 + totalHeight / 8;
            var contentSize = new CCSize(columnWidth, contentHeight);

            var footerHeight = totalHeight / 4;
            var footerSize = new CCSize(ClientArea.Size.Width, footerHeight);

            ConstructButtonRect = new CCRect(0, -ClientArea.Size.Height, ClientArea.Size.Width, footerHeight);

            var contentFont = Font.ArialFonts[16];
            var headerFont = Font.ArialFonts[20];

            // --- Description ---
            DrawNode.DrawText(columnWidth * 0.5f, y - headerHeight / 2, "Description", headerFont, headerSize);
            DrawNode.DrawText(columnWidth * 0.5f, y - headerHeight - contentHeight / 2, structureDescription.Description, contentFont, contentSize);

            // --- Construction ---
            DrawNode.DrawText(columnWidth * 1.5f, y - headerHeight / 2, "Construction", headerFont, headerSize);
            DrawNode.DrawText(columnWidth * 1.5f, y - headerHeight - contentHeight / 2, structureDescription.ConstructionCost.GetText() + $"{newLine}({structureDescription.ConstructionTime} s)", contentFont, contentSize);

            if (structureDescription.IsProducer)
            {
                DrawNode.DrawText(columnWidth * 2.5f, y - headerHeight / 2, "Production", headerFont, headerSize);
                DrawNode.DrawText(columnWidth * 2.5f, y - headerHeight - contentHeight / 2, $"- Ingredients -{newLine}{structureDescription.ProductionInformation.Ingredients.GetText()}{newLine}- Products -{newLine}{structureDescription.ProductionInformation.Products.GetText()}{newLine}- Duration -{newLine}{structureDescription.ProductionInformation.ProductionTime} s", contentFont, contentSize);
            }

            DrawNode.DrawText(ClientArea.Size.Width / 2, y - headerHeight - contentHeight - footerHeight / 2, "Construct", Font.ArialFonts[30], footerSize, new CCColor3B(colorCollection.GreenLight));

            DrawNode.DrawRect(new CCPoint(0, y) + new CCSize(ClientArea.Size.Width, -headerHeight).Center, new CCSize(ClientArea.Size.Width, headerHeight), colorCollection.GrayDark, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(0, y) + new CCSize(columnWidth, -headerHeight - contentHeight).Center + new CCPoint(columnWidth, 0) * 0, new CCSize(columnWidth, headerHeight + contentHeight), colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(0, y) + new CCSize(columnWidth, -headerHeight - contentHeight).Center + new CCPoint(columnWidth, 0) * 1, new CCSize(columnWidth, headerHeight + contentHeight), colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(0, y) + new CCSize(columnWidth, -headerHeight - contentHeight).Center + new CCPoint(columnWidth, 0) * 2, new CCSize(columnWidth, headerHeight + contentHeight), colorCollection.Transparent, 1, colorCollection.White);

        }

        private class BuildMenuEntry
        {
            public BuildingConstructionFactory Factory { get; }
            public CCRect Position { get; }
            public BuildMenu BuildMenu { get; }

            public bool IsSelected { get; set; }
            public bool IsPressed { get; set; }

            public BuildMenuEntry(BuildingConstructionFactory factory, CCRect position, BuildMenu buildMenu)
            {
                Factory = factory;
                Position = position;
                BuildMenu = buildMenu;
            }

            public void Render()
            {
                var colorCollection = BuildMenu.VisualSettings.ColorCollection;
                var backColor = IsPressed ? colorCollection.GrayVeryDark : colorCollection.GrayDark;
                var borderColor = IsSelected ? colorCollection.YellowNormal : colorCollection.White;
                var borderThickness = BuildMenu.VisualSettings.BuildMenuButtonBorderThickness;
                BuildMenu.DrawNode?.DrawRect(Position, backColor, borderThickness, borderColor);
                BuildMenu.DrawNode?.DrawText(Position.Center, Factory.StructureDescription.Name, Font.ArialFonts[16], Position.Size);
            }
        }
    }
}