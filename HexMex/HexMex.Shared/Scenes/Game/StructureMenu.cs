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
        public World World { get; }
        public Structure Structure { get; set; }
        public StructureMenu(VisualSettings visualSettings, World world) : base(visualSettings)
        {
            World = world;
        }

        protected override void OnAddedToScene()
        {
            base.OnAddedToScene();
            DrawNode.Schedule(Update, 0.1f);
            Update(0);
        }

        private void Update(float dt)
        {
            if (Structure == null || Host == null)
                return;
            var colorCollection = VisualSettings.ColorCollection;
            DrawNode.Clear();
            float headerHeight = ClientSize.Height / 6;
            float footerHeight = ClientSize.Height / 8;
            float descriptionHeight = ClientSize.Height / 8;
            float contentHeight = ClientSize.Height - headerHeight - descriptionHeight - footerHeight;
            DrawNode.DrawRect(ClientSize.Center.InvertY, ClientSize, CCColor4B.Lerp(colorCollection.GrayVeryDark, colorCollection.Transparent, 0.25f), VisualSettings.BuildMenuBorderThickness, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(ClientSize.Width / 2, -headerHeight / 2), ClientSize.Width, headerHeight, colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(ClientSize.Width / 2, -headerHeight - descriptionHeight / 2), ClientSize.Width, descriptionHeight, colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(ClientSize.Width / 2, -headerHeight - descriptionHeight - contentHeight / 2), ClientSize.Width, contentHeight, colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawRect(new CCPoint(ClientSize.Width / 2, -headerHeight - descriptionHeight - contentHeight - footerHeight / 2), ClientSize.Width, footerHeight, colorCollection.Transparent, 1, colorCollection.White);
            DrawNode.DrawText(ClientSize.Center.X, -headerHeight / 2, Structure.Description.Name, Font.ArialFonts[VisualSettings.StructureMenuHeaderFontSize], new CCSize(ClientSize.Width, headerHeight));
            DrawNode.DrawText(ClientSize.Center.X, -headerHeight - descriptionHeight / 2, Structure.Description.Description, Font.ArialFonts[VisualSettings.StructureMenuDescriptionFontSize], new CCSize(ClientSize.Width, descriptionHeight));
            DrawNode.DrawText(ClientSize.Center.X, -headerHeight - contentHeight - descriptionHeight - footerHeight / 2, Structure is Construction ? "Cancel" : "Deconstruct", Font.ArialFonts[VisualSettings.StructureMenuFooterFontSize], new CCSize(ClientSize.Width, footerHeight));
            RenderContent(new CCPoint(0, -headerHeight - descriptionHeight), new CCSize(ClientSize.Width, contentHeight));
        }
        private void RenderContent(CCPoint topLeft, CCSize size)
        {
            var colorCollection = VisualSettings.ColorCollection;
            float resourceRadius = 16;
            float structureRadius = 64;
            var ingredients = Structure.Description.ProductionInformation?.Ingredients.ResourceTypes.OrderBy(i => i).ToList() ?? Structure.Description.ConstructionCost.ResourceTypes.OrderBy(i => i).ToList();
            var products = Structure.Description.ProductionInformation?.Products.ResourceTypes;
            float ingredientSpacing = size.Width / (ingredients.Count == 0 ? 1 : ingredients.Count);
            float productsSpacing = size.Width / (products?.Count == 0 ? 1 : products?.Count) ?? 1;
            float rowHeight = size.Height / (Structure is Construction ? 2 : 3);
            float ingredientY = topLeft.Y - rowHeight * 0 - rowHeight / 2;
            float structureY = topLeft.Y - rowHeight * 1 - rowHeight / 2;
            float productsY = topLeft.Y - rowHeight * 2 - rowHeight / 2;

            var pendingRequests = Structure.ResourceDirector.PendingRequests.ToList();
            for (int i = 0; i <ingredients.Count; i++)
            {
                var resourceType = ingredients[i];
                bool arrived = !pendingRequests.Contains(resourceType);
                if (!arrived)
                    pendingRequests.Remove(resourceType);
                float x = i * ingredientSpacing + ingredientSpacing / 2 + topLeft.X;
                var resourceColor = resourceType.GetColor(colorCollection);
                DrawNode.DrawCircle(new CCPoint(x, ingredientY), resourceRadius, resourceColor, 2, arrived ? colorCollection.White : colorCollection.GrayNormal);
            }

            var structurePosition = new CCPoint(topLeft.X + size.Width / 2, structureY);
            Structure.Render(DrawNode, structurePosition, structureRadius);
            if (Structure is IHasProgress progressStructure)
            {
                var arcColor = CCColor4B.Lerp(World.GameSettings.VisualSettings.ColorCollection.Black, World.GameSettings.VisualSettings.ColorCollection.Transparent, 0.25f);
                DrawNode.DrawSolidArc(structurePosition, structureRadius * World.GameSettings.VisualSettings.ProgressRadiusFactor, (float)(progressStructure.Progress * PI * 2), arcColor);
            }

            if (!(Structure is Construction) || Structure.Description.IsProducer)
            {
                var pendingProvisions = Structure.ResourceDirector.PendingProvisions.ToList();
                for (int i = 0; i < (products?.Count ?? 0); i++)
                {
                    var resourceType = products?[i] ?? ResourceType.Anything;
                    bool provided = pendingProvisions.Contains(resourceType);
                    if (provided)
                        pendingProvisions.Remove(resourceType);
                    float x = i * productsSpacing + productsSpacing / 2 + topLeft.X;
                    var resourceColor = resourceType.GetColor(colorCollection);
                    DrawNode.DrawCircle(new CCPoint(x, productsY), resourceRadius, resourceColor, 2, provided ? colorCollection.White : colorCollection.GrayNormal);
                }
            }

        }
    }
}