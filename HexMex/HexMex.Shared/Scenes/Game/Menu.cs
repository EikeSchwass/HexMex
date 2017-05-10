using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public class Menu : Control
    {
        public Menu(Control backButton, WorldSettings worldSettings, params MenuEntry[] menuEntries)
        {
            BackgroundNode = new CCDrawNode();
            BackButton = backButton;
            WorldSettings = worldSettings;
            BackButton.TouchUp += BackButtonTouched;
            MenuRadius = WorldSettings.MenuRadius;
            Push(menuEntries);
            Initialize();
        }

        public bool HasBackButton => MenuEntries.Count > 1;
        public bool IsOpen { get; private set; }

        public float MenuRadius { get; }
        public WorldSettings WorldSettings { get; }

        private Control BackButton { get; }
        private CCDrawNode BackgroundNode { get; }
        private Stack<MenuEntry[]> MenuEntries { get; } = new Stack<MenuEntry[]>();

        public void Close()
        {
            Visible = false;
            IsOpen = false;
        }

        public override bool IsPointInBounds(CCTouch position)
        {
            base.IsPointInBounds(position);
            var location = ScreenToWorldspace(position.LocationOnScreen);
            return (location - Position).Length <= MenuRadius;
        }

        public void Open(CCPoint position)
        {
            Visible = true;
            Position = position;
            IsOpen = true;
        }


        public void Push(params MenuEntry[] menuEntries)
        {
            foreach (var menuEntry in menuEntries)
            {
                menuEntry.Menu = this;
            }
            MenuEntries.Push(menuEntries);
            LoadMenuEntries();
            BackButton.Visible = HasBackButton;
        }

        private void BackButtonTouched(Control arg1, CCTouch arg2)
        {
            Pop();
        }

        private void Initialize()
        {
            AddChild(BackgroundNode);
            AddChild(BackButton);
            BackButton.Position = CCPoint.Zero;
            BackgroundNode.Position = CCPoint.Zero;

            CCPoint[] corners = new CCPoint[6];
            double angle = PI * 2 / 6;
            for (int i = 0; i < 6; i++)
            {
                var x = Sin(i * angle) * MenuRadius;
                var y = Cos(i * angle) * MenuRadius;
                corners[i] = new CCPoint((float)x, (float)y);
            }
            BackgroundNode.DrawPolygon(corners, 6, WorldSettings.MenuBackgroundColor, 1, CCColor4B.White);
        }

        private void LoadMenuEntries()
        {
            foreach (var menuEntry in Children?.OfType<MenuEntry>()??Enumerable.Empty<MenuEntry>())
            {
                RemoveChild(menuEntry);
            }
            var menuEntries = MenuEntries.Peek();
            double angle = PI * 2 / 6;
            for (var i = 0; i < Min(menuEntries.Length, 6); i++)
            {
                var menuEntry = menuEntries[i];
                AddChild(menuEntry);
                var x = Sin(i * angle + PI / 6) * MenuRadius / 2;
                var y = Cos(i * angle + PI / 6) * MenuRadius / 2;
                menuEntry.Position = new CCPoint((float)x, (float)y);
            }
        }

        private void Pop()
        {
            MenuEntries.Pop();
            LoadMenuEntries();
            BackButton.Visible = HasBackButton;
        }
    }
}