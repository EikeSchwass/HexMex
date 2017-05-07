using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class StructureLayer : CCLayer
    {
        public WorldSettings WorldSettings { get; }
        private CCPoint[] HexagonCorners { get; }

        public StructureLayer(StructureManager structureManager, WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            structureManager.StructureAdded += StructureAdded;
            structureManager.StructureRemoved += StructureRemoved;
            HexagonCorners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, 1).Select(c => c.RotateAround(CCPoint.Zero, 30)).ToArray();
        }
        private Dictionary<Structure, CCDrawNode> Structures { get; } = new Dictionary<Structure, CCDrawNode>();

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            var drawNode = new CCDrawNode();
            var worldPosition = structure.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            drawNode.Position = worldPosition;
            AddChild(drawNode);
            Structures.Add(structure, drawNode);
            structure.RequiresRedraw += RedrawStructure;
            RedrawStructure(structure);
        }

        private void RedrawStructure(Structure structure)
        {
            var drawNode = Structures[structure];
            drawNode.Clear();
            DrawStructureOutline(structure, drawNode);

            if (structure is Construction)
            {
                drawNode.DrawDot(CCPoint.Zero, 10, CCColor4B.Red);
            }

        }

        private void DrawStructureOutline(Structure structure, CCDrawNode drawNode)
        {
            drawNode.DrawPolygon(HexagonCorners.Select(c => c * structure.RenderInformation.HexagonRadius).ToArray(), 6, structure.RenderInformation.BackgroundColor, 1, structure.RenderInformation.BorderColor);
        }

        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            structure.RequiresRedraw -= RedrawStructure;
        }
    }
}