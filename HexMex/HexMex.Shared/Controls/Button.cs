using System;
using CocosSharp;

namespace HexMex.Controls
{
    public class Button : Control
    {
        private bool TouchStartedOnThis { get; set; }

        public event Action<CCTouch> Touched;
        public event Action<CCTouch> Touching;
        public event Action<CCTouch> TouchCancelled;

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

        public override bool OnTouchDown(CCTouch touch)
        {
            base.OnTouchDown(touch);
            TouchStartedOnThis = true;
            Touching?.Invoke(touch);
            return true;
        }

        public override void OnTouchLeave(CCTouch touch)
        {
            base.OnTouchLeave(touch);
            TouchStartedOnThis = false;
            TouchCancelled?.Invoke(touch);
        }

        public override void OnTouchEnter(CCTouch touch)
        {
            base.OnTouchEnter(touch);
            TouchStartedOnThis = false;
        }
    }
}