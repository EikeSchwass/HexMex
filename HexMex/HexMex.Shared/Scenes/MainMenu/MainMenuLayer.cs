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
            AddChild(StartGameButton);
            AddChild(HelpButton);
            AddChild(OptionsButton);

            StartGameButton.Touched += StartGameButton_Clicked;
            HelpButton.Touched += HelpButton_Clicked;
            OptionsButton.Touched += OptionsButton_Clicked;

            TouchHandler = new TouchHandler(this) { PintchingEnabled = false, DraggingEnabled = false };

            Schedule();
        }

        private float AnimationTime { get; set; }

        private HexButton HelpButton { get; } = new HexButton("Help", 150);
        private HexButton OptionsButton { get; } = new HexButton("Options", 150);
        private HexButton StartGameButton { get; } = new HexButton("Start", 150);
        private TouchHandler TouchHandler { get; }

        public override void Update(float dt)
        {
            base.Update(dt);
            AnimationTime += dt * AnimationSpeed;
            StartGameButton.BorderThickness = (float)(Math.Sin(AnimationTime) * 2 + 4);
            HelpButton.BorderThickness = (float)(Math.Sin(AnimationTime) + 4);
            OptionsButton.BorderThickness = (float)(Math.Sin(AnimationTime) + 4);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var bounds = VisibleBoundsWorldspace;

            float centerX = bounds.MidX;
            float centerY = bounds.MidY;
            var d = Math.Sin(Math.PI / 3) * HelpButton.Radius * 2;
            var deltaX = Math.Sin(Math.PI / 6) * d;
            var deltaY = Math.Cos(Math.PI / 6) * d;

            StartGameButton.PositionX = centerX;
            StartGameButton.PositionY = centerY + StartGameButton.Radius;
            HelpButton.PositionX = (float)(StartGameButton.PositionX - deltaX);
            HelpButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
            OptionsButton.PositionX = (float)(StartGameButton.PositionX + deltaX);
            OptionsButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
        }

        private void HelpButton_Clicked(CCTouch obj)
        {
            Debug.WriteLine("Help Button Clicked");
        }

        private void OptionsButton_Clicked(CCTouch obj)
        {
            Debug.WriteLine("Options Button Clicked");
        }

        private void StartGameButton_Clicked(CCTouch obj)
        {
            World world = new World(new WorldSettings());
            Window.DefaultDirector.PushScene(new GameScene(Window, HexMexScene.DataLoader, world));
            world.Initialize();


        }
    }
}