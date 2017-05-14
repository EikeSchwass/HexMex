using System.Collections.Generic;
using CocosSharp;
using HexMex.Scenes.Game;

namespace HexMex.Scenes
{
    public class GameTouchHandler
    {
        private const float MINTOUCHDISTANCE = 10;

        public GameLayer GameLayer { get; }
        public HexMexCamera HexMexCamera { get; }

        public double MinDragDistance => 10;
        public TouchMode TouchMode { get; private set; } = TouchMode.Idle;
        private CCPoint? StartPosition { get; set; } = CCPoint.Zero;
        private float StartZoom { get; set; } = 1;

        private List<CCTouch> Touches { get; } = new List<CCTouch>();

        public GameTouchHandler(GameLayer gameLayer, HexMexCamera hexMexCamera)
        {
            GameLayer = gameLayer;
            HexMexCamera = hexMexCamera;
            var eventListener = new CCEventListenerTouchOneByOne {IsEnabled = true, OnTouchBegan = OnTouchBegan, OnTouchCancelled = OnTouchCancelled, OnTouchEnded = OnTouchEnded, OnTouchMoved = OnTouchMoved};
            gameLayer.AddEventListener(eventListener);
        }

        private void Dragging(CCPoint delta)
        {
            HexMexCamera.MoveToPosition(HexMexCamera.Position - delta / HexMexCamera.ZoomFactor);
        }

        private bool OnTouchBegan(CCTouch arg1, CCEvent arg2)
        {
            var touchEventArgs = new TouchEventArgs(arg1);
            Touches.Add(arg1);
            if (Touches.Count == 1)
            {
                TouchMode = TouchMode.Pressing;
                foreach (var touchLayer in GameLayer.TouchLayers)
                {
                    touchLayer.OnTouchDown(touchEventArgs);
                    if (touchEventArgs.Handled)
                        break;
                }
            }
            else if (Touches.Count == 2)
            {
                TouchMode = TouchMode.Pintching;
                foreach (var touchLayer in GameLayer.TouchLayers)
                {
                    touchLayer.OnTouchCancelled(touchEventArgs, TouchCancelReason.PintchingStarted);
                }
            }
            return true;
        }

        private void OnTouchCancelled(CCTouch arg1, CCEvent arg2)
        {
            var touchEventArgs = new TouchEventArgs(arg1);
            Touches.Remove(arg1);
            foreach (var touchLayer in GameLayer.TouchLayers)
            {
                touchLayer.OnTouchCancelled(touchEventArgs, TouchCancelReason.UserCancelled);
            }
            if (Touches.Count == 0)
            {
                TouchMode = TouchMode.Idle;
                StartPosition = null;
            }
            else if (Touches.Count == 1)
            {
                TouchMode = TouchMode.Dragging;
            }
        }

        private void OnTouchEnded(CCTouch arg1, CCEvent arg2)
        {
            var touchEventArgs = new TouchEventArgs(arg1);
            Touches.Remove(arg1);
            if (Touches.Count == 0)
            {
                if (TouchMode == TouchMode.Pressing)
                {
                    foreach (var touchLayer in GameLayer.TouchLayers)
                    {
                        touchLayer.OnTouchUp(touchEventArgs);
                        if (touchEventArgs.Handled)
                            break;
                    }
                }
                TouchMode = TouchMode.Idle;
                StartPosition = null;
            }
            else
            {
                TouchMode = TouchMode.Dragging;
            }
        }

        private void OnTouchMoved(CCTouch arg1, CCEvent arg2)
        {
            var touchEventArgs = new TouchEventArgs(arg1);
            if (Touches.Count == 1) // Dragging or Pressing
            {
                if (TouchMode == TouchMode.Pressing)
                {
                    if ((arg1.StartLocation - arg1.Location).Length > MinDragDistance)
                    {
                        TouchMode = TouchMode.Dragging;
                        foreach (var touchLayer in GameLayer.TouchLayers)
                        {
                            touchLayer.OnTouchCancelled(touchEventArgs, TouchCancelReason.DraggingStarted);
                        }
                    }
                }
                else if (TouchMode == TouchMode.Dragging)
                {
                    bool draggingBlocked = false;
                    foreach (var touchLayer in GameLayer.TouchLayers)
                    {
                        if (touchLayer.BlockDragOrPintch(arg1))
                            draggingBlocked = true;
                    }
                    if (!draggingBlocked)
                        Dragging(arg1.Delta);
                }
            }
            else if (Touches.Count == 2 && TouchMode == TouchMode.Pintching)
            {
                bool draggingBlocked = false;
                foreach (var touchLayer in GameLayer.TouchLayers)
                {
                    if (touchLayer.BlockDragOrPintch(arg1))
                        draggingBlocked = true;
                }
                if (!draggingBlocked)
                    Pintching(Touches[0], Touches[1]);
            }
        }

        private void Pintching(CCTouch touch1, CCTouch touch2)
        {
            var prevDiff = (touch1.PreviousLocation - touch2.PreviousLocation).Length;
            var currentDiff = (touch1.Location - touch2.Location).Length;
            if (currentDiff < MINTOUCHDISTANCE)
                return;

            var sizeFactor = currentDiff / prevDiff;
            sizeFactor = sizeFactor - (sizeFactor - 1) / 2;

            HexMexCamera.SetZoomFactor(sizeFactor * HexMexCamera.ZoomFactor);
        }
    }

    public enum TouchCancelReason
    {
        UserCancelled,
        DraggingStarted,
        PintchingStarted,
        Other
    }
}