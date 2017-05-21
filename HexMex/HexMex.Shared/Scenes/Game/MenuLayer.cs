using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;

namespace HexMex.Scenes.Game
{
    public class MenuLayer : TouchLayer
    {
        public sealed override bool Visible { get; set; }
        public VisualSettings VisualSettings { get; }
        public CCRect ClientRectangle { get; private set; }
        public HexMexCamera HexMexCamera { get; private set; }
        private Menu Menu { get; set; }
        public ExtendedDrawNode DrawNode { get; } = new ExtendedDrawNode();

        public MenuLayer(World world, HexMexCamera hexMexCamera)
        {
            VisualSettings = world.GameSettings.VisualSettings;
            HexMexCamera = hexMexCamera;
            Visible = false;
            AddChild(DrawNode);
        }

        public void DisplayMenu(Menu menu)
        {
            Visible = true;
            Menu = menu;
            Menu.Host = this;
            Menu.AddedToScene();
        }

        public bool IsPointInBounds(CCTouch touch)
        {
            var screenToWorldspace = ScreenToWorldspace(touch.LocationOnScreen);
            var isPointInBounds = ClientRectangle.ContainsPoint(screenToWorldspace);
            return isPointInBounds;
        }

        public override void OnTouchCancelled(TouchEventArgs e, TouchCancelReason cancelReason)
        {
            base.OnTouchCancelled(e, cancelReason);
            Menu?.TouchCancel(ScreenToWorldspace(e.Touch.LocationOnScreen) - new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY), cancelReason);
        }

        public override bool BlockDragOrPintch(CCTouch touch)
        {
            return Visible;
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            if (!Visible)
                return;
            e.Handled = true;
            if (!IsPointInBounds(e.Touch))
                return;
            Menu.TouchDown(ScreenToWorldspace(e.Touch.LocationOnScreen)-new CCPoint(ClientRectangle.MinX,ClientRectangle.MaxY));
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            if (!Visible)
                return;

            e.Handled = true;
            if (!IsPointInBounds(e.Touch))
            {
                Visible = false;
                return;
            }
            Menu.TouchUp(ScreenToWorldspace(e.Touch.LocationOnScreen) - new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY));
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            float width = VisibleBoundsWorldspace.Size.Width * 0.6f;
            var height = VisibleBoundsWorldspace.Size.Height * 0.8f;
            ClientRectangle = new CCRect((VisibleBoundsWorldspace.Size.Width - width) / 2, (VisibleBoundsWorldspace.Size.Height - height) / 2, width, height);

            DrawNode.Position = new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY);
        }

        public void Close()
        {
            Visible = false;
        }
    }
}