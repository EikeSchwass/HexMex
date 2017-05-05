using System.Collections.Generic;
using CocosSharp;

namespace HexMex.Game
{
    public class World : ICCUpdatable
    {
        public World()
        {
            HexagonList.Add(new ResourceHexagon(typeof(PlatinumResource), 25, HexagonPosition.Zero));
            HexagonList.Add(new ResourceHexagon(typeof(PlatinumResource), 25, new HexagonPosition(-1, 0, 1)));
            HexagonList.Add(new ResourceHexagon(typeof(PlatinumResource), 25, new HexagonPosition(0, -1, 1)));
            StructureList.Add(new VillageBuilding(new HexagonCornerPosition(HexagonList[0].Position, HexagonList[1].Position, HexagonList[2].Position), ResourceManager, RecipeDatabase));
        }

        public RecipeDatabase RecipeDatabase { get; } = new RecipeDatabase();
        private List<Connection> ConnectionList { get; } = new List<Connection>();
        private List<Hexagon> HexagonList { get; } = new List<Hexagon>();
        private ResourceManager ResourceManager { get; } = new ResourceManager(1);
        private List<Structure> StructureList { get; } = new List<Structure>();

        public void Update(float dt)
        {
            ResourceManager.Update(dt);
        }
    }
}