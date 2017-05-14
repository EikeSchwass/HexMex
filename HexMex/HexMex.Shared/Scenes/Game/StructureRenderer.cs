using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class StructureRenderer
    {
        public WorldSettings WorldSettings { get; }

        public StructureRenderer(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
        }

        public void Render(Structure structure, CCDrawNode drawNode)
        {
            structure.Render(drawNode);
        }

        private void Render(DiamondExtractor building, CCDrawNode drawNode)
        {
            var position = building.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, WorldSettings.HexagonMargin, ColorCollection.MineBuildingColor);
        }

        private void Render(Construction construction, CCDrawNode drawNode)
        {
            var position = construction.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            drawNode.DrawSolidCircle(position, WorldSettings.HexagonMargin, ColorCollection.ConstructionBackgroundColor);
            drawNode.DrawSolidArc(position, WorldSettings.HexagonMargin, (float)(PI / 2), (float)(-construction.Progress * PI * 2), ColorCollection.ConstructionProgressColor);
            drawNode.DrawSolidCircle(position, WorldSettings.HexagonMargin * 3 / 4, ColorCollection.ConstructionBackgroundColor);
        }
    }
}