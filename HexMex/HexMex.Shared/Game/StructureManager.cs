using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;

namespace HexMex.Game
{
    public class StructureManager : IEnumerable<Structure>, ICCUpdatable
    {
        public event Action<StructureManager, Structure> StructureAdded;
        public event Action<StructureManager, Structure> StructureRemoved;
        public event Action<StructureManager, Structure, Structure> StructureReplaced;

        public Structure this[HexagonNode hexagonNode] => GetStructureAtPosition(hexagonNode);

        private Dictionary<HexagonNode, Structure> Structures { get; } = new Dictionary<HexagonNode, Structure>();

        public void CreateStrucuture(Structure structure)
        {
            Structures.Add(structure.Position, structure);
            StructureAdded?.Invoke(this, structure);
        }

        public IEnumerator<Structure> GetEnumerator()
        {
            return Structures.Values.GetEnumerator();
        }

        public Structure GetStructureAtPosition(HexagonNode hexagonNode) => !Structures.ContainsKey(hexagonNode) ? null : Structures[hexagonNode];

        public void RemoveStructure(Structure structure)
        {
            Structures.Remove(structure.Position);
            StructureRemoved?.Invoke(this, structure);
        }

        public void ReplaceStructure(Structure oldStructure, Structure newStructure)
        {
            Structures.Remove(oldStructure.Position);
            Structures.Add(newStructure.Position, newStructure);
            StructureReplaced?.Invoke(this, oldStructure, newStructure);
        }

        public void Update(float dt)
        {
            foreach (var structure in this.ToArray())
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