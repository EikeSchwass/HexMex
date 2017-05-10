using CocosSharp;
using HexMex.Controls;

namespace HexMex.Scenes.Game
{
    public class MenuEntry : Control
    {
        public MenuEntry(string title, Font font, params MenuEntry[] entries)
        {
            Title = title;
            Entries = entries;
            var label = new CCLabel(Title, font.FontPath, font.FontSize, font.FontType);
            AddChild(label);
        }

        public Menu Menu { get; set; }
        public string Title { get; }
        public MenuEntry[] Entries { get; }

        public override bool IsPointInBounds(CCTouch position)
        {
            base.IsPointInBounds(position);
            var location = Menu.ScreenToWorldspace(position.LocationOnScreen);
            var convertToWorldspace = Menu.ConvertToWorldspace(Position);
            var point = location - convertToWorldspace;
            return point.Length <= 50;
        }

        public override bool OnTouchUp(CCTouch touch)
        {
            base.OnTouchUp(touch);
            if (Entries != null && Entries.Length > 0)
            {
                Menu.Push(Entries);
            }
            return true;
        }

        public override void OnTouchLeave(CCTouch touch)
        {
            base.OnTouchLeave(touch);
        }

        public override bool OnTouchDown(CCTouch touch)
        {
            base.OnTouchDown(touch);
            return true;
        }

        public override void OnTouchEnter(CCTouch touch)
        {
            base.OnTouchEnter(touch);
        }

        public override bool OnTouchMove(CCTouch touch)
        {
            base.OnTouchMove(touch);
            return true;
        }
    }
}