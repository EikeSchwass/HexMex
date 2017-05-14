using System;
using CocosSharp;

namespace HexMex.Controls
{
    public abstract class Button : CCNode, IPointInBoundsCheck
    {
        private bool isPressed;

        public event Action<Button> Touched;

        public bool IsPressed
        {
            get => isPressed;
            set
            {
                isPressed = value;
                OnIsPressedChanged();
            }
        }

        public abstract bool IsPointInBounds(CCTouch position);

        public void OnTouchUp()
        {
            if (IsPressed)
            {
                IsPressed = false;
                Touched?.Invoke(this);
            }
        }

        protected virtual void OnIsPressedChanged()
        {
        }
    }
}