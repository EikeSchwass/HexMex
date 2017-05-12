using CocosSharp;
using HexMex.Controls;
using HexMex.Game.Buildings;
using HexMex.Helper;

namespace HexMex.Scenes.Game
{
    public partial class BuildMenuLayer
    {
        private class BuildMenuEntry : CCNode, IPointInBoundsCheck
        {
            private bool isPressed;
            private bool isSelected;
            private float margin = 8;

            public BuildMenuEntry(BuildingConstructionFactory factory, float width, float height)
            {
                Factory = factory;
                Width = width;
                Height = height;
                Label = new CCLabel(factory.BuildingInformation.Name, Font.BuildMenuButtonFont.FontPath, Font.BuildMenuButtonFont.FontSize, Font.BuildMenuButtonFont.FontType);
                Border = new CCDrawNode();
                AddChild(Border);
                AddChild(Label);
            }

            public CCDrawNode Border { get; }

            public BuildingConstructionFactory Factory { get; }

            public bool IsPressed
            {
                get => isPressed;
                set
                {
                    isPressed = value;
                    DrawBorder();
                }
            }

            public CCLabel Label { get; }

            public float Margin
            {
                get => margin;
                set
                {
                    margin = value;
                    DrawBorder();
                }
            }

            private float Height { get; }

            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    isSelected = value;
                    DrawBorder();
                }
            }

            private float Width { get; }

            public bool IsPointInBounds(CCTouch touch)
            {
                var globalPosition = this.GetGlobalPosition();
                var bounds = GetBounds(globalPosition);
                var position = ScreenToWorldspace(touch.LocationOnScreen);
                return bounds.ContainsPoint(position);
            }

            protected override void AddedToScene()
            {
                base.AddedToScene();
                DrawBorder();
            }

            private void DrawBorder()
            {
                Border.Clear();
                var labelBoundingBox = GetBounds(CCPoint.Zero);
                Border.DrawRect(labelBoundingBox, CCColor4B.Lerp(IsPressed ? CCColor4B.Gray : CCColor4B.Black, CCColor4B.Transparent, 0.5f), 1, IsSelected ? CCColor4B.Yellow : CCColor4B.White);
            }

            private CCRect GetBounds(CCPoint center)
            {
                return new CCRect(center.X - Width / 2 + Margin, center.Y - Height / 2 + Margin, Width - Margin * 2, Height - Margin * 2);
            }
        }
    }
}