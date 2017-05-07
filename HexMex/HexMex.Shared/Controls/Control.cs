using System;
using CocosSharp;

namespace HexMex.Controls
{
    public class Control : CCNode
    {
        public virtual bool IsTouched { get; set; }

        public event Action<Control, CCTouch> TouchEntered;
        public event Action<Control, CCTouch> TouchLeft;
        public event Action<Control, CCTouch> TouchMoved;
        public event Action<Control, CCTouch> TouchDown;
        public event Action<Control, CCTouch> TouchUp;

        public virtual bool IsPointInBounds(CCTouch position)
        {
            return BoundingBoxTransformedToWorld.ContainsPoint(position.Location);
        }

        public virtual void OnTouchEnter(CCTouch touch)
        {
            TouchEntered?.Invoke(this, touch);
        }

        public virtual void OnTouchLeave(CCTouch touch)
        {
            TouchLeft?.Invoke(this, touch);
        }

        public virtual bool OnTouchMove(CCTouch touch)
        {
            TouchMoved?.Invoke(this, touch);
            return true;
        }

        public virtual bool OnTouchDown(CCTouch touch)
        {
            TouchDown?.Invoke(this, touch);
            return true;
        }

        public virtual bool OnTouchUp(CCTouch touch)
        {
            TouchUp?.Invoke(this, touch);
            return true;
        }


    }
}