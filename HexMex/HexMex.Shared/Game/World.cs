using CocosSharp;
using HexMex.Controls;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public class World : ICCUpdatable
    {
        public ButtonManager ButtonManager { get; }
        public EdgeManager EdgeManager { get; }
        public HexagonManager HexagonManager { get; }
        public bool IsInitialized { get; private set; }
        public PathFinder PathFinder { get; }
        public ResourceManager ResourceManager { get; }
        public StructureManager StructureManager { get; }
        public GameSettings GameSettings { get; }
        public DiceManager DiceManager { get; }

        public World(GameSettings gameSettings)
        {
            GameSettings = gameSettings;
            StructureManager = new StructureManager();
            EdgeManager = new EdgeManager(GameSettings.GameplaySettings);
            HexagonManager = new HexagonManager(GameSettings.GameplaySettings);
            ButtonManager = new ButtonManager(GameSettings.LayoutSettings);
            PathFinder = new PathFinder(HexagonManager, EdgeManager, StructureManager);
            ResourceManager = new ResourceManager(this);
            DiceManager=new DiceManager(this);
            StructureManager.StructureAdded += StructureAdded;
        }

        public void Initialize()
        {
            var p1 = HexagonPosition.Zero;
            var p2 = new HexagonPosition(0, 1, -1);
            var p3 = new HexagonPosition(1, 0, -1);

            HexagonManager.RevealHexagonAt(p1);
            HexagonManager.RevealHexagonAt(p2);
            HexagonManager.RevealHexagonAt(p3);

            StructureManager.CreateStrucuture(new DiamondExtractor(new HexagonNode(p1, p2, p3), this));

            IsInitialized = true;
        }

        public void Update(float dt)
        {
            if (!IsInitialized)
                return;
            DiceManager.Update(dt);
            HexagonManager.Update(dt);
            ResourceManager.Update(dt);
            StructureManager.Update(dt);
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
                        var button = new BuildButton(GameSettings, adjacentHexagonNode);
                        ButtonManager.AddButton(button, adjacentHexagonNode);
                    }
                    if (!EdgeManager.ContainsEdge(structure.Position, adjacentHexagonNode))
                        EdgeManager.AddEdge(structure.Position, adjacentHexagonNode);
                }
            }
        }
    }
}