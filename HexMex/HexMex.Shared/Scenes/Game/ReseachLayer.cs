using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;

namespace HexMex.Scenes.Game
{
    public class ReseachLayer : TouchLayer
    {
        private CCPoint researchButtonPosition;
        private float radius;
        private bool isResearchButtonPressed;
        public GameSettings GameSettings { get; }
        private ExtendedDrawNode DrawNode { get; }
        public CCPoint ResearchButtonPosition
        {
            get => researchButtonPosition;
            set
            {
                researchButtonPosition = value;
                RenderResearchButton();
            }
        }

        public float Radius
        {
            get => radius;
            set
            {
                radius = value;
                RenderResearchButton();
            }
        }

        public bool IsResearchButtonPressed
        {
            get => isResearchButtonPressed;
            private set
            {
                isResearchButtonPressed = value;
                RenderResearchButton();
            }
        }

        public ReseachLayer(World world)
        {
            GameSettings = world.GameSettings;
            DrawNode = new ExtendedDrawNode();
            Sprite = new CCSprite("research.png") { Visible = false };
            AddChild(DrawNode);
            AddChild(Sprite);
        }
        private CCSprite Sprite { get; }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var size = Window.WindowSizeInPixels;
            ResearchButtonPosition = new CCPoint(size.Width - 160, 160);
            Radius = 96;
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            var pos = e.Touch.LocationOnScreen.InvertY + new CCPoint(0, Window.WindowSizeInPixels.Height);
            IsResearchButtonPressed = (pos - ResearchButtonPosition).LengthSquared <= Radius * Radius;
        }

        public override void OnTouchCancelled(TouchEventArgs e, TouchCancelReason cancelReason)
        {
            base.OnTouchCancelled(e, cancelReason);
            IsResearchButtonPressed = false;
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            IsResearchButtonPressed = false;
            if ((e.Touch.LocationOnScreen - ResearchButtonPosition).LengthSquared <= Radius * Radius)
            {
                // TODO TouchUp
            }
        }

        private void RenderResearchButton()
        {
            var colorCollection = GameSettings.VisualSettings.ColorCollection;
            DrawNode.Clear();
            Sprite.Visible = true;
            Sprite.Position = ResearchButtonPosition + new CCPoint(0, Radius / 8);
            Sprite.ContentSize = new CCSize(Radius * 1.5f, Radius * 1.5f);
            Sprite.Color = colorCollection.ResearchButtonLiquidColor;
            DrawNode.DrawCircle(ResearchButtonPosition, Radius, colorCollection.ResearchButtonBackground, IsResearchButtonPressed ? 8 : 4, colorCollection.ResearchButtonBorder);
        }
    }
}