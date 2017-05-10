using System;
using System.Diagnostics;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Scenes.Game;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuLayer : HexMexLayer
    {
        public const float AnimationSpeed = 1.25f;

        public MainMenuLayer() : base(CCColor4B.Black)
        {

            StartGameButton.Touched += StartGameButton_Clicked;
            HelpButton.Touched += HelpButton_Clicked;
            OptionsButton.Touched += OptionsButton_Clicked;

            TouchHandler = new TouchHandler(this) { PintchingEnabled = false, DraggingEnabled = false };

        }

        private HexButton HelpButton { get; } = new HexButton("Help", 150, Font.MainMenuButtonFont);
        private HexButton OptionsButton { get; } = new HexButton("Options", 150, Font.MainMenuButtonFont);
        private HexButton StartGameButton { get; } = new HexButton("Start", 150, Font.MainMenuButtonFont);
        private TouchHandler TouchHandler { get; }


        protected override void AddedToScene()
        {
            base.AddedToScene();
            var hexMexCamera = new HexMexCamera(VisibleBoundsWorldspace.Size);
            Camera = hexMexCamera;

            var d = Math.Sin(Math.PI / 3) * HelpButton.Radius * 2;
            var deltaX = Math.Sin(Math.PI / 6) * d;
            var deltaY = Math.Cos(Math.PI / 6) * d;

            StartGameButton.PositionX = 0;
            StartGameButton.PositionY = 0 + StartGameButton.Radius;
            HelpButton.PositionX = (float)(StartGameButton.PositionX - deltaX);
            HelpButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
            OptionsButton.PositionX = (float)(StartGameButton.PositionX + deltaX);
            OptionsButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
            AddChild(StartGameButton);
            AddChild(HelpButton);
            AddChild(OptionsButton);
            hexMexCamera.MoveToPosition(CCPoint.Zero);
        }

        private void HelpButton_Clicked(Button sender, CCTouch obj)
        {
            Debug.WriteLine("Help Button Clicked");
        }

        private void OptionsButton_Clicked(Button sender, CCTouch obj)
        {
            Debug.WriteLine("Options Button Clicked");
        }

        private void StartGameButton_Clicked(Button sender, CCTouch obj)
        {
            World world = new World(new WorldSettings());
            Window.DefaultDirector.PushScene(new GameScene(Window, HexMexScene.DataLoader, world));
            world.Initialize();
        }
    }
}