using System.Collections.Generic;
using CocosSharp;
using HexMex.Game;
using HexMex.Game.Buildings;

namespace HexMex.Scenes.Game
{
    public class StructureLayer : TouchLayer
    {
        public StructureLayer(World world, HexMexCamera camera) : base(camera)
        {
            WorldSettings = world.WorldSettings;
            world.StructureManager.StructureAdded += StructureAdded;
            world.StructureManager.StructureRemoved += StructureRemoved;
            StructureRenderer = new StructureRenderer(WorldSettings);
            Schedule();
            AddChild(DrawNode);
        }

        public WorldSettings WorldSettings { get; }

        private CCDrawNode DrawNode { get; } = new CCDrawNode();

        private bool RedrawRequested { get; set; }
        private StructureRenderer StructureRenderer { get; }

        private List<Structure> Structures { get; } = new List<Structure>();

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            RedrawRequested = false;
            DrawNode.Clear();
            foreach (var structure in Structures)
            {
                StructureRenderer.Render(structure, DrawNode);
            }
        }

        private void StructureAdded(StructureManager structureManager, Structure structure)
        {
            structure.RequiresRedraw += s => RedrawRequested = true;
            Structures.Add(structure);
            RedrawRequested = true;
        }

        private void StructureRemoved(StructureManager structureManager, Structure structure)
        {
            Structures.Remove(structure);
            RedrawRequested = true;
        }
    }
}