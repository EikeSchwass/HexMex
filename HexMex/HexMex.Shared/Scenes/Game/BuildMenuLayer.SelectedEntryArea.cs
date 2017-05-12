using System;
using CocosSharp;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public partial class BuildMenuLayer
    {
        private class SelectedEntryArea : CCNode
        {
            private bool isConstructButtonPressed;
            private BuildMenuEntry selectedMenuEntry;

            public SelectedEntryArea(float width, float height)
            {
                Width = width;
                Height = height;
            }

            public bool IsConstructButtonPressed
            {
                get => isConstructButtonPressed;
                set
                {
                    isConstructButtonPressed = value;
                    UpdateConstructButton();
                }
            }

            public BuildMenuEntry SelectedMenuEntry
            {
                get => selectedMenuEntry;
                set
                {
                    selectedMenuEntry = value;
                    DisplayMenuEntry(value);
                }
            }

            private MenuLabel ConstructButton { get; set; }

            private float Height { get; }

            private float Width { get; }

            public bool IsPointInConstructButtonBounds(CCTouch touch)
            {
                if (ConstructButton == null)
                    return false;
                var location = ScreenToWorldspace(touch.LocationOnScreen);
                var position = ConstructButton.GetGlobalPosition() - new CCPoint(ConstructButton.Size.Width / 2, ConstructButton.Size.Height / 2);
                var rect = new CCRect(position.X, position.Y, ConstructButton.Size.Width, ConstructButton.Size.Height);
                return rect.ContainsPoint(location);
            }

            private void DisplayMenuEntry(BuildMenuEntry value)
            {
                if (value == null)
                {
                    Visible = false;
                    return;
                }
                Visible = true;
                var buildingInformation = value.Factory.BuildingInformation;
                Children?.Clear();
                var drawNode = new CCDrawNode();
                AddChild(drawNode);

                float columnWidth = Width / (buildingInformation.IsProducer ? 3 : 2);
                float headerHeight = Height / 6;
                float footerHeight = Height / 4;
                float contentHeight = Height - headerHeight - footerHeight;

                drawNode.DrawRect(new CCRect(0, -Height + footerHeight + contentHeight, Width, headerHeight), new CCColor4B(0, 0, 0, 0.75f), 1, CCColor4B.White); // Header
                drawNode.DrawRect(new CCRect(0, -Height + footerHeight, Width, contentHeight), new CCColor4B(0, 0, 0, 0.5f), 1, CCColor4B.White); // Content
                drawNode.DrawRect(new CCRect(0, -Height, Width, footerHeight), new CCColor4B(0, 0, 0, 1f), 1, CCColor4B.White); // Footer
                for (int i = 1; i < 3; i++)
                {
                    var x = i * columnWidth;
                    drawNode.DrawLine(new CCPoint(x, 0), new CCPoint(x, -headerHeight - contentHeight), 1, CCColor4B.White);
                }
                var titleLabel = new MenuLabel("Description", 50f, new CCSize(columnWidth, headerHeight), new CCPoint(columnWidth / 2, -headerHeight / 2));
                var descriptionLabel = new MenuLabel(buildingInformation.Description, 30f, new CCSize(columnWidth, contentHeight), new CCPoint(columnWidth / 2, -headerHeight - contentHeight / 2));
                var constructionHeaderLabel = new MenuLabel("Construction", 50f, new CCSize(columnWidth, headerHeight), new CCPoint(columnWidth / 2 + columnWidth, -headerHeight / 2));
                var constructionLabel = new MenuLabel(buildingInformation.ConstructionCost.GetText() + $"{Environment.NewLine}({buildingInformation.ConstructionTime} s)", 30f, new CCSize(columnWidth, contentHeight), new CCPoint(columnWidth + columnWidth / 2, -headerHeight - contentHeight / 2));
                if (buildingInformation.IsProducer) // Production Column
                {
                    var productionHeaderHeight = contentHeight / 4;
                    var productionContentHeight = (contentHeight - productionHeaderHeight * 3) / 2;
                    var productionHeaderLabel = new MenuLabel("Production", 50f, new CCSize(columnWidth, headerHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight / 2));
                    var ingredientsHeaderLabel = new MenuLabel("Ingredients", 35f, new CCSize(columnWidth, productionHeaderHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight - productionHeaderHeight / 2))
                    {
                        HorizontalAlignment = CCTextAlignment.Left
                    };
                    var productsHeaderLabel = new MenuLabel("Products", 35f, new CCSize(columnWidth, productionHeaderHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight - productionHeaderHeight - productionContentHeight - productionHeaderHeight / 2))
                    {
                        HorizontalAlignment = CCTextAlignment.Left
                    };
                    var productionTimeHeaderLabel = new MenuLabel("Duration: " + buildingInformation.ProductionInformation.ProductionTime + " s", 35f, new CCSize(columnWidth, productionHeaderHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight - productionHeaderHeight * 2 - productionContentHeight * 2 - productionHeaderHeight / 2))
                    {
                        HorizontalAlignment = CCTextAlignment.Left
                    };
                    var ingredientsLabel = new MenuLabel(buildingInformation.ProductionInformation.Ingredients.GetText(), 30f, new CCSize(columnWidth, productionContentHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight - productionHeaderHeight - productionContentHeight / 2));
                    var productsLabel = new MenuLabel(buildingInformation.ProductionInformation.Products.GetText(), 30f, new CCSize(columnWidth, productionContentHeight), new CCPoint(columnWidth * 2 + columnWidth / 2, -headerHeight - productionHeaderHeight * 2 - productionContentHeight - productionContentHeight / 2));

                    AddChild(productionHeaderLabel);
                    AddChild(ingredientsHeaderLabel);
                    AddChild(productsHeaderLabel);
                    AddChild(ingredientsHeaderLabel);
                    AddChild(ingredientsLabel);
                    AddChild(productsLabel);
                    AddChild(productionTimeHeaderLabel);
                }
                AddChild(titleLabel);
                AddChild(descriptionLabel);
                AddChild(constructionHeaderLabel);
                AddChild(constructionLabel);

                ConstructButton = new MenuLabel("CONSTRUCT", 70f, new CCSize(Width, footerHeight), new CCPoint(Width / 2, -Height + footerHeight / 2))
                {
                    Color = CCColor3B.Green
                };
                AddChild(ConstructButton);
                UpdateConstructButton();
            }


            private void UpdateConstructButton()
            {
                if (ConstructButton == null)
                    return;
                var lerp = CCColor4B.Lerp(CCColor4B.Green, CCColor4B.Black, IsConstructButtonPressed ? 0.25f : 0);
                ConstructButton.Color = new CCColor3B(lerp.R, lerp.G, lerp.B);
            }

            private class MenuLabel : CCLabel
            {
                public MenuLabel(string text, float fontSize, CCSize size) : this(text, fontSize, size, CCPoint.Zero)
                {
                }

                public MenuLabel(string text, float fontSize, CCSize size, CCPoint position) : base(text, Font.DefaultFontPath, fontSize, size * 1.9f, CCLabelFormat.SystemFont)
                {
                    HorizontalAlignment = CCTextAlignment.Center;
                    VerticalAlignment = CCVerticalTextAlignment.Center;
                    LineBreak = CCLabelLineBreak.Word;
                    Position = position;
                    Size = size;
                }

                public sealed override CCPoint Position
                {
                    get => base.Position;
                    set => base.Position = value;
                }

                public CCSize Size { get; }
            }
        }
    }
}