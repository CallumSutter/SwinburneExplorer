using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer {
    public class MainMenu : IDraw {
        private static Bitmap _background;
        private static UIButton _playButton;
        private static UIButton _exitButton;
        private static UIButton _campusButton;
        private static UIButton _carParkButton;
        private static UIButton _trainButton;
        private static Font _mainFont;
        private static bool _playPressed;

        private const int X_LOC_PLAY_BUTTON = 600;
        private const int Y_LOC_PLAY_BUTTON = 400;
        private const int X_LOC_EXIT_BUTTON = 900;
        private const int Y_LOC_EXIT_BUTTON = 400;
        private const int X_LOC_CARPARK_BUTTON = 750;
        private const int Y_LOC_CARPARK_BUTTON = 400;
        private const int X_LOC_TEXT = 500;
        private const int Y_LOC_TEXT = 70;

        public MainMenu() {
            _playPressed = false;
            InitialiseBackground();
            InitialiseButtons();
            InitialiseFont();
        }

        public bool PlayPressed {
            get {
                return _playPressed;
            }

            set {
                _playPressed = value;
            }
        }

        public void InitialiseFont() {
            _mainFont = GameResources.GetFont("gameFont");
        }

        public void InitialiseBackground() {
            _background = new Bitmap("background", "swinburne.jpg");
        }

        public void InitialiseButtons() {
            Rectangle playMask = CreateMask(X_LOC_PLAY_BUTTON, Y_LOC_PLAY_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);        
            Rectangle exitMask = CreateMask(X_LOC_EXIT_BUTTON, Y_LOC_EXIT_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);
            Rectangle trainMask = CreateMask(X_LOC_PLAY_BUTTON, Y_LOC_PLAY_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);
            Rectangle campusMask = CreateMask(X_LOC_EXIT_BUTTON, Y_LOC_EXIT_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);
            Rectangle carParkMask = CreateMask(X_LOC_CARPARK_BUTTON, Y_LOC_CARPARK_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);
            _playButton = new UIButton(playMask, "Play", 23, 10);
            _exitButton = new UIButton(exitMask, "Exit", 27, 10);
            _trainButton = new UIButton(trainMask, "Train", 20, 10);
            _campusButton = new UIButton(campusMask, "Campus", 10, 10);
            _carParkButton = new UIButton(carParkMask, "Car Park", 5, 10);
        }

        private Rectangle CreateMask(double x, double y, double width, double height) {
            Rectangle mask = new Rectangle();
            mask.X = x;
            mask.Y = y;
            mask.Width = width;
            mask.Height = height;

            return mask;
        }

        public bool CheckMouseInTrainButton() {
            return _trainButton.IsHovering(SplashKit.MousePosition());
        }

        public bool CheckMouseInCampusButton() {
            return _campusButton.IsHovering(SplashKit.MousePosition());
        }

        public bool CheckMouseInPlayButton() {
            return _playButton.IsHovering(SplashKit.MousePosition());
        }

        public bool CheckMouseInExitButton() {
            return _exitButton.IsHovering(SplashKit.MousePosition());
        }

        public bool CheckMouseInCarParkButton() {
            return _carParkButton.IsHovering(SplashKit.MousePosition());
        }

        public void Draw() {
            GameController.gameWindow.DrawBitmap(_background, 0, 0);
            GameController.gameWindow.DrawText("Swinburne Explorer", Color.DarkRed, _mainFont, 70, X_LOC_TEXT, Y_LOC_TEXT);
            if (_playPressed) {
                GameController.gameWindow.DrawText("Starting Location:", Color.DarkRed, _mainFont, 32, X_LOC_TEXT + 170, Y_LOC_TEXT + 280);
                _trainButton.Draw();
                _campusButton.Draw();
                _carParkButton.Draw();
            }
            else {
                _playButton.Draw();
                _exitButton.Draw();
            }        
        }
        
    }
}
