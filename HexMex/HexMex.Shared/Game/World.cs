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
            ResourceManager = new ResourceManager(this);
            HexagonManager = new HexagonManager(this);
            StructureManager = new StructureManager(this);
            ButtonManager = new ButtonManager(this);
            StructureManager.StructureAdded += StructureAdded;
        }

        public ButtonManager ButtonManager { get; }
        public HexagonManager HexagonManager { get; }
        public bool IsInitialized { get; private set; }
        public ResourceManager ResourceManager { get; }
        public StructureManager StructureManager { get; }

        public WorldSettings WorldSettings { get; }

        public void Initialize()
        {
            var p1 = HexagonPosition.Zero;
            var p2 = new HexagonPosition(0, 1, -1);
            var p3 = new HexagonPosition(1, 0, -1);
            //var p4 = new HexagonPosition(1, -1, 0);
            //var p5 = new HexagonPosition(-1, 1, 0);
            //var p6 = new HexagonPosition(1, 1, -2);

            HexagonManager.RevealHexagonAt(p1);
            HexagonManager.RevealHexagonAt(p2);
            HexagonManager.RevealHexagonAt(p3);
            //HexagonManager.RevealHexagonAt(p4);
            //HexagonManager.RevealHexagonAt(p5);
            //HexagonManager.RevealHexagonAt(p6);

            StructureManager.CreateStrucuture(new MineBuilding(new HexagonNode(p1, p2, p3), ResourceManager));

            IsInitialized = true;
        }

        public void Update(float dt)
        {
            if (!IsInitialized)
                return;
            ResourceManager.Update(dt);
            HexagonManager.Update(dt);
            StructureManager.Update(dt);
        }

        private void BuildingBlockTouched(Button button, HexagonNode hexagonNode)
        {
            StructureManager.CreateStrucuture(new MineBuilding(hexagonNode, ResourceManager));
            ButtonManager.RemoveButton(button);
        }

        private void StructureAdded(StructureManager manager, Structure structure)
        {
            if (structure is Building)
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