using System.Globalization;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class StatisticLayer : TouchLayer
    {
        public World World { get; }
        private ExtendedDrawNode DrawNode { get; }

        private CCSprite EnergySprite { get; }
        private CCSprite Knowledge1Sprite { get; }
        private CCSprite Knowledge2Sprite { get; }
        private CCSprite Knowledge3Sprite { get; }
        private CCSprite FastForewardSprite { get; }

        private bool IsGameSpeedButtonPressed { get; set; }

        public StatisticLayer(World world)
        {
            World = world;
            var colorCollection = world.GameSettings.VisualSettings.ColorCollection;
            DrawNode = new ExtendedDrawNode();
            EnergySprite = new CCSprite("bolt.png") { Color = colorCollection.Energy };
            Knowledge1Sprite = new CCSprite("research.png") { Color = colorCollection.Knowledge1 };
            Knowledge2Sprite = new CCSprite("research.png") { Color = colorCollection.Knowledge2 };
            Knowledge3Sprite = new CCSprite("research.png") { Color = colorCollection.Knowledge3 };
            FastForewardSprite = new CCSprite("fastforeward.png");
            AddChild(DrawNode);
            AddChild(EnergySprite);
            AddChild(Knowledge1Sprite);
            AddChild(Knowledge2Sprite);
            AddChild(Knowledge3Sprite);
            AddChild(FastForewardSprite);
            World.GlobalResourceManager.ValueChanged += e => Render();
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            Render();
        }

        private void Render()
        {
            {
                DrawNode.Clear();
                var visualSettings = World.GameSettings.VisualSettings;
                int fontSize = visualSettings.StatisticsFontSize;
                var spriteSize = fontSize * 2f;
                var margin = 10;
                float x = visualSettings.StatisticsMargin;
                float y0 = VisibleBoundsWorldspace.MaxY - visualSettings.StatisticsMargin - spriteSize * 0 - margin * 0;
                float y1 = VisibleBoundsWorldspace.MaxY - visualSettings.StatisticsMargin - spriteSize * 1 - margin * 1;
                float y2 = VisibleBoundsWorldspace.MaxY - visualSettings.StatisticsMargin - spriteSize * 2 - margin * 2;
                float y3 = VisibleBoundsWorldspace.MaxY - visualSettings.StatisticsMargin - spriteSize * 3 - margin * 3;

                var resourceManager = World.GlobalResourceManager;

                var s0 = resourceManager.EnvironmentResource.Energy.ToString(CultureInfo.InvariantCulture);
                var s1 = resourceManager.Knowledge.Knowledge1.ToString();
                var s2 = resourceManager.Knowledge.Knowledge2.ToString();
                var s3 = resourceManager.Knowledge.Knowledge3.ToString();
                DrawNode.DrawText(x + spriteSize - spriteSize / 8 + (float)Sqrt(s0.Length) / 1.5f * fontSize, y0, s0, Font.ArialFonts[fontSize], CCSize.Zero);
                DrawNode.DrawText(x + spriteSize - spriteSize / 8 + (float)Sqrt(s1.Length) / 1.5f * fontSize, y1, s1, Font.ArialFonts[fontSize], CCSize.Zero);
                DrawNode.DrawText(x + spriteSize - spriteSize / 8 + (float)Sqrt(s2.Length) / 1.5f * fontSize, y2, s2, Font.ArialFonts[fontSize], CCSize.Zero);
                DrawNode.DrawText(x + spriteSize - spriteSize / 8 + (float)Sqrt(s3.Length) / 1.5f * fontSize, y3, s3, Font.ArialFonts[fontSize], CCSize.Zero);

                DrawNode.DrawCircle(new CCPoint(x, y0), spriteSize / 2, CCColor4B.White);
                DrawNode.DrawCircle(new CCPoint(x, y1), spriteSize / 2, CCColor4B.White);
                DrawNode.DrawCircle(new CCPoint(x, y2), spriteSize / 2, CCColor4B.White);
                DrawNode.DrawCircle(new CCPoint(x, y3), spriteSize / 2, CCColor4B.White);

                EnergySprite.Position = new CCPoint(x, y0);
                Knowledge1Sprite.Position = new CCPoint(x, y1);
                Knowledge2Sprite.Position = new CCPoint(x, y2);
                Knowledge3Sprite.Position = new CCPoint(x, y3);

                EnergySprite.ContentSize = Knowledge1Sprite.ContentSize = Knowledge2Sprite.ContentSize = Knowledge3Sprite.ContentSize = new CCSize(spriteSize * 0.8f, spriteSize * 0.8f);
            }
            {
                var colorCollection = World.GameSettings.VisualSettings.ColorCollection;
                var margin = World.GameSettings.VisualSettings.StatisticsMargin;
                var radius = 45;
                var x = VisibleBoundsWorldspace.MaxX - margin;
                var y = VisibleBoundsWorldspace.MaxY - margin;
                var position = new CCPoint(x, y);
                DrawNode.DrawCircle(position, radius, colorCollection.FastForewardBackground, 2, colorCollection.FastForewardBorder);
                FastForewardSprite.Color = new CCColor3B(World.GameSpeed.GetColor(colorCollection));
                FastForewardSprite.Position = position + new CCPoint(2, 0);
                FastForewardSprite.ContentSize = new CCSize(radius * 2, radius * 2) * 0.8f;
            }
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            IsGameSpeedButtonPressed = IsLocationInsideGameSpeedButton(e.Touch.LocationOnScreen);
            Render();
        }

        public override void OnTouchCancelled(TouchEventArgs e, TouchCancelReason cancelReason)
        {
            base.OnTouchCancelled(e, cancelReason);
            IsGameSpeedButtonPressed = false;
            Render();
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            if (IsGameSpeedButtonPressed && IsLocationInsideGameSpeedButton(e.Touch.LocationOnScreen))
            {
                World.GameSpeed = World.GameSpeed.Next();
            }
            IsGameSpeedButtonPressed = false;
            Render();
        }

        private bool IsLocationInsideGameSpeedButton(CCPoint position)
        {
            position = new CCPoint(position.X, VisibleBoundsWorldspace.MaxY - position.Y);
            var distance = FastForewardSprite.Position - position;
            var radius = FastForewardSprite.ContentSize.Width * FastForewardSprite.ContentSize.Height;
            return distance.LengthSquared <= radius;
        }
    }
}