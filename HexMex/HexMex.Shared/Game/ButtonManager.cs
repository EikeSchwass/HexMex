using System.Collections.Generic;
using System.Linq;
using HexMex.Controls;

namespace HexMex.Game
{
    public class ButtonManager
    {
        public World World { get; }

        private Dictionary<HexagonNode, Button> Buttons { get; } = new Dictionary<HexagonNode, Button>();

        public event ButtonListChangedEventHandler ButtonAdded;
        public event ButtonListChangedEventHandler ButtonRemoved;

        public ButtonManager(World world)
        {
            World = world;
        }

        public void AddButton(Button button, HexagonNode hexagonNode)
        {
            Buttons.Add(hexagonNode, button);
            ButtonAdded?.Invoke(this, button);
            button.Position = hexagonNode.GetWorldPosition(World.WorldSettings.HexagonRadius, World.WorldSettings.HexagonMargin);
        }

        public Button this[HexagonNode hexagonNode] => Buttons.ContainsKey(hexagonNode) ? Buttons[hexagonNode] : null;

        public void RemoveButton(Button button)
        {
            var hexagonNode = Buttons.First(b => b.Value == button).Key;
            Buttons.Remove(hexagonNode);
            ButtonRemoved?.Invoke(this, button);
        }
    }
}