using System.Collections.Generic;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class StructureLayer : TouchLayer
    {
        public World World { get; }
        private ExtendedDrawNode DrawNode { get; } = new ExtendedDrawNode();

        private bool RedrawRequested { get; set; }

        private List<Structure> Structures { get; } = new List<Structure>();

        public StructureLayer(World world, HexMexCamera camera) : base(camera)
        {
            World = world;
            world.StructureManager.StructureAdded += StructureAdded;
            world.StructureManager.StructureRemoved += StructureRemoved;
            Schedule(Update, 0.05f);
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
            var settings = World.GameSettings;
            var arcColor = CCColor4B.Lerp(settings.VisualSettings.ColorCollection.GrayNormal, settings.VisualSettings.ColorCollection.Transparent, 0.333f);
            foreach (var structure in Structures)
            {
                structure.Render(DrawNode);
                if (structure is IHasProgress progress)
                {
                    var position = structure.Position.GetWorldPosition(settings.LayoutSettings.HexagonRadius, settings.LayoutSettings.HexagonMargin);
                    DrawNode.DrawSolidArc(position, settings.VisualSettings.BuildingRadius * settings.VisualSettings.ProgressRadiusFactor, (float)(progress.Progress * PI * 2), arcColor);
                }
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