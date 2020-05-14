using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer {

	public enum ArrowDir {
		Up,
		Down,
		Left,
		Right
	}

	public class UI : IDraw {
		private static int ARROW_SIZE = 72;

		private static double ARROW_Y = GameController.WINDOW_HEIGHT * (3.3 / 5.0);
		private static double ARROW_X = GameController.WINDOW_WIDTH / 2.0 - ARROW_SIZE / 2.0;
		private static double ARROW_X_OFFSET = 120;
		private static double ARROW_Y_OFFSET = ARROW_SIZE / 2 + 40;

		private const double LOC_X_SCALING = (double)GameController.WINDOW_WIDTH / 1300;
		private const double LOC_Y_SCALING = (double)GameController.WINDOW_HEIGHT / 614;

		private const int LOC_IMAGE_X_OFFSET = GameController.WINDOW_WIDTH - 1300;
		private const int LOC_IMAGE_Y_OFFSET = GameController.WINDOW_HEIGHT - 614;

		private ArrowButton[] _arrows;
		private UIButton _enterBtn;
		private UIButton _exitBtn;
		private UIButton _enterBtn2;
		private UIButton _exitBtn2;
		private UIObject _infoBtn;
		private UIObject _scroll;

		public UI()	{
			InitialiseArrows();
			InitialiseButtons();
			InitialiseInfoButton();
			InitialiseScroll();
		}

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
			DrawInfoButton();

			//draw objectives
			DrawObjectives();

			//target 60 fps
			GameController.gameWindow.Refresh(60);
		}

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

		private void DrawMinimap() {
			GameController.theMap.Draw();
		}

		private void DrawPlayerLocation() {
			//Draws Players location
			if (GameController._currentState == GameState.Travelling.ToString() || GameController._currentState == GameState.InBuilding.ToString()) {
				GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));
			}
			else {
				GameController.gameWindow.DrawBitmap(GameResources.GetImage("classroom"), LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));
			}
		}

		private void DrawLocationInformation() {
			//Draws location name
			GameController.gameWindow.DrawRectangle(Color.DarkRed, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			GameController.gameWindow.FillRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
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

			GameController.gameWindow.DrawText(_location, Color.DarkRed, GameController.WINDOW_WIDTH / 2 - 120, 20);
		}

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

		private void DrawObjectives() {
			_scroll.Draw();
			GameController.gameWindow.DrawText("Objective", Color.DarkRed, GameResources.GetFont("gameFont"), 40, 1010, 40);
			GameController.gameWindow.DrawText(GameController._player.CurrentObjective.Description, Color.DarkRed, 970, 90);
			GameController.gameWindow.DrawText(GameController._player.CurrentObjective.Description2, Color.DarkRed, 970, 100);
		}

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

		private void DrawInfoButton() {
			_infoBtn.Draw();
		}

		private void InitialiseScroll() {
			_scroll = new UIObject(GameResources.GetImage("scroll"),
				CreateMask(GameController.WINDOW_WIDTH - GameResources.GetImage("scroll").Width, 0, GameResources.GetImage("scroll").Width, GameResources.GetImage("scroll").Height));
		}

		private void InitialiseArrows()	{
			_arrows = new ArrowButton[4];

			Rectangle arrowMask = new Rectangle();
			arrowMask.Height = GameResources.ARROW_SIZE;
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

		private void InitialiseButtons() {
			Bitmap btnImg = GameResources.GetImage("btnBase");
			Rectangle btnMask = CreateMask(ARROW_X - 8, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);
			Rectangle btnMask2 = CreateMask(ARROW_X - 8 - 47, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);
			Rectangle btnMask3 = CreateMask(ARROW_X - 8 + 47, ARROW_Y + btnImg.Height / 4, btnImg.Width, btnImg.Height);

			_enterBtn = new UIButton(btnMask, "Enter");

			_exitBtn = new UIButton(btnMask, "Exit");
			_exitBtn.Visible = false;

			_enterBtn2 = new UIButton(btnMask2, "Enter");
			_enterBtn2.Visible = false;

			_exitBtn2 = new UIButton(btnMask3, "Exit");
			_exitBtn2.Visible = false;
		}

		private void InitialiseInfoButton()	{
			_infoBtn = new UIObject(GameResources.GetImage("infoBtn"), 
				CreateMask(2*Map.MAP_ICON_X_OFFSET + GameResources.GetImage("sMap").Width, Map.MAP_ICON_Y_OFFSET, 50, 50));
		}

		private Rectangle CreateMask(double x, double y, double width, double height) {
			Rectangle mask = new Rectangle();
			mask.X = x;
			mask.Y = y;
			mask.Width = width;
			mask.Height = height;

			return mask;
		}

		public ArrowDir? CheckMouseInArrow() {
			for (uint i = 0; i < 4; i++) {
				if (_arrows[i].IsHovering(SplashKit.MousePosition())) {
					return (ArrowDir)i;
				}
			}
			return null;
		}

		public bool CheckMouseInEnterButton() {
			return (_enterBtn.IsHovering(SplashKit.MousePosition()));
		}

		public bool CheckMouseInEnter2Button() {
			return (_enterBtn2.IsHovering(SplashKit.MousePosition()));
		}

		public bool CheckMouseInExit2Button() {
			return (_exitBtn2.IsHovering(SplashKit.MousePosition()));
		}

		public bool CheckMouseInInfoButton() {
			return (_infoBtn.IsHovering(SplashKit.MousePosition()));
		}

		public ArrowButton[] Arrows	{
			get	{
				return _arrows;
			}
		}

		public UIButton EnterButton	{
			get	{
				return _enterBtn;
			}
		}

		public UIButton ExitButton {
			get {
				return _exitBtn;
			}
		}

		public UIButton EnterButton2 {
			get {
				return _enterBtn2;
			}
		}

		public UIButton ExitButton2 {
			get {
				return _exitBtn2;
			}
		}
	}
}
