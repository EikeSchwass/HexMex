using System.Collections.Generic;
using System.Linq;
using HexMex.Controls;

namespace HexMex.Game
{
    public class ButtonManager
    {
        public ButtonManager(WorldSettings worldSettings)
        {
            WorldSettings = worldSettings;
        }

        public event ButtonListChangedEventHandler ButtonAdded;
        public event ButtonListChangedEventHandler ButtonRemoved;

        public Button this[HexagonNode hexagonNode] => Buttons.ContainsKey(hexagonNode) ? Buttons[hexagonNode] : null;
        public WorldSettings WorldSettings { get; }

        private Dictionary<HexagonNode, Button> Buttons { get; } = new Dictionary<HexagonNode, Button>();

        public void AddButton(Button button, HexagonNode hexagonNode)
        {
            Buttons.Add(hexagonNode, button);
            ButtonAdded?.Invoke(this, button);
            button.Position = hexagonNode.GetWorldPosition(WorldSettings.HexagonRadius, WorldSettings.HexagonMargin);
        }

        public void RemoveButton(Button button)
        {
            var hexagonNode = Buttons.First(b => b.Value == button).Key;
            Buttons.Remove(hexagonNode);
            ButtonRemoved?.Invoke(this, button);
        }
    }
}