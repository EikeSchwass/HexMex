using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Controls;

namespace HexMex.Scenes
{
    public class TouchHandler
    {
        private const float DRAGTHRESHOLD = 5;

        public TouchHandler(CCNode rootNode)
        {
            TouchMode = TouchMode.Idle;
            RootNode = rootNode;
            var touchListener = new CCEventListenerTouchOneByOne
            {
                OnTouchCancelled = TouchCancelled,
                OnTouchEnded = TouchEnded,
                OnTouchMoved = TouchMoved,
                OnTouchBegan = TouchBegan
            };
            rootNode.AddEventListener(touchListener);
        }

        public event Action<CCPoint> Dragging;
        public event Action<CCTouch, CCTouch> Pintching;

        public bool DraggingEnabled { get; set; } = true;
        public bool PintchingEnabled { get; set; } = true;
        public CCNode RootNode { get; }

        public TouchMode TouchMode { get; private set; }

        private CCPoint CurrentDragOffset { get; set; }

        private List<CCTouch> CurrentTouches { get; } = new List<CCTouch>();

        private void AddTouch(CCTouch touch)
        {
            CurrentTouches.Add(touch);
            if (CurrentTouches.Count == 1)
                TouchMode = TouchMode.Pressing;
            else if (PintchingEnabled)
            {
                TouchMode = TouchMode.Pintching;
            }
        }

        private void OnUpdateDrag(CCTouch touch)
        {
            var delta = touch.Delta * -1;
            delta += CurrentDragOffset;
            delta = new CCPoint(delta.X, delta.Y);

            Dragging?.Invoke(delta);
        }

        private void OnUpdatePintch()
        {
            Pintching?.Invoke(CurrentTouches[0], CurrentTouches[1]);
        }

        private void RemoveTouch(CCTouch touch)
        {
            CurrentTouches.Remove(touch);
            if (CurrentTouches.Count == 1)
            {
                TouchMode = DraggingEnabled ? TouchMode.Dragging : TouchMode.Idle;
            }
            else
                TouchMode = TouchMode.Idle;
        }

        private bool TouchBegan(CCTouch touch, CCEvent e)
        {
            if (CurrentTouches.Count >= 2)
            {
                e.StopPropogation();
                return false;
            }
            AddTouch(touch);
            if (TouchMode == TouchMode.Pressing)
                VisitControlTree(RootNode, touch, VisitTouchDown);
            return true;
        }

        private void TouchCancelled(CCTouch touch, CCEvent e)
        {
            RemoveTouch(touch);

            VisitControlTree(RootNode, touch, VisitTouchCancelled);
        }

        private void TouchEnded(CCTouch touch, CCEvent arg2)
        {
            RemoveTouch(touch);
            if (TouchMode == TouchMode.Idle)
                VisitControlTree(RootNode, touch, VisitTouchUp);
            else
                VisitControlTree(RootNode, touch, VisitTouchCancelled);
        }

        private void TouchMoved(CCTouch touch, CCEvent arg2)
        {
            if ((touch.StartLocation - touch.Location).Length < DRAGTHRESHOLD && TouchMode == TouchMode.Pressing)
            {
                VisitControlTree(RootNode, touch, VisitTouchMoved);
            }
            else
            {
                if (DraggingEnabled)
                    VisitControlTree(RootNode, touch, VisitTouchCancelled);
                if (TouchMode == TouchMode.Pintching)
                    OnUpdatePintch();
                else if (TouchMode != TouchMode.Dragging && DraggingEnabled)
                {
                    TouchMode = TouchMode.Dragging;
                    CurrentDragOffset = CCPoint.Zero;
                }
                else
                    OnUpdateDrag(touch);
            }
            VisitControlTree(RootNode, touch, VisitTouchMoved);
        }

        private void VisitControlTree(CCNode node, CCTouch touch, Func<Control, CCTouch, bool> touchedControlAction)
        {
            if (node is Control control)
            {
                if (!control.IsPointInBounds(touch.Location))
                {
                    if (control.IsTouched)
                    {
                        control.OnTouchLeave(touch);
                        control.IsTouched = false;
                    }
                    return;
                }
                var continueSearch = touchedControlAction(control, touch);
                if (!continueSearch)
                    return;
            }
            if (node?.Children == null || !node.Children.Any())
                return;
            foreach (var nodeChild in node.Children)
            {
                VisitControlTree(nodeChild, touch, touchedControlAction);
            }
        }

        private bool VisitTouchCancelled(Control control, CCTouch touch)
        {
            if (control.IsTouched)
                control.OnTouchLeave(touch);
            control.IsTouched = false;
            return true;
        }

        private bool VisitTouchDown(Control control, CCTouch touch)
        {
            if (!control.IsTouched)
                control.OnTouchEnter(touch);
            control.IsTouched = true;
            var handled = control.OnTouchDown(touch);
            return !handled;
        }

        private bool VisitTouchMoved(Control control, CCTouch touch)
        {
            if (!control.IsTouched)
                control.OnTouchEnter(touch);
            control.IsTouched = true;
            var handled = control.OnTouchMove(touch);
            return !handled;
        }

        private bool VisitTouchUp(Control control, CCTouch touch)
        {
            var handled = control.OnTouchUp(touch);
            control.IsTouched = false;
            return !handled;
        }
    }

    public enum TouchMode
    {
        Idle,
        Pressing,
        Dragging,
        Pintching
    }
}