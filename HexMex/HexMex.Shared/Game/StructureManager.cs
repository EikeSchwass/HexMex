using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Game.Buildings;

namespace HexMex.Game
{
    public class StructureManager : IEnumerable<Structure>, ICCUpdatable
    {
        public StructureManager(World world)
        {
            World = world;
        }

        public event Action<StructureManager, Structure> StructureAdded;
        public event Action<StructureManager, Structure> StructureRemoved;

        public Structure this[HexagonNode hexagonNode] => GetStructureAtPosition(hexagonNode);
        public World World { get; }

        private Dictionary<HexagonNode, Structure> Structures { get; } = new Dictionary<HexagonNode, Structure>();

        public void CreateStrucuture(Structure structure)
        {
            Structures.Add(structure.Position, structure);
            StructureAdded?.Invoke(this, structure);
        }

        public void RemoveStructure(Structure structure)
        {
            Structures.Remove(structure.Position);
            StructureRemoved?.Invoke(this, structure);
        }

        public IEnumerator<Structure> GetEnumerator()
        {
            return Structures.Values.GetEnumerator();
        }

        public Structure GetStructureAtPosition(HexagonNode hexagonNode)
        {
            if (!Structures.ContainsKey(hexagonNode))
                return null;
            return Structures[hexagonNode];
        }

        public void Update(float dt)
        {
            foreach (var structure in this.OfType<ICCUpdatable>())
            {
                structure.Update(dt);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}