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
        private static UIButton _trainButton;
        private static Font _mainFont;
        private static bool _playPressed;

        private const int X_LOC_PLAY_BUTTON = 600;
        private const int Y_LOC_PLAY_BUTTON = 400;
        private const int X_LOC_EXIT_BUTTON = 900;
        private const int Y_LOC_EXIT_BUTTON = 400;
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
            _playButton = new UIButton(playMask, "Play");
            Rectangle exitMask = CreateMask(X_LOC_EXIT_BUTTON, Y_LOC_EXIT_BUTTON, GameResources.GetImage("btnBase").Width, GameResources.GetImage("btnBase").Height);
            _exitButton = new UIButton(exitMask, "Exit");
            _trainButton = new UIButton(playMask, "Train");
            _campusButton = new UIButton(exitMask, "Campus");
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
            return _trainButton.IsHovering(SplashKit.MousePosition());
        }

        public bool CheckMouseInExitButton() {
            return _exitButton.IsHovering(SplashKit.MousePosition());
        }

        public void Draw() {
            GameController.gameWindow.DrawBitmap(_background, 0, 0);
            GameController.gameWindow.DrawText("Swinburne Explorer", Color.DarkRed, _mainFont, 70, X_LOC_TEXT, Y_LOC_TEXT);
            if (_playPressed) {
                GameController.gameWindow.DrawText("Starting Location:", Color.DarkRed, _mainFont, 32, X_LOC_TEXT + 170, Y_LOC_TEXT + 280);
                _trainButton.Draw();
                _campusButton.Draw();
            }
            else {
                _playButton.Draw();
                _exitButton.Draw();
            }        
        }
        
    }
}
