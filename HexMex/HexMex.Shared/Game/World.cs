using System;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public class World : ICCUpdatable
    {
        public World(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
            EdgeManager = new EdgeManager();
            StructureManager = new StructureManager();
            ResourcePackageManager = new ResourcePackageManager();
            HexagonManager = new HexagonManager(WorldSettings);
            ButtonManager = new ButtonManager(WorldSettings);
            PathFinder = new PathFinder(HexagonManager, EdgeManager, StructureManager);
            ResourceManager = new ResourceManager(PathFinder, EdgeManager, ResourcePackageManager, 1);
            StructureManager.StructureAdded += StructureAdded;
        }

        public ButtonManager ButtonManager { get; }
        public EdgeManager EdgeManager { get; }
        public HexagonManager HexagonManager { get; }
        public bool IsInitialized { get; private set; }
        public PathFinder PathFinder { get; }
        public ResourceManager ResourceManager { get; }
        public ResourcePackageManager ResourcePackageManager { get; }
        public StructureManager StructureManager { get; }
        public WorldSettings WorldSettings { get; }

        public void Initialize()
        {
            var p1 = HexagonPosition.Zero;
            var p2 = new HexagonPosition(0, 1, -1);
            var p3 = new HexagonPosition(1, 0, -1);

            HexagonManager.RevealHexagonAt(p1);
            HexagonManager.RevealHexagonAt(p2);
            HexagonManager.RevealHexagonAt(p3);

            StructureManager.CreateStrucuture(new MineBuilding(new HexagonNode(p1, p2, p3), ResourceManager, HexagonManager));

            IsInitialized = true;
        }

        public void Update(float dt)
        {
            if (!IsInitialized)
                return;
            HexagonManager.Update(dt);
            ResourceManager.Update(dt);
            ResourcePackageManager.Update(dt);
            StructureManager.Update(dt);
        }

        private void BuildingBlockTouched(Button button, HexagonNode hexagonNode)
        {
            StructureManager.CreateStrucuture(new Construction(hexagonNode, 10, ResourceManager, HexagonManager, s => { ReplaceConstructionWithBuilding(s, () => new VillageBuilding(hexagonNode, ResourceManager, HexagonManager)); }, ResourceType.Gold));
            ButtonManager.RemoveButton(button);
        }

        private void ReplaceConstructionWithBuilding(Construction construction, Func<Structure> structureCreator)
        {
            StructureManager.RemoveStructure(construction);
            StructureManager.CreateStrucuture(structureCreator());
        }

        private void StructureAdded(StructureManager manager, Structure structure)
        {
            if (!(structure is Construction))
            {
                foreach (var adjacentHexagonNode in structure.Position.GetAccessibleAdjacentHexagonNodes(HexagonManager))
                {
                    var p1 = adjacentHexagonNode.Position1;
                    var p2 = adjacentHexagonNode.Position2;
                    var p3 = adjacentHexagonNode.Position3;
                    if (HexagonManager.GetHexagonAtPosition(p1) == null)
                        HexagonManager.RevealHexagonAt(p1);
                    if (HexagonManager.GetHexagonAtPosition(p2) == null)
                        HexagonManager.RevealHexagonAt(p2);
                    if (HexagonManager.GetHexagonAtPosition(p3) == null)
                        HexagonManager.RevealHexagonAt(p3);

                    if (StructureManager[adjacentHexagonNode] == null && ButtonManager[adjacentHexagonNode] == null)
                    {
                        var button = new TextButton("Test", 100);
                        button.Touched += touch => BuildingBlockTouched(button, adjacentHexagonNode);
                        ButtonManager.AddButton(button, adjacentHexagonNode);
                    }
                }
            }
        }
    }
}