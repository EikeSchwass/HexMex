using CocosSharp;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class StatisticLayer : TouchLayer
    {
        public World World { get; }
        private ExtendedDrawNode DrawNode { get; }

        public StatisticLayer(World world)
        {
            World = world;
            DrawNode = new ExtendedDrawNode();
            AddChild(DrawNode);
            World.GlobalResourceManager.ValueChanged += e => Render();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Render();
        }

        private void Render()
        {
            var visualSettings = World.GameSettings.VisualSettings;
            int fontSize = visualSettings.StatisticsFontSize;
            float y = VisibleBoundsWorldspace.MaxY - visualSettings.StatisticsMargin;
            float minX = VisibleBoundsWorldspace.MinX + visualSettings.StatisticsMargin;
            //float maxX = VisibleBoundsWorldspace.MaxX - visualSettings.StatisticsMargin;

            var resourceManager = World.GlobalResourceManager;
            DrawNode.DrawText(minX, y, $"K1: {resourceManager.Knowledge.Knowledge1} K2: {resourceManager.Knowledge.Knowledge2} K3: {resourceManager.Knowledge.Knowledge3}", Font.ArialFonts[fontSize], CCSize.Zero);
        }
    }
}