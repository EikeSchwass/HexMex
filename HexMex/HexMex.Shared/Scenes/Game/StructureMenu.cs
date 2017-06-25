using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using HexMex.Game.Settings;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class StructureMenu : Menu
    {
        private Structure structure;
        public World World { get; }
        public Structure Structure
        {
            get => structure;
            set
            {
                SuspendEntry = null;
                DeconstructEntry = null;
                if (structure is Construction c1)
                {
                    c1.ConstructionCompleted -= ConstructionCompleted;
                }
                structure = value;
                if (structure is Construction c2)
                {
                    c2.ConstructionCompleted += ConstructionCompleted;
                }
                ReloadResources();
            }
        }

        private StructureMenuResource[] Ingredients { get; set; } = new StructureMenuResource[0];
        private StructureMenuResource[] Products { get; set; } = new StructureMenuResource[0];

        private StructureMenuEntry DeconstructEntry { get; set; }
        private StructureMenuEntry SuspendEntry { get; set; }

        public StructureMenu(VisualSettings visualSettings, World world) : base(visualSettings)
        {
            World = world;
        }

        public override void TouchCancel(CCPoint position, TouchCancelReason reason)
        {
            base.TouchCancel(position, reason);
            if (DeconstructEntry != null)
                DeconstructEntry.IsPressed = false;
            if (SuspendEntry != null)
                SuspendEntry.IsPressed = false;
        }

        public override void TouchDown(CCPoint position)
        {
            base.TouchDown(position);
            if (DeconstructEntry != null)
            {
                DeconstructEntry.IsPressed = PointInRect(position, DeconstructEntry.Area);
            }
            if (SuspendEntry != null)
            {
                SuspendEntry.IsPressed = PointInRect(position, SuspendEntry.Area);
            }
        }

        public override void TouchUp(CCPoint position)
        {
            base.TouchDown(position);
            if (DeconstructEntry?.IsPressed == true)
            {
                Deconstruct();
            }
            if (SuspendEntry?.IsPressed == true)
            {
                Suspend();
            }
        }

        protected override void OnAddedToScene()
        {
            base.OnAddedToScene();
            Host.Schedule(Update, 0.1f);
            Update(0);
        }
        private void ConstructionCompleted(Construction construction)
        {
            Structure = null;
            Host?.Close();
        }
        private void Deconstruct()
        {
            World.StructureManager.RemoveStructure(Structure);
            Host?.Close();
        }

        private bool PointInRect(CCPoint position, CCRect area)
        {
            return area.ContainsPoint(position);
        }

        private void ReloadResources()
        {
            Products = new StructureMenuResource[0];
            Ingredients = new StructureMenuResource[0];
            if (Structure == null)
                return;
            if (Structure?.Description?.ProductionInformation != null)
            {
                var productionInfo = Structure.Description.ProductionInformation;
                var ingredients = productionInfo.Ingredients.ResourceTypes.OrderBy(i => i.ResourceType).ToArray();
                Ingredients = new StructureMenuResource[ingredients.Length];
                for (var i = 0; i < ingredients.Length; i++)
                {
                    var structureMenuResource = new StructureMenuResource(ingredients[i], null);
                    Ingredients[i] = structureMenuResource;
                }
                var products = productionInfo.Products.ResourceTypes.OrderBy(i => i.ResourceType).ToArray();
                Products = new StructureMenuResource[products.Length];
                for (var i = 0; i < products.Length; i++)
                {
                    var structureMenuResource = new StructureMenuResource(products[i], null);
                    Products[i] = structureMenuResource;
                }
            }
            else if (Structure is Construction construction && construction.BuildingDescription?.ConstructionInformation?.ResourceTypes != null)
            {
                var ingredients = construction.Description.ConstructionInformation.ResourceTypes.OrderBy(i => i.ResourceType).ToArray();
                Ingredients = new StructureMenuResource[ingredients.Length];
                for (var i = 0; i < ingredients.Length; i++)
                {
                    var structureMenuResource = new StructureMenuResource(ingredients[i], null);
                    Ingredients[i] = structureMenuResource;
                }
            }
        }

        private void RenderContent(CCPoint topLeft, CCSize size)
        {
            var colorCollection = VisualSettings.ColorCollection;
            float resourceRadius = 64;
            float structureRadius = 96;
            float ingredientSpacing = size.Width / (Ingredients.Length == 0 ? 1 : Ingredients.Length);
            float productsSpacing = size.Width / (Products.Length == 0 ? 1 : Products.Length);
            float rowHeight = size.Height / (Structure is Construction ? 2 : 3);
            float ingredientY = topLeft.Y - rowHeight * 0 - rowHeight / 2;
            float structureY = topLeft.Y - rowHeight * 1 - rowHeight / 2;
            float productsY = topLeft.Y - rowHeight * 2 - rowHeight / 2;

            var pendingRequests = Structure.ResourceDirector.PendingRequests.ToList();
            for (int i = 0; i < Ingredients.Length; i++)
            {
                var structureMenuResource = Ingredients[i];
                bool arrived = !pendingRequests.Any(r => structureMenuResource.ResourceTypeSource.ResourceType.CanBeUsedFor(r));
                if (!arrived)
                    pendingRequests.Remove(structureMenuResource.ResourceTypeSource.ResourceType);
                float x = i * ingredientSpacing + ingredientSpacing / 2 + topLeft.X;
                var position = new CCPoint(x, ingredientY);
                //DrawNode.DrawCircle(position, resourceRadius, resourceColor, 4, arrived ? colorCollection.StructureMenuResourceArrivedBorder : colorCollection.StructureMenuResourceNotArrivedBorder);
                DrawNode.DrawTexture(structureMenuResource.ResourceTypeSource.ResourceType.GetSpriteFrame(), position, new CCSize(resourceRadius * 2, resourceRadius * 2), arrived ? 1 : 0.33f);
            }

            var structurePosition = new CCPoint(topLeft.X + size.Width / 2, structureY);
            Structure.Render(DrawNode, structurePosition, structureRadius);
            if (Structure is IHasProgress progressStructure)
            {
                var arcColor = colorCollection.StructureMenuProgressCircleFill;
                DrawNode.DrawSolidArc(structurePosition, structureRadius * World.GameSettings.VisualSettings.ProgressRadiusFactor, (float)(progressStructure.Progress * PI * 2), arcColor);
            }

            if (!(Structure is Construction) || Structure.Description.IsProducer)
            {
                var pendingProvisions = Structure.ResourceDirector.PendingProvisions.ToList();
                for (int i = 0; i < (Products.Length); i++)
                {
                    var resourceType = Products[i].ResourceTypeSource.ResourceType;
                    bool provided = pendingProvisions.Contains(resourceType);
                    if (provided)
                        pendingProvisions.Remove(resourceType);
                    float x = i * productsSpacing + productsSpacing / 2 + topLeft.X;
                    var position = new CCPoint(x, productsY);
                    //DrawNode.DrawCircle(position, resourceRadius, resourceColor, 2, provided ? colorCollection.StructureMenuResourceArrivedBorder : colorCollection.StructureMenuResourceNotArrivedBorder);
                    DrawNode.DrawTexture(Products[i].ResourceTypeSource.ResourceType.GetSpriteFrame(), position, new CCSize(resourceRadius * 2, resourceRadius * 2), provided ? 1 : 0.33f);
                }
            }
            if (Structure.Description.VerbalStructureDescription.InternalName.ToUpper() == "PALACE")
            {
                var winStep = World.GlobalResourceManager.EnvironmentResource.WinStep;
                var totalWinStep = World.GameSettings.GameplaySettings.PalastWinSteps;
                var x = productsSpacing / 2 + topLeft.X;
                var y = productsY;
                string text = $"{winStep}/{totalWinStep}";
                DrawNode.DrawText(x, y, text, Font.ArialFonts[40], CCSize.Zero);
            }
        }
        private void Suspend()
        {
            var building = (Building)Structure;
            if (building.IsSuspended)
                building.Resume();
            else
                building.Suspend();
        }

        private void Update(float dt)
        {
            if (Structure == null || Host == null)
                return;
            var colorCollection = VisualSettings.ColorCollection;
            DrawNode.Clear();
            float headerHeight = ClientArea.Size.Height / 6;
            float footerHeight = ClientArea.Size.Height / 8;
            float descriptionHeight = ClientArea.Size.Height / 8;
            float contentHeight = ClientArea.Size.Height - headerHeight - descriptionHeight - footerHeight;
            DrawNode.DrawRect(ClientArea.Size.Center.InvertY, ClientArea.Size, colorCollection.StructureMenuBackground, VisualSettings.BuildMenuBorderThickness, colorCollection.StructureMenuBorder);
            DrawNode.DrawRect(new CCPoint(ClientArea.Size.Width / 2, -headerHeight / 2), ClientArea.Size.Width, headerHeight, colorCollection.StructureMenuGridBackground, 1, colorCollection.StructureMenuGridBorder);
            DrawNode.DrawRect(new CCPoint(ClientArea.Size.Width / 2, -headerHeight - descriptionHeight / 2), ClientArea.Size.Width, descriptionHeight, colorCollection.StructureMenuGridBackground, 1, colorCollection.StructureMenuGridBorder);
            DrawNode.DrawRect(new CCPoint(ClientArea.Size.Width / 2, -headerHeight - descriptionHeight - contentHeight / 2), ClientArea.Size.Width, contentHeight, colorCollection.StructureMenuGridBackground, 1, colorCollection.StructureMenuGridBorder);
            DrawNode.DrawRect(new CCPoint(ClientArea.Size.Width / 2, -headerHeight - descriptionHeight - contentHeight - footerHeight / 2), ClientArea.Size.Width, footerHeight, colorCollection.StructureMenuGridBackground, 1, colorCollection.StructureMenuGridBorder);
            var languageSettings = World.GameSettings.LanguageSettings;
            DrawNode.DrawText(ClientArea.Size.Center.X, -headerHeight / 2, Structure.Description.VerbalStructureDescription.NameID.Translate(languageSettings), Font.ArialFonts[VisualSettings.StructureMenuHeaderFontSize], new CCSize(ClientArea.Size.Width, headerHeight));
            DrawNode.DrawText(ClientArea.Size.Center.X, -headerHeight - descriptionHeight / 2, Structure.Description.VerbalStructureDescription.DescriptionID.Translate(languageSettings), Font.ArialFonts[VisualSettings.StructureMenuDescriptionFontSize], new CCSize(ClientArea.Size.Width, descriptionHeight));
            if (Structure is Building building)
            {
                if (SuspendEntry == null)
                {
                    var area = new CCRect(ClientArea.MinX, -headerHeight - contentHeight - descriptionHeight - footerHeight, ClientArea.Size.Width / 2, footerHeight);
                    SuspendEntry = new StructureMenuEntry(this, area);
                }
                if (DeconstructEntry == null)
                {
                    var area = new CCRect(ClientArea.MinX + ClientArea.Size.Width / 2, -headerHeight - contentHeight - descriptionHeight - footerHeight, ClientArea.Size.Width / 2, footerHeight);
                    DeconstructEntry = new StructureMenuEntry(this, area);
                }
                DrawNode.DrawText(ClientArea.Size.Center.X - ClientArea.Size.Width / 4, -headerHeight - contentHeight - descriptionHeight - footerHeight / 2, building.IsSuspended ? "Resume" : "Suspend", Font.ArialFonts[VisualSettings.StructureMenuFooterFontSize], new CCSize(ClientArea.Size.Width, footerHeight), new CCColor3B(SuspendEntry.IsPressed ? colorCollection.StructureMenuSuspendButtonPressed : colorCollection.StructureMenuSuspendButtonReleased));
                DrawNode.DrawText(ClientArea.Size.Center.X + ClientArea.Size.Width / 4, -headerHeight - contentHeight - descriptionHeight - footerHeight / 2, "Deconstruct", Font.ArialFonts[VisualSettings.StructureMenuFooterFontSize], new CCSize(ClientArea.Size.Width, footerHeight), new CCColor3B(DeconstructEntry.IsPressed ? colorCollection.StructureMenuDeconstructButtonPressed : colorCollection.StructureMenuDeconstructButtonReleased));
            }
            else
            {
                if (DeconstructEntry == null)
                {
                    var area = new CCRect(ClientArea.MinX, -headerHeight - contentHeight - descriptionHeight - footerHeight, ClientArea.Size.Width, footerHeight);
                    DeconstructEntry = new StructureMenuEntry(this, area);
                }
                DrawNode.DrawText(ClientArea.Size.Center.X, -headerHeight - contentHeight - descriptionHeight - footerHeight / 2, "Cancel", Font.ArialFonts[VisualSettings.StructureMenuFooterFontSize], new CCSize(ClientArea.Size.Width, footerHeight), new CCColor3B(DeconstructEntry.IsPressed ? colorCollection.StructureMenuCancelButtonPressed : colorCollection.StructureMenuCancelButtonReleased));
            }
            RenderContent(new CCPoint(0, -headerHeight - descriptionHeight), new CCSize(ClientArea.Size.Width, contentHeight));
        }

        private class StructureMenuEntry
        {
            public StructureMenu StructureMenu { get; }
            public CCRect Area { get; }
            public bool IsPressed { get; set; }

            public StructureMenuEntry(StructureMenu structureMenu, CCRect area)
            {
                StructureMenu = structureMenu;
                Area = area;
            }
        }
    }
}