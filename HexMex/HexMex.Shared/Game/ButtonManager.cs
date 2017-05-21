using System.Collections.Generic;
using System.Linq;
using HexMex.Controls;
using HexMex.Game.Settings;

namespace HexMex.Game
{
    public class ButtonManager
    {
        public event ButtonListChangedEventHandler ButtonAdded;
        public event ButtonListChangedEventHandler ButtonRemoved;

        public Button this[HexagonNode hexagonNode] => Buttons.ContainsKey(hexagonNode) ? Buttons[hexagonNode] : null;
        public LayoutSettings LayoutSettings { get; }

        private Dictionary<HexagonNode, Button> Buttons { get; } = new Dictionary<HexagonNode, Button>();

        public ButtonManager(LayoutSettings layoutSettings)
        {
            LayoutSettings = layoutSettings;
        }

        public void AddButton(Button button, HexagonNode hexagonNode)
        {
            Buttons.Add(hexagonNode, button);
            ButtonAdded?.Invoke(this, button);
            button.Position = hexagonNode.GetWorldPosition(LayoutSettings.HexagonRadius, LayoutSettings.HexagonMargin);
        }

        public void RemoveButton(Button button)
        {
            if (Buttons.All(b => b.Value != button))
                return;
            var hexagonNode = Buttons.First(b => b.Value == button).Key;
            Buttons.Remove(hexagonNode);
            ButtonRemoved?.Invoke(this, button);
        }
    }
}