using System;
using CocosSharp;

namespace HexMex.Controls
{
    public abstract class Button : CCNode, IPointInBoundsCheck
    {
        private bool isPressed;

        public bool IsPressed
        {
            get => isPressed;
            set
            {
                isPressed = value;
                OnIsPressedChanged();
            }
        }

        protected virtual void OnIsPressedChanged() { }

        public event Action<Button> Touched;

        public void OnTouchUp()
        {
            if (IsPressed)
            {
                IsPressed = false;
                Touched?.Invoke(this);
            }
        }

        public abstract bool IsPointInBounds(CCTouch position);
    }
}