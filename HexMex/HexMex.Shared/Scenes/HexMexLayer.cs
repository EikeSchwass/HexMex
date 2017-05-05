using System;
using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Shared;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace HexMex.Scenes
{
    public abstract class HexMexLayer : CCLayerColor
    {
        protected HexMexLayer(CCColor4B color) : base(color)
        {
            var touchListener = new CCEventListenerTouchOneByOne
            {
                OnTouchCancelled = TouchCancelled,
                OnTouchEnded = TouchEnded,
                OnTouchMoved = TouchMoved,
                OnTouchBegan = TouchBegan
            };
            AddEventListener(touchListener);
            Schedule();
        }

        private void ButtonStatusChanged(CCEventGamePadButton gamePadButton)
        {
            if (gamePadButton.Back == CCGamePadButtonStatus.Pressed)
            {
                if (Window.DefaultDirector.CanPopScene)
                    Window.DefaultDirector.PopScene();
                else
                    Window.Application.ToggleFullScreen();
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                if (Window.DefaultDirector.CanPopScene)
                    Window.DefaultDirector.PopScene();
                else
                    ((AppDelegate)Window.Application.ApplicationDelegate).AppSuspender.Suspend();
            }
        }

        public HexMexScene HexMexScene => Scene as HexMexScene;

        private bool TouchBegan(CCTouch touch, CCEvent e)
        {
            VisitControlTree(this, touch, control =>
                                          {
                                              if (!control.IsTouched)
                                                  control.OnTouchEnter(touch);
                                              control.IsTouched = true;
                                              var handled = control.OnTouchDown(touch);
                                              return !handled;
                                          });

            return true;
        }

        private void TouchCancelled(CCTouch touch, CCEvent e)
        {
            VisitControlTree(this, touch, control =>
                                          {
                                              if (control.IsTouched)
                                                  control.OnTouchLeave(touch);
                                              control.IsTouched = false;
                                              return true;
                                          });
        }

        private void TouchEnded(CCTouch touch, CCEvent arg2)
        {
            VisitControlTree(this, touch, control =>
                                          {
                                              var handled = control.OnTouchUp(touch);
                                              control.IsTouched = false;
                                              return !handled;
                                          });
        }

        private void TouchMoved(CCTouch touch, CCEvent arg2)
        {
            VisitControlTree(this, touch, control =>
                                          {
                                              if (!control.IsTouched)
                                                  control.OnTouchEnter(touch);
                                              control.IsTouched = true;
                                              var handled = control.OnTouchMove(touch);
                                              return !handled;
                                          });
        }

        private void VisitControlTree(CCNode node, CCTouch touch, Func<Control, bool> touchedControlAction)
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
                var continueSearch = touchedControlAction(control);
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
    }
}