using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer {
	/// <summary>
	/// enum for arrow directions
	/// </summary>
	public enum ArrowDir {
		/// <summary>
		/// up direction
		/// </summary>
		Up,
		/// <summary>
		/// down direction
		/// </summary>
		Down,
		/// <summary>
		/// left direction
		/// </summary>
		Left,
		/// <summary>
		/// right direction
		/// </summary>
		Right
	}

	/// <summary>
	/// UI class used to draw to game window when in the main game
	/// </summary>
	public class UI : IDraw {
		private const double HALF_WIN_WIDTH = GameController.WINDOW_WIDTH / 2.0;
		private const double HALF_WIN_HEIGHT = GameController.WINDOW_HEIGHT / 2.0;

		// Arrow constants
		private static int ARROW_SIZE = 72;

		private static double ARROW_Y = GameController.WINDOW_HEIGHT * (3.3 / 5.0);
		private static double ARROW_X = HALF_WIN_WIDTH - ARROW_SIZE / 2.0;
		private static double ARROW_X_OFFSET = 120;
		private static double ARROW_Y_OFFSET = ARROW_SIZE / 2 + 40;

		// location image scaling
		private const double LOC_X_SCALING = (double)GameController.WINDOW_WIDTH / 1300;
		private const double LOC_Y_SCALING = (double)GameController.WINDOW_HEIGHT / 614;

		// location image offset
		private const int LOC_IMAGE_X_OFFSET = GameController.WINDOW_WIDTH - 1300;
		private const int LOC_IMAGE_Y_OFFSET = GameController.WINDOW_HEIGHT - 614;

		private ArrowButton[] _arrows;
		private UIButton _enterBtn;
		private UIButton _exitBtn;
		private UIButton _enterBtn2;
		private UIButton _exitBtn2;
		private UIButton _quitBtn;
		private UIObject _infoBtn;
		private UIObject _scroll;

		// info UI
		private Font _infoFont;

		/// <summary>
		/// Constructor for UI class object
		/// Initialises properties
		/// </summary>
		public UI()	{
			InitialiseArrows();
			InitialiseButtons();
			InitialiseInfoResources();
			InitialiseScroll();
		}

		/// <summary>
		/// Draw UI onto window
		/// </summary>
		public void Draw() {
			//Draw the player location and extra information
			DrawPlayerLocation();

			//Draws information about the player's location
			DrawLocationInformation();

			//draw map
			DrawMinimap();

			//draw objectives

			//draws arrows
			DrawDirectionArrows();

			//draw buttons		
			DrawEnterButton();
			DrawExitButton();
			DrawQuitButton();
			DrawInfoButton();

			//draw objectives
			DrawObjectives();

			//target 60 fps
			GameController.gameWindow.Refresh(60);
		}

		/// <summary>
		/// Draw directional arrows onto window
		/// </summary>
		private void DrawDirectionArrows() {
			if (GameController._currentState == GameState.InBuilding.ToString()) {
				if (GameController._player.ReturnBuildingIfExists().CurrentFloor == 1) {
					if (GameController._player.ReturnBuildingIfExists().CurrentFloor != GameController._player.ReturnBuildingIfExists().FloorCount) {
						//forward arrow
						_arrows[0].Draw();
					}
				} 
				else {
					if (GameController._player.ReturnBuildingIfExists().CurrentFloor != GameController._player.ReturnBuildingIfExists().FloorCount) {
						//forward arrow
						_arrows[0].Draw();
						//backward arrow
						_arrows[1].Draw();
					} 		
					else {
						//backward arrow
						_arrows[1].Draw();
					}
				}
			}
			else if (GameController._currentState == GameState.Travelling.ToString()) {
				for (uint i = 0; i < 4; i++) {
					if (GameController._player.Location.Paths[i] != null) {
						_arrows[i].Draw();
					}
				}
			}
		}

		/// <summary>
		/// Draws minimap to screen
		/// </summary>
		private void DrawMinimap() {
			GameController.theMap.Draw();
		}

		/// <summary>
		/// Draws player location to screen
		/// </summary>
		private void DrawPlayerLocation() {
			//Draws Players location
			if (GameController._currentState == GameState.Travelling.ToString() || GameController._currentState == GameState.InBuilding.ToString()) {
				GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));
			}
			else {
				GameController.gameWindow.DrawBitmap(GameResources.GetImage("classroom"), LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));
			}
		}

		/// <summary>
		/// Draw location information to screen
		/// </summary>
		private void DrawLocationInformation() {
			//Draws location name
			GameController.gameWindow.DrawRectangle(Color.DarkRed, HALF_WIN_WIDTH - 150, 0, 300, 50);
			GameController.gameWindow.FillRectangle(Color.Black, HALF_WIN_WIDTH - 150, 0, 300, 50);
			string _location = "";

			if (GameController._currentState == GameState.InBuilding.ToString()) {
				_location = "Current Location: " + GameController._player.Location.Name + " F" + GameController._player.ReturnBuildingIfExists().CurrentFloor;
			}
			else if (GameController._currentState == GameState.InClassroom.ToString()) {
				_location = "Current Location: " + GameController._player.ReturnBuildingIfExists().CurrentClassroom.RoomId;			
			}
			else {
				_location = "Current Location: " + GameController._player.Location.Name;
			}

			GameController.gameWindow.DrawText(_location, Color.DarkRed, HALF_WIN_WIDTH - 120, 20);
		}

		/// <summary>
		/// Draw objective complete to screen
		/// </summary>
		public void DrawObjectiveComplete() {
			DrawPlayerLocation();
			GameController.gameWindow.FillRectangle(Color.Black, 310, 100, 600, 300);
			GameController.gameWindow.DrawRectangle(Color.DarkRed, 310, 100, 600, 300);
			GameController.gameWindow.DrawText("Objective Complete", Color.DarkRed, GameResources.GetFont("gameFont"), 40, 450, 130);
			GameController.gameWindow.DrawText("Press the Right Mouse Button or Spacebar to continue", Color.DarkRed, 400, 300);
			GameController.gameWindow.Refresh();
			SplashKit.PlaySoundEffect(GameResources.GetSound("objectiveComplete"));
			while (!SplashKit.MouseClicked(MouseButton.RightButton) && (!SplashKit.KeyTyped(KeyCode.SpaceKey))) {
				SplashKit.ProcessEvents();
			}
		}

		/// <summary>
		/// Draw objectives to screen
		/// </summary>
		private void DrawObjectives() {
			_scroll.Draw();
			GameController.gameWindow.DrawText("Objective", Color.DarkRed, GameResources.GetFont("gameFont"), 40, 1010, 40);
			GameController.gameWindow.DrawText(GameController._player.CurrentObjective.Description, Color.DarkRed, 970, 90);
			GameController.gameWindow.DrawText(GameController._player.CurrentObjective.Description2, Color.DarkRed, 970, 100);
		}

		/// <summary>
		/// Draw enter button to screen
		/// </summary>
		private void DrawEnterButton() {
			//only draw enter button if building or classroom is avaliable to enter
			if (GameController._currentState == GameState.InBuilding.ToString()) {
				if (GameController._player.ReturnBuildingIfExists().CurrentFloor != 1) {
					_enterBtn.Draw();
					_enterBtn.Visible = true;

					_enterBtn2.Visible = false;
				}

				else {
					_enterBtn.Visible = false;

					_enterBtn2.Draw();
					_enterBtn2.Visible = true;
				}
			}
			else if (GameController._player.Location.EnterBuilding != null) {
				_enterBtn.Draw();
				_enterBtn.Visible = true;
				_enterBtn2.Visible = false;
			}
			else {
				_enterBtn.Visible = false;
				_enterBtn2.Visible = false;
			}
		}

		/// <summary>
		/// Draw exit button to screen
		/// </summary>
		private void DrawExitButton() {
			//only draw exit button 1 if player is not on floor 1 of building or is in a classroom
			//only draw exit button 2 is player in on fl0or 1 of building
			if (GameController._currentState == GameState.InBuilding.ToString()) {
				if (GameController._player.ReturnBuildingIfExists().CurrentFloor == 1) {
					_exitBtn.Visible = false;

					_exitBtn2.Draw();
					_exitBtn2.Visible = true;					
				}
				else {
					_exitBtn.Visible = false;
					_exitBtn2.Visible = false;
				}
			}
			else if (GameController._currentState == GameState.InClassroom.ToString()) {
				_exitBtn.Draw();
				_exitBtn.Visible = true;
				_exitBtn2.Visible = false;
			}
			else {
				_exitBtn.Visible = false;
				_exitBtn2.Visible = false;
			}
		}

		/// <summary>
		/// Draw quit button onto UI
		/// </summary>
		private void DrawQuitButton() {
			_quitBtn.Draw();
		}

		/// <summary>
		/// Draw information button onto UI
		/// </summary>
		private void DrawInfoButton() {
			if (GameController.Player.Location.Building != null |
				GameController.Player.Location is Building)
			{
				_infoBtn.Draw();
			}
		}

		/// <summary>
		/// Draw Building info to screen
		/// </summary>
		public void DrawBuildingInfo()
		{
			Bitmap infoBox = GameResources.GetImage("infoBox");
			GameController.gameWindow.DrawBitmap(infoBox, 
				HALF_WIN_WIDTH - infoBox.Width / 2, HALF_WIN_HEIGHT - infoBox.Height / 2);

           

			DrawInfoText(GameController.Player.Location.Name + " Info", 20, HALF_WIN_WIDTH - infoBox.Width / 2 + 25, 
				HALF_WIN_HEIGHT - infoBox.Height / 2 + 20);

            // 41 characters p/l with current settings.
            string text = GameController.Player.Location.GetInfo;
            if (text == null)
            {
                return;
            }


            string[] textSplit = text.Split(' ');
			string toDraw = "";
			int lines = 0;

			for(int i = 0; i < textSplit.Length; i++)
			{
				if ((toDraw + " " + textSplit[i]).Length <= 41) {
					toDraw += " " + textSplit[i];
				}
				else {
					DrawInfoText(toDraw, 15, HALF_WIN_WIDTH - infoBox.Width / 2 + 25, 
						HALF_WIN_HEIGHT - infoBox.Height / 2 + 43 + lines++ * 15);

					toDraw = "";
				}
			}

			// draw if not already drawn
			if (toDraw != "") {
				DrawInfoText(toDraw, 15, HALF_WIN_WIDTH - infoBox.Width / 2 + 25,
					HALF_WIN_HEIGHT - infoBox.Height / 2 + 43 + lines++ * 15);
			}

			DrawInfoText("Right click or press space to exit", 10, HALF_WIN_WIDTH - infoBox.Width / 2 + 25,
				GameController.WINDOW_HEIGHT - 100);

			SplashKit.RefreshScreen();

			// wait for exit
			while (!SplashKit.MouseClicked(MouseButton.RightButton) && (!SplashKit.KeyTyped(KeyCode.SpaceKey))) {
				SplashKit.ProcessEvents();
			}
		}

		/// <summary>
		/// Draw info text to screen
		/// </summary>
		/// <param name="text"></param>
		/// <param name="size"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		private void DrawInfoText(string text, int size, double x, double y)
		{
			GameController.gameWindow.DrawText(text, Color.Black, _infoFont, size, x, y);
		}

		/// <summary>
		/// Initialse scroll object
		/// </summary>
		private void InitialiseScroll() {
			_scroll = new UIObject(GameResources.GetImage("scroll"),
				CreateMask(GameController.WINDOW_WIDTH - GameResources.GetImage("scroll").Width, 0, GameResources.GetImage("scroll").Width, GameResources.GetImage("scroll").Height));
		}

		/// <summary>
		/// Initialise arrow objects
		/// </summary>
		private void InitialiseArrows()	{
			_arrows = new ArrowButton[4];

			Rectangle arrowMask = new Rectangle();
			arrowMask.Height = ARROW_SIZE;
			arrowMask.Width = ARROW_SIZE;

			//position of up arrow
			arrowMask.X = ARROW_X;
			arrowMask.Y = ARROW_Y - ARROW_Y_OFFSET;
			_arrows[0] = new ArrowButton(arrowMask, 270);

			//position of down arrow
			arrowMask.X = ARROW_X;
			arrowMask.Y = ARROW_Y + ARROW_Y_OFFSET;
			_arrows[1] = new ArrowButton(arrowMask, 90);

			//position of left arrow
			arrowMask.X = ARROW_X - ARROW_X_OFFSET;
			arrowMask.Y = ARROW_Y;
			_arrows[2] = new ArrowButton(arrowMask, 180);

			//position of right arrow
			arrowMask.X = ARROW_X + ARROW_X_OFFSET;
			arrowMask.Y = ARROW_Y;
			_arrows[3] = new ArrowButton(arrowMask, 0);
		}

		/// <summary>
		/// Initialse button objects
		/// </summary>
		private void InitialiseButtons() {
			Bitmap btnImg = GameResources.GetImage("btnBase");
			Rectangle btnMask = CreateMask(ARROW_X - 8, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);
			Rectangle btnMask2 = CreateMask(ARROW_X - 8 - 47, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);
			Rectangle btnMask3 = CreateMask(ARROW_X - 8 + 47, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);
			Rectangle btnQuit = CreateMask(5, GameController.WINDOW_HEIGHT - 50, btnImg.Width, btnImg.Height);

			_enterBtn = new UIButton(btnMask, "Enter", 20, 10);

			_exitBtn = new UIButton(btnMask, "Exit", 27, 10);
			_exitBtn.Visible = false;

			_enterBtn2 = new UIButton(btnMask2, "Enter", 20, 10);
			_enterBtn2.Visible = false;

			_exitBtn2 = new UIButton(btnMask3, "Exit", 27, 10);
			_exitBtn2.Visible = false;

			_quitBtn = new UIButton(btnQuit, "Quit", 27, 10);
		}

		/// <summary>
		/// Initialise building information resources
		/// - button
		/// - font
		/// </summary>
		private void InitialiseInfoResources()	{
			_infoFont = GameResources.GetFont("infoFont");

			Bitmap infoBtn = GameResources.GetImage("infoBtn");
			_infoBtn = new UIObject(infoBtn, 
				CreateMask(2*Map.MAP_ICON_X_OFFSET + GameResources.GetImage("sMap").Width, Map.MAP_ICON_Y_OFFSET, 50, 50));
		}

		/// <summary>
		/// Generate an object mask
		/// </summary>
		/// <param name="x">x coord</param>
		/// <param name="y">y coord</param>
		/// <param name="width">width of mask</param>
		/// <param name="height">height of mask</param>
		/// <returns>object mask</returns>
		private Rectangle CreateMask(double x, double y, double width, double height) {
			Rectangle mask = new Rectangle();
			mask.X = x;
			mask.Y = y;
			mask.Width = width;
			mask.Height = height;

			return mask;
		}

		/// <summary>
		/// Checks if the mouse is on an arrow
		/// </summary>
		/// <returns>true or false</returns>
		public ArrowDir? CheckMouseInArrow() {
			for (uint i = 0; i < 4; i++) {
				if (_arrows[i].IsHovering(SplashKit.MousePosition())) {
					return (ArrowDir)i;
				}
			}
			return null;
		}

		/// <summary>
		/// Checks if the mouse is on the enter arrow
		/// </summary>
		/// <returns>true or false</returns>
		public bool CheckMouseInEnterButton() {
			return (_enterBtn.IsHovering(SplashKit.MousePosition()));
		}

		/// <summary>
		/// Checks if the mouse is on the enter2 button
		/// </summary>
		/// <returns>true or false</returns>
		public bool CheckMouseInEnter2Button() {
			return (_enterBtn2.IsHovering(SplashKit.MousePosition()));
		}

		/// <summary>
		/// Checks if the mouse is on the exit2 button
		/// </summary>
		/// <returns>true or false</returns>
		public bool CheckMouseInExit2Button() {
			return (_exitBtn2.IsHovering(SplashKit.MousePosition()));
		}

		/// <summary>
		/// Checks if the mouse is on the info button
		/// </summary>
		/// <returns>true or false</returns>
		public bool CheckMouseInInfoButton() {
			return (_infoBtn.IsHovering(SplashKit.MousePosition()));
		}

		/// <summary>
		/// Checks if the mouse is on the quit button
		/// </summary>
		/// <returns>true or false</returns>
		public bool CheckMouseInQuitButton() {
			return (_quitBtn.IsHovering(SplashKit.MousePosition()));
		}

		/// <summary>
		/// public getter for Arrows
		/// </summary>
		public ArrowButton[] Arrows	{
			get	{
				return _arrows;
			}
		}

		/// <summary>
		/// public getter for EnterButton
		/// </summary>
		public UIButton EnterButton	{
			get	{
				return _enterBtn;
			}
		}

		/// <summary>
		/// public getter for ExitButton
		/// </summary>
		public UIButton ExitButton {
			get {
				return _exitBtn;
			}
		}

		/// <summary>
		/// public getter for EnterButton2
		/// </summary>
		public UIButton EnterButton2 {
			get {
				return _enterBtn2;
			}
		}

		/// <summary>
		/// public getter for ExitButton2
		/// </summary>
		public UIButton ExitButton2 {
			get {
				return _exitBtn2;
			}
		}
	}
}
