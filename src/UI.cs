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

		public UI()	{
			InitialiseArrows();
			InitialiseButtons();
			InitialiseInfoButton();
		}

		public void Draw() {
			//draws players location
			GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING, LOC_Y_SCALING));

			//draws current location
			GameController.gameWindow.DrawRectangle(Color.DarkRed, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			GameController.gameWindow.FillRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			string _location = "Current Location: " + GameController._player.Location.Name;
			GameController.gameWindow.DrawText(_location, Color.DarkRed, GameController.WINDOW_WIDTH / 2 - 120, 20);

			//draw map
			GameController.theMap.Draw();

			//draw objectives

			//draws arrows
			DrawDirectionArrows();
			DrawButtons();
			DrawInfoButton();

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

		private void DrawButtons() {
			_enterBtn.Draw();
		}

		private void DrawInfoButton() {
			_infoBtn.Draw();
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
