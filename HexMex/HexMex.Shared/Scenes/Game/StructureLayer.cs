using System.Collections.Generic;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;

namespace HexMex.Scenes.Game
{
    public class StructureLayer : TouchLayer
    {
        private CCDrawNode DrawNode { get; } = new CCDrawNode();

        private bool RedrawRequested { get; set; }

        private List<Structure> Structures { get; } = new List<Structure>();

        public StructureLayer(World world, HexMexCamera camera) : base(camera)
        {
            world.StructureManager.StructureAdded += StructureAdded;
            world.StructureManager.StructureRemoved += StructureRemoved;
            Schedule(Update, 0.5f);
            AddChild(DrawNode);
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            RedrawRequested = false;
            Render();
        }

        private void Render()
        {
            DrawNode.Clear();
            foreach (var structure in Structures)
            {
                structure.Render(DrawNode);
            }
        }

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            structure.RequiresRedraw += s => RedrawRequested = true;
            Structures.Add(structure);
            Render();
        }

        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            Structures.Remove(structure);
            RedrawRequested = true;
            Render();
        }
    }
}