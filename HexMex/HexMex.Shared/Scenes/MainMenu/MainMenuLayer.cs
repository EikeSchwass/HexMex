using System;
using System.Diagnostics;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Scenes.Game;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuLayer : CCLayer
    {
        private HexButton HelpButton { get; } = new HexButton("Help", 150, Font.MainMenuButtonFont);
        private HexButton OptionsButton { get; } = new HexButton("Options", 150, Font.MainMenuButtonFont);
        private HexButton StartGameButton { get; } = new HexButton("Start", 150, Font.MainMenuButtonFont);

        public MainMenuLayer()
        {
            StartGameButton.Touched += StartGameButton_Clicked;
            HelpButton.Touched += HelpButton_Clicked;
            OptionsButton.Touched += OptionsButton_Clicked;

            AddEventListener(new CCEventListenerTouchOneByOne { OnTouchBegan = TouchDown, OnTouchCancelled = OnTouchCancelled, OnTouchEnded = OnTouchUp, OnTouchMoved = OnTouchMoved });
        }

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

        private void HelpButton_Clicked(Button sender)
        {
            Debug.WriteLine("Help Button Clicked");
        }

        private void OnTouchCancelled(CCTouch arg1, CCEvent arg2)
        {
            StartGameButton.IsPressed = false;
            HelpButton.IsPressed = false;
            OptionsButton.IsPressed = false;
        }

        private void OnTouchMoved(CCTouch arg1, CCEvent arg2)
        {
            if (!StartGameButton.IsPointInBounds(arg1) && StartGameButton.IsPressed)
                StartGameButton.IsPressed = false;
            if (!HelpButton.IsPointInBounds(arg1) && HelpButton.IsPressed)
                HelpButton.IsPressed = false;
            if (!OptionsButton.IsPointInBounds(arg1) && OptionsButton.IsPressed)
                OptionsButton.IsPressed = false;
        }

        private void OnTouchUp(CCTouch arg1, CCEvent arg2)
        {
            if (StartGameButton.IsPointInBounds(arg1) && StartGameButton.IsPressed)
                StartGameButton.OnTouchUp();
            if (HelpButton.IsPointInBounds(arg1) && HelpButton.IsPressed)
                HelpButton.OnTouchUp();
            if (OptionsButton.IsPointInBounds(arg1) && OptionsButton.IsPressed)
                OptionsButton.OnTouchUp();
        }

        private void OptionsButton_Clicked(Button sender)
        {
            Debug.WriteLine("Options Button Clicked");
        }

        private void StartGameButton_Clicked(Button sender)
        {
            World world = new World(new GameSettings());
            Window.DefaultDirector.PushScene(new GameScene(Window, world));
            world.Initialize();
        }

        private bool TouchDown(CCTouch arg1, CCEvent arg2)
        {
            if (StartGameButton.IsPointInBounds(arg1))
            {
                StartGameButton.IsPressed = true;
            }
            if (HelpButton.IsPointInBounds(arg1))
            {
                HelpButton.IsPressed = true;
            }
            if (OptionsButton.IsPointInBounds(arg1))
            {
                OptionsButton.IsPressed = true;
            }
            return true;
        }
    }
}