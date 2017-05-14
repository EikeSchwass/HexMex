using System;
using System.Collections.Generic;
using HexMex.Controls;
using HexMex.Game;

namespace HexMex.Scenes.Game
{
    public class ButtonLayer : TouchLayer
    {
        public event Action<ButtonLayer, BuildButton> ConstructionRequested;

        public ButtonManager ButtonManager { get; }
        private List<Button> Buttons { get; } = new List<Button>();

        public ButtonLayer(World world, HexMexCamera hexMexCamera) : base(hexMexCamera)
        {
            ButtonManager = world.ButtonManager;
            ButtonManager.ButtonAdded += StructureAdded;
            ButtonManager.ButtonRemoved += StructureRemoved;
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
                if (button.IsPointInBounds(e.Touch))
                    button.IsPressed = true;
            }
        }

        public override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            foreach (var button in Buttons)
            {
                if (button.IsPointInBounds(e.Touch) && button.IsPressed)
                    button.OnTouchUp();
            }
        }

        private void BuildButtonTouched(Button button)
        {
            ConstructionRequested?.Invoke(this, (BuildButton)button);
        }

        private void StructureAdded(ButtonManager buttonManager, Button button)
        {
            AddChild(button);
            Buttons.Add(button);

            if (button is BuildButton buildButton)
            {
                buildButton.Touched += BuildButtonTouched;
            }
            else
            {
                throw new NotSupportedException($"This type of button is not yet supported ({button.GetType()})");
            }
        }

        private void StructureRemoved(ButtonManager buttonManager, Button button)
        {
            RemoveChild(button);
            Buttons.Remove(button);
            if (button is BuildButton buildButton)
            {
                buildButton.Touched -= BuildButtonTouched;
            }
            else
            {
                throw new NotSupportedException($"This type of button is not yet supported ({button.GetType()})");
            }
        }
    }
}