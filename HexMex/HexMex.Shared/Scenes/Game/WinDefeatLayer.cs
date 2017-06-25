using System;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Scenes.Game
{
    public class WinDefeatLayer : CCLayerColor
    {
        public sealed override bool Visible
        {
            get => base.Visible;
            set => base.Visible = value;
        }

        private ExtendedDrawNode DrawNode { get; } = new ExtendedDrawNode();

        public WinDefeatLayer() : base(new CCColor4B(0f, 0, 0, 0.5f))
        {
            AddChild(DrawNode);
            Visible = false;
        }

        public void ShowDefeatMessage(float duration, Action<WinDefeatLayer> callback)
        {
            Visible = true;
            DrawNode.DrawText(VisibleBoundsWorldspace.Center, "Defeat", Font.ArialFonts[50], VisibleBoundsWorldspace.Size);
            ScheduleOnce(f => callback(this), duration);
        }

        public void ShowVictoryMessage(float duration, Action<WinDefeatLayer> callback)
        {
            Visible = true;
            DrawNode.DrawText(VisibleBoundsWorldspace.Center, "Victory", Font.ArialFonts[50], VisibleBoundsWorldspace.Size);
            ScheduleOnce(f => callback(this), duration);
        }
    }
}