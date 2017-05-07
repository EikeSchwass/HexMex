using System;
using CocosSharp;

namespace HexMex.Controls
{
    public abstract class Button : Control
    {

        public event Action<CCTouch> TouchCancelled;

        public event Action<CCTouch> Touched;
        public event Action<CCTouch> Touching;

        private bool TouchStartedOnThis { get; set; }

        public virtual void OnRender() { }

        public override bool OnTouchDown(CCTouch touch)
        {
            base.OnTouchDown(touch);
            TouchStartedOnThis = true;
            Touching?.Invoke(touch);
            return true;
        }

        public override void OnTouchEnter(CCTouch touch)
        {
            base.OnTouchEnter(touch);
            TouchStartedOnThis = false;
        }

        public override void OnTouchLeave(CCTouch touch)
        {
            base.OnTouchLeave(touch);
            TouchStartedOnThis = false;
            TouchCancelled?.Invoke(touch);
        }

        public override bool OnTouchUp(CCTouch touch)
        {
            base.OnTouchUp(touch);
            bool startedHere = TouchStartedOnThis;
            TouchStartedOnThis = false;
            if (startedHere)
            {
                Touched?.Invoke(touch);
                return true;
            }
            return false;
        }
    }

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
            var t2 = ConvertToWorldspace(touch.LocationOnScreen);

            var p1 = Position;
            var p2 = ConvertToWorldspace(Position);
            var p3 = ScreenToWorldspace(Position);

            var delta = t1 - p1;
            delta = t1 - p2;
            delta = t1 - p3;
            delta = t2 - p1;
            delta = t2 - p2;
            delta = t1 - p1;
            return delta.Length < FontSize;
        }
    }
}