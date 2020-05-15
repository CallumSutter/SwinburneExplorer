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

		/// <summary>
		/// Constructor for main menu
		/// </summary>
        public MainMenu() {
            _playPressed = false;
            InitialiseBackground();
            InitialiseButtons();
            InitialiseFont();
        }

		/// <summary>
		/// Get/Set for if play button was pressed
		/// </summary>
        public bool PlayPressed {
            get {
                return _playPressed;
            }

            set {
                _playPressed = value;
            }
        }

		/// <summary>
		/// Initialise font for main menu
		/// </summary>
        public void InitialiseFont() {
            _mainFont = GameResources.GetFont("gameFont");
        }

		/// <summary>
		/// Initialise background image
		/// </summary>
        public void InitialiseBackground() {
			_background = GameResources.GetImage("background");
        }

		/// <summary>
		/// Initialise masks for buttons and initialise button
		/// </summary>
        public void InitialiseButtons() {
			Bitmap btnBase = GameResources.GetImage("btnBase");

			Rectangle playMask = CreateMask(X_LOC_PLAY_BUTTON, Y_LOC_PLAY_BUTTON, btnBase.Width, btnBase.Height);
			Rectangle exitMask = CreateMask(X_LOC_EXIT_BUTTON, Y_LOC_EXIT_BUTTON, btnBase.Width, btnBase.Height);

			_playButton = new UIButton(playMask, "Play");
            _exitButton = new UIButton(exitMask, "Exit");
            _trainButton = new UIButton(playMask, "Train");
            _campusButton = new UIButton(exitMask, "Campus");
        }

		/// <summary>
		/// Create mask given parameters
		/// </summary>
		/// <param name="x">x coordinate</param>
		/// <param name="y">y coordinate</param>
		/// <param name="width">width of mask</param>
		/// <param name="height">height of mask</param>
		/// <returns>rectangle mask</returns>
        private Rectangle CreateMask(double x, double y, double width, double height) {
            Rectangle mask = new Rectangle();
            mask.X = x;
            mask.Y = y;
            mask.Width = width;
            mask.Height = height;

            return mask;
        }

		/// <summary>
		/// Check if mouse is in 'Train' button
		/// </summary>
		/// <returns>if mouse is in button</returns>
        public bool CheckMouseInTrainButton() {
            return _trainButton.IsHovering(SplashKit.MousePosition());
        }

		/// <summary>
		/// Check if mouse is in 'Campus' button
		/// </summary>
		/// <returns>if mouse is in button</returns>
		public bool CheckMouseInCampusButton() {
            return _campusButton.IsHovering(SplashKit.MousePosition());
        }

		/// <summary>
		/// Check if mouse is in 'Play' button
		/// </summary>
		/// <returns>if mouse is in button</returns>
		public bool CheckMouseInPlayButton() {
            return _playButton.IsHovering(SplashKit.MousePosition());
        }

		/// <summary>
		/// Check if mouse is in 'Exit' button
		/// </summary>
		/// <returns>if mouse is in button</returns>
		public bool CheckMouseInExitButton() {
            return _exitButton.IsHovering(SplashKit.MousePosition());
        }

		/// <summary>
		/// Draw main menu onto window
		/// </summary>
        public void Draw() {
            GameController.gameWindow.DrawBitmap(_background, 0, 0);
			GameController.gameWindow.DrawText("Swinburne Explorer", Color.White, _mainFont, 70, X_LOC_TEXT-3, Y_LOC_TEXT-3);
			GameController.gameWindow.DrawText("Swinburne Explorer", Color.DarkRed, _mainFont, 70, X_LOC_TEXT, Y_LOC_TEXT);
            
			if (_playPressed) {
				GameController.gameWindow.DrawText("Starting Location:", Color.White, _mainFont, 32, X_LOC_TEXT + 167, Y_LOC_TEXT + 277);
				GameController.gameWindow.DrawText("Starting Location:", Color.White, _mainFont, 32, X_LOC_TEXT + 173, Y_LOC_TEXT + 283);
				GameController.gameWindow.DrawText("Starting Location:", Color.White, _mainFont, 32, X_LOC_TEXT + 173, Y_LOC_TEXT + 277);
				GameController.gameWindow.DrawText("Starting Location:", Color.White, _mainFont, 32, X_LOC_TEXT + 167, Y_LOC_TEXT + 283);
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
