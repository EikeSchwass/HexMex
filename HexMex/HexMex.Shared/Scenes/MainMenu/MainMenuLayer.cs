using System;
using CocosSharp;
using HexMex.Controls;
using HexMex.Game;
using HexMex.Game.Settings;
using HexMex.Scenes.Game;
using HexMex.Scenes.Help;

namespace HexMex.Scenes.MainMenu
{
    public class MainMenuLayer : CCLayer
    {
        public BuildingDescriptionDatabase BuildingDescriptionDatabase { get; }
        public ColorCollectionFile ColorCollectionFile { get; }
        public LanguageSettings LanguageSettings { get; }
        public VisualSettings VisualSettings { get; }
        private HexButton HelpButton { get; }
        //private HexButton OptionsButton { get; }
        private HexButton StartGameButton { get; }

        public MainMenuLayer(BuildingDescriptionDatabase buildingDescriptionDatabase, ColorCollectionFile colorCollectionFile, LanguageSettings languageSettings)
        {
            var colorCollection = new ColorCollection(colorCollectionFile);
            HelpButton = new HexButton("Help", 150, colorCollection);
            //OptionsButton = new HexButton("Options", 150, colorCollection);
            StartGameButton = new HexButton("Start", 150, colorCollection);

            BuildingDescriptionDatabase = buildingDescriptionDatabase;
            ColorCollectionFile = colorCollectionFile;
            LanguageSettings = languageSettings;
            VisualSettings = new VisualSettings(colorCollectionFile);
            StartGameButton.Touched += StartGameButton_Clicked;
            HelpButton.Touched += HelpButton_Clicked;
            //OptionsButton.Touched += OptionsButton_Clicked;

            AddEventListener(new CCEventListenerTouchOneByOne { OnTouchBegan = TouchDown, OnTouchCancelled = OnTouchCancelled, OnTouchEnded = OnTouchUp, OnTouchMoved = OnTouchMoved });
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var actualSize = Scene.Window.WindowSizeInPixels;
            var hexMexCamera = new HexMexCamera(VisibleBoundsWorldspace.Size);
            Camera = hexMexCamera;

            var radius = actualSize.Width / 4 * 1f;
            StartGameButton.Radius = radius;
            HelpButton.Radius = radius;
            //OptionsButton.Radius = radius;

            var d = Math.Sin(Math.PI / 3) * radius * 2;
            var deltaX = Math.Sin(Math.PI / 6) * d;
            var deltaY = Math.Cos(Math.PI / 6) * d;

            StartGameButton.PositionX = (float)(deltaX / 2);
            StartGameButton.PositionY = 0 + StartGameButton.Radius;
            HelpButton.PositionX = (float)(StartGameButton.PositionX - deltaX);
            HelpButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
            //OptionsButton.PositionX = (float)(StartGameButton.PositionX + deltaX);
            //OptionsButton.PositionY = (float)(StartGameButton.PositionY - deltaY);
            AddChild(StartGameButton);
            AddChild(HelpButton);
            //AddChild(OptionsButton);
            hexMexCamera.MoveToPosition(CCPoint.Zero);
        }

        private void HelpButton_Clicked(Button sender)
        {
            Window.DefaultDirector.PushScene(new HelpScene(Window, VisualSettings, LanguageSettings));
        }

        private void OnTouchCancelled(CCTouch arg1, CCEvent arg2)
        {
            StartGameButton.IsPressed = false;
            HelpButton.IsPressed = false;
            //OptionsButton.IsPressed = false;
        }

        private void OnTouchMoved(CCTouch arg1, CCEvent arg2)
        {
            if (!StartGameButton.IsPointInBounds(arg1) && StartGameButton.IsPressed)
                StartGameButton.IsPressed = false;
            if (!HelpButton.IsPointInBounds(arg1) && HelpButton.IsPressed)
                HelpButton.IsPressed = false;
            /*if (!OptionsButton.IsPointInBounds(arg1) && OptionsButton.IsPressed)
                OptionsButton.IsPressed = false;*/
        }

        private void OnTouchUp(CCTouch arg1, CCEvent arg2)
        {
            if (StartGameButton.IsPointInBounds(arg1) && StartGameButton.IsPressed)
                StartGameButton.OnTouchUp();
            if (HelpButton.IsPointInBounds(arg1) && HelpButton.IsPressed)
                HelpButton.OnTouchUp();
            /*if (OptionsButton.IsPointInBounds(arg1) && OptionsButton.IsPressed)
                OptionsButton.OnTouchUp();*/
        }
        

        private void StartGameButton_Clicked(Button sender)
        {
            World world = new World(new GameSettings(BuildingDescriptionDatabase, ColorCollectionFile, LanguageSettings));
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
            /*if (OptionsButton.IsPointInBounds(arg1))
            {
                OptionsButton.IsPressed = true;
            }*/
            return true;
        }
    }
}