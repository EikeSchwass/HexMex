using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;

namespace HexMex.Game
{
    public class World : ICCUpdatable
    {
        public World()
        {
            int size = 3;
            Random r = new Random();
            for (int x = -size; x < size + 1; x++)
            {
                for (int y = -size; y < size + 1; y++)
                {
                    for (int z = -size; z < size + 1; z++)
                    {
                        if (x + y + z != 0)
                            continue;
                        HexagonList.Add(new ResourceHexagon(ResourceType.Platinum, r.Next(0, WorldSettings.MaxNumberOfResourcesInHexagon), new HexagonPosition(x, y, z)));
                    }
                }
            }
            StructureList.Add(new VillageBuilding(new HexagonCornerPosition(HexagonList[0].Position, HexagonList[1].Position, HexagonList[2].Position), ResourceManager, RecipeDatabase));
        }

        public event Action<World, Hexagon> HexagonAdded;
        public IReadOnlyCollection<Connection> Connections => ConnectionList.AsReadOnly();

        public IReadOnlyCollection<Hexagon> Hexagons => HexagonList.AsReadOnly();

        public RecipeDatabase RecipeDatabase { get; } = new RecipeDatabase();
        public IReadOnlyCollection<Structure> Structures => StructureList.AsReadOnly();
        public WorldSettings WorldSettings { get; } = new WorldSettings();
        private List<Connection> ConnectionList { get; } = new List<Connection>();
        private List<Hexagon> HexagonList { get; } = new List<Hexagon>();
        private ResourceManager ResourceManager { get; } = new ResourceManager(1);
        private List<Structure> StructureList { get; } = new List<Structure>();
        private HexagonLoader HexagonLoader { get; } = new HexagonLoader();

        public void Update(float dt)
        {
            ResourceManager.Update(dt);
            var updateableStructures = StructureList.OfType<ICCUpdatable>();
            var updateableHexagons = StructureList.OfType<ICCUpdatable>();
            var updateableConnections = StructureList.OfType<ICCUpdatable>();

            foreach (var updatable in updateableStructures.Concat(updateableHexagons).Concat(updateableConnections))
            {
                updatable.Update(dt);
            }
        }

        public void RevealHexagon(HexagonPosition position)
        {
            var revealHexagon = HexagonLoader.RevealHexagon(position);
            HexagonList.Add(revealHexagon);
            HexagonAdded?.Invoke(this, revealHexagon);
        }
    }
}