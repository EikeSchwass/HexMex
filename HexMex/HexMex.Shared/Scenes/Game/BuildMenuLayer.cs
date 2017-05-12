using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Buildings;
using HexMex.Helper;
using static System.Math;

namespace HexMex.Scenes.Game
{
    public partial class BuildMenuLayer : TouchLayer, IPointInBoundsCheck
    {
        private BuildMenuEntry selectedEntry;

        public BuildMenuLayer(HexMexCamera hexMexCamera)
        {
            HexMexCamera = hexMexCamera;
            HexMexCamera.PositionUpdated += (c, p) => UpdatePosition();
            HexMexCamera.ZoomUpdated += (c, p) => UpdatePosition();
            Visible = false;
        }

        public sealed override bool Visible { get; set; }

        public CCNode AllMenuEntriesArea { get; } = new CCNode();

        public CCNode SelectedMenuEntryArea { get; } = new CCNode();

        private CCRect ClientRectangle { get; set; }

        private HexMexCamera HexMexCamera { get; }

        private List<BuildMenuEntry> MenuEntries { get; } = new List<BuildMenuEntry>();

        private CCDrawNode MenuOutlineNode { get; } = new CCDrawNode();

        private CCDrawNode RenderLinesNode { get; } = new CCDrawNode();
        //private CCNode RootNode { get; } = new CCNode();

        private BuildMenuEntry SelectedEntry
        {
            get => selectedEntry;
            set
            {
                selectedEntry = value;
                RenderSelectedEntry();
            }
        }

        private BuildButton Target { get; set; }

        public void DisplayBuildMenuFor(BuildButton buildButton)
        {
            Visible = true;
            Target = buildButton;
            UpdatePosition();
        }

        public bool IsPointInBounds(CCTouch touch)
        {
            var screenToWorldspace = ScreenToWorldspace(touch.LocationOnScreen);
            var diff = screenToWorldspace - Position;
            diff = new CCPoint(Abs(diff.X), Abs(diff.Y));
            return !(diff.X > ClientRectangle.Size.Width / 2 || diff.Y > ClientRectangle.Size.Height / 2);
        }

        public override void OnTouchCancelled(TouchEventArgs e, TouchCancelReason cancelReason)
        {
            base.OnTouchCancelled(e, cancelReason);
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            if (!Visible)
                return;
            if (!IsPointInBounds(e.Touch))
                return;
            e.Handled = true;
            foreach (var menuEntry in MenuEntries)
            {
                if (menuEntry.IsPointInBounds(e.Touch))
                    menuEntry.IsPressed = true;
                else
                    menuEntry.IsPressed = false;
            }
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            if (!Visible)
                return;

            if (!IsPointInBounds(e.Touch))
            {
                Visible = false;
            }
            else
                e.Handled = true;

            foreach (var menuEntry in MenuEntries)
            {
                if (menuEntry.IsPointInBounds(e.Touch) && menuEntry.IsPressed)
                {
                    menuEntry.IsSelected = true;
                    SelectedEntry = menuEntry;
                }
                menuEntry.IsPressed = false;
            }

            foreach (var menuEntry in MenuEntries)
            {
                if (SelectedEntry != menuEntry)
                    menuEntry.IsSelected = false;
            }
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var clientSize = VisibleBoundsWorldspace.Size * 0.6f;

            ClientRectangle = new CCRect(-clientSize.Width / 2, -clientSize.Height / 2, clientSize.Width, clientSize.Height);
            UpdatePosition();
            AllMenuEntriesArea.Position = new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY);
            SelectedMenuEntryArea.Position = new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY - 2 * ClientRectangle.Size.Height / 3);
            AddChild(RenderLinesNode);
            AddChild(MenuOutlineNode);
            AddChild(AllMenuEntriesArea);

            const int columnCount = 3;
            float columnWidth = ClientRectangle.Size.Width / columnCount;
            float columnHeight = 100;

            for (int i = 0; i < BuildingConstructionFactory.Factories.Count; i++)
            {
                var factory = BuildingConstructionFactory.Factories.ElementAt(i).Value;
                var buildMenuEntry = new BuildMenuEntry(factory, columnWidth, columnHeight);
                AllMenuEntriesArea.AddChild(buildMenuEntry);
                int row = i / columnCount;
                int column = i % columnCount;
                float x = column * columnWidth + columnWidth / 2;
                float y = -row * columnHeight - columnHeight / 2;
                buildMenuEntry.Position = new CCPoint(x, y);
                MenuEntries.Add(buildMenuEntry);
            }

            RenderMenu();
        }

        private void RenderLines()
        {
            if (Target == null)
                return;
            RenderLinesNode.Clear();
            var targetPos = Target.GetGlobalPosition();
            targetPos = ConvertToWorldspace(targetPos) * HexMexCamera.ZoomFactor - HexMexCamera.Position * HexMexCamera.ZoomFactor;
            RenderLinesNode.DrawLine(new CCPoint(ClientRectangle.MinX, ClientRectangle.MinY), targetPos, 1, CCColor4B.White);
            RenderLinesNode.DrawLine(new CCPoint(ClientRectangle.MaxX, ClientRectangle.MinY), targetPos, 1, CCColor4B.White);
            RenderLinesNode.DrawLine(new CCPoint(ClientRectangle.MinX, ClientRectangle.MaxY), targetPos, 1, CCColor4B.White);
            RenderLinesNode.DrawLine(new CCPoint(ClientRectangle.MaxX, ClientRectangle.MaxY), targetPos, 1, CCColor4B.White);
        }

        private void RenderMenu()
        {
            MenuOutlineNode.Clear();
            MenuOutlineNode.DrawRect(ClientRectangle, ColorCollection.BuildMenuBackgroundColor, 1, CCColor4B.White);
        }

        private void RenderSelectedEntry()
        {
            SelectedMenuEntryArea.Children?.Clear();
            throw new NotImplementedException("");
            // TODO Draw slected area and add button for construction (see onenote)
        }

        private void UpdatePosition()
        {
            if (Target == null)
                return;
            Position = VisibleBoundsWorldspace.Center;
            RenderLines();
        }
    }
}