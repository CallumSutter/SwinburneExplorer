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
			DrawButtons();
			DrawInfoButton();

			//draw objectives
			DrawObjectives();

			//target 60 fps
			GameController.gameWindow.Refresh(60);
		}

		private void DrawDirectionArrows() {
			for(uint i = 0; i < 4; i++)	{
				if (GameController._player.Location.Paths[i] != null) {
					_arrows[i].Draw();
				}
			}
		}

		private void DrawMinimap() {
			GameController.theMap.Draw();
		}

		private void DrawPlayerLocation() {
			//Draws Players location
			GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));
		}

		private void DrawLocationInformation() {
			//Draws location name
			GameController.gameWindow.DrawRectangle(Color.DarkRed, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			GameController.gameWindow.FillRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			string _location = "Current Location: " + GameController._player.Location.Name;
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

		private void DrawButtons() {
			_enterBtn.Draw();
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
			//btnMask.Width = btnImg.Width;
			//btnMask.Height = btnImg.Height;
			//btnMask.X = ARROW_X - 8;
			//btnMask.Y = ARROW_Y + btnImg.Height / 4;

			_enterBtn = new UIButton(btnMask, "Enter");
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
	}
}
