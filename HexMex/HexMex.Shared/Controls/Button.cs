using System;
using CocosSharp;

namespace HexMex.Controls
{
    public abstract class Button : Control
    {

        public event Action<Button, CCTouch> TouchCancelled;

        public event Action<Button,CCTouch> Touched;
        public event Action<Button, CCTouch> Touching;

        private bool TouchStartedOnThis { get; set; }

        public virtual void OnRender() { }

        public override bool OnTouchDown(CCTouch touch)
        {
            base.OnTouchDown(touch);
            TouchStartedOnThis = true;
            Touching?.Invoke(this,touch);
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
            TouchCancelled?.Invoke(this,touch);
        }

        public override bool OnTouchUp(CCTouch touch)
        {
            base.OnTouchUp(touch);
            bool startedHere = TouchStartedOnThis;
            TouchStartedOnThis = false;
            if (startedHere)
            {
                Touched?.Invoke(this,touch);
                return true;
            }
            return false;
        }
    }
}