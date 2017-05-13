using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class StructureRenderer
    {
        public StructureRenderer(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
        }

        public WorldSettings WorldSettings { get; }

        public void Render(Structure structure, CCDrawNode drawNode)
        {
            switch (structure)
            {
                case Construction construction:
                    Render(construction, drawNode);
                    break;
                case MineBuilding building:
                    Render(building, drawNode);
                    break;
                case VillageBuilding building:
                    Render(building, drawNode);
                    break;
            }
        }

        private void Render(MineBuilding building, CCDrawNode drawNode)
        {
            var position = building.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, WorldSettings.HexagonMargin, ColorCollection.MineBuildingColor);
        }

        private void Render(VillageBuilding building, CCDrawNode drawNode)
        {
            var position = building.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            drawNode.DrawDot(position, WorldSettings.HexagonMargin, ColorCollection.VillageBuildingColor);
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