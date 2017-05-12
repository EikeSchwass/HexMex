using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Controls.Buildings;
using HexMex.Game;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class StructureLayer : TouchLayer
    {
        public WorldSettings WorldSettings { get; }
        private CCPoint[] HexagonCorners { get; }

        public StructureLayer(StructureManager structureManager, WorldSettings worldSettings, HexMexCamera camera) : base(camera)
        {
            WorldSettings = worldSettings;
            structureManager.StructureAdded += StructureAdded;
            structureManager.StructureRemoved += StructureRemoved;
            HexagonCorners = HexagonHelper.GenerateWorldCorners(CCPoint.Zero, 1).Select(c => c.RotateAround(CCPoint.Zero, 30)).ToArray();
        }
        private Dictionary<Structure, StructureControl> Structures { get; } = new Dictionary<Structure, StructureControl>();

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            StructureControl node;
            switch (structure)
            {
                case Construction construction:
                    node = new ConstructionControl(construction, WorldSettings.HexagonMargin);
                    break;
                case MineBuilding mineBuilding:
                    node = new MineControl(mineBuilding, WorldSettings.HexagonMargin);
                    break;
                case VillageBuilding villageBuilding:
                    node = new VillageControl(villageBuilding, WorldSettings.HexagonMargin);
                    break;
                default:
                    throw new ArgumentException("All Structures must have an appropiate control", nameof(structure));

            }
            var worldPosition = structure.Position.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
            node.Position = worldPosition;
            AddChild(node);
            node.OnRequiresRedraw(structure);
            Structures.Add(structure, node);
        }


        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            var node = Structures[structure];
            RemoveChild(node);
        }
    }
}