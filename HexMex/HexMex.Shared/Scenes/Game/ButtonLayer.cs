using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public class ButtonLayer : TouchLayer
    {
        public event Action<ButtonLayer, BuildButton> ConstructionRequested;
        public event Action<ButtonLayer, StructureButton> DisplayStructureRequested;

        public ButtonManager ButtonManager { get; }
        private List<Button> Buttons { get; } = new List<Button>();
        private CCDrawNode DrawNode { get; } = new CCDrawNode();
        private GameSettings GameSettings { get; }

        private bool RedrawRequested { get; set; }

        public ButtonLayer(World world, HexMexCamera hexMexCamera) : base(hexMexCamera)
        {
            GameSettings = world.GameSettings;
            AddChild(DrawNode);
            ButtonManager = world.ButtonManager;
            ButtonManager.ButtonAdded += ButtonAdded;
            ButtonManager.ButtonRemoved += ButtonRemoved;
            Schedule();
        }

        public override void OnTouchCancelled(TouchEventArgs e, TouchCancelReason cancelReason)
        {
            base.OnTouchCancelled(e, cancelReason);
            foreach (var button in Buttons)
            {
                button.IsPressed = false;
            }
        }

        public override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            foreach (var button in Buttons)
            {
                if (IsPointInBounds(button, e.Touch))
                    button.IsPressed = true;
            }
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            foreach (var button in Buttons)
            {
                if (IsPointInBounds(button, e.Touch) && button.IsPressed)
                    button.OnTouchUp();
            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);
            if (!RedrawRequested)
                return;
            DrawNode.Clear();
            foreach (var button in Buttons.OfType<BuildButton>())
            {
                var position = button.Position;
                var delta = GameSettings.VisualSettings.BuildButtonRadius * 0.5f;
                DrawNode.DrawCircle(position, GameSettings.VisualSettings.BuildButtonRadius, GameSettings.VisualSettings.ColorCollection.BuildButtonBackground, GameSettings.VisualSettings.StructureBorderThickness, GameSettings.VisualSettings.ColorCollection.BuildButtonBorder);
                DrawNode.DrawSegment(position - CCPoint.AnchorUpperLeft * delta, position + CCPoint.AnchorUpperLeft * delta, GameSettings.VisualSettings.PlusCrossRadius, GameSettings.VisualSettings.ColorCollection.BuildButtonForeground);
                DrawNode.DrawSegment(position - CCPoint.AnchorLowerRight * delta, position + CCPoint.AnchorLowerRight * delta, GameSettings.VisualSettings.PlusCrossRadius, GameSettings.VisualSettings.ColorCollection.BuildButtonForeground);
            }
        }

        private void BuildButtonTouched(Button button)
        {
            ConstructionRequested?.Invoke(this, (BuildButton)button);
        }

        private void ButtonAdded(ButtonManager buttonManager, Button button)
        {
            Buttons.Add(button);
            RedrawRequested = true;

            if (button is BuildButton buildButton)
            {
                buildButton.Touched += BuildButtonTouched;
            }
            else if (button is StructureButton structureButton)
            {
                structureButton.Touched += StructureButtonTouched;
            }
            else
            {
                throw new NotSupportedException($"This type of button is not yet supported ({button.GetType()})");
            }
        }

        private void ButtonRemoved(ButtonManager buttonManager, Button button)
        {
            Buttons.Remove(button);
            RedrawRequested = true;
            if (button is BuildButton buildButton)
            {
                buildButton.Touched -= BuildButtonTouched;
            }
            else if (button is StructureButton structureButton)
            {
                structureButton.Touched -= StructureButtonTouched;
            }
            else
            {
                throw new NotSupportedException($"This type of button is not yet supported ({button.GetType()})");
            }
        }

        private bool IsPointInBounds(Button button, CCTouch eTouch)
        {
            var screenToWorldspace = ScreenToWorldspace(eTouch.LocationOnScreen);
            var globalPosition = button.GetGlobalPosition();
            return (screenToWorldspace - globalPosition).Length <= GameSettings.VisualSettings.BuildButtonRadius * 2;
        }

        private void StructureButtonTouched(Button button)
        {
            DisplayStructureRequested?.Invoke(this, (StructureButton)button);
        }
    }
}