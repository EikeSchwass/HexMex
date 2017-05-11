using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Buildings;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class BuildMenu : Control
    {
        public BuildMenu(BuildButton buildButton, WorldSettings worldSettings, double radius)
        {
            BuildButton = buildButton;
            WorldSettings = worldSettings;
            Radius = radius;
            MenuEntries = new BuildMenuEntry[BuildingConstructionFactory.Factories.Count];
            LoadEntries();
        }

        public double Radius { get; }
        public WorldSettings WorldSettings { get; }
        private BuildButton BuildButton { get; }

        private BuildMenuEntry[] MenuEntries { get; }

        private void LoadEntries()
        {
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                var factory = BuildingConstructionFactory.Factories.ElementAt(i).Value;
                var menuEntry = new BuildMenuEntry(factory);
                MenuEntries[i] = menuEntry;
                AddChild(menuEntry);
            }
            double angle = PI * 2 / MenuEntries.Length;
            for (int i = 0; i < MenuEntries.Length; i++)
            {
                var x = Sin(i * angle) * Radius / 3 * 2;
                var y = Sin(i * angle) * Radius / 3 * 2;
                MenuEntries[i].Position = new CCPoint((float)x, (float)y);
            }
        }
    }
}