using CocosSharp;

namespace HexMex.Controls
{
    public class TextButton : Button
    {
        public string Text { get; }
        public float FontSize { get; }

        public TextButton(string text, float fontSize)
        {
            Text = text;
            FontSize = fontSize;
            AddChild(new CCLabel(Text, "fonts/MarkerFelt-22.xnb", fontSize, CCLabelFormat.SystemFont));
        }

        public override bool IsPointInBounds(CCTouch touch)
        {
            var t1 = ScreenToWorldspace(touch.LocationOnScreen);

            var p1 = Position;

            var delta = t1 - p1;
            return delta.Length < FontSize;
        }
    }
}