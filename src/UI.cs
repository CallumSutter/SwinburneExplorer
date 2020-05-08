using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public enum ArrowDir {
		Up,
		Down,
		Left,
		Right
	}

	public class UI : IDraw
	{
		private static int ARROW_SIZE = 72;

		private static double ARROW_Y = GameController.WINDOW_HEIGHT * (3.3 / 5.0);
		private static double ARROW_X = GameController.WINDOW_WIDTH / 2.0 - ARROW_SIZE / 2.0;
		private static double ARROW_X_OFFSET = 120;
		private static double ARROW_Y_OFFSET = ARROW_SIZE / 2 + 40;

		private ArrowButton[] _arrows;

		public UI()
		{
			InitialiseArrows();
		}

		public void Draw()
		{
			//draws players location
			GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, -1613, -760, SplashKit.OptionScaleBmp(0.3, 0.3));

			//draws current location
			GameController.gameWindow.DrawRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			GameController.gameWindow.FillRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
			string _location = "Current Location: " + GameController._player.Location.Name;
			GameController.gameWindow.DrawText(_location, Color.White, GameController.WINDOW_WIDTH / 2 - 120, 20);

			//draw map
			GameController.theMap.Draw();

			//draw objectives

			//draws arrows
			DrawDirectionArrows();

			//target 60 fps
			GameController.gameWindow.Refresh(60);
		}

		private void DrawDirectionArrows()
		{
			for(uint i = 0; i < 4; i++)
			{
				if (GameController._player.Location.Paths[i] != null)
				{
					_arrows[i].Draw();
				}
			}
		}

		private void InitialiseArrows()
		{
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

		public ArrowDir? CheckMouseInArrow()
		{
			for(uint i = 0; i < 3; i++)
			{
				if (_arrows[i].IsHovering(SplashKit.MousePosition()))
				{
					return (ArrowDir)i;
				}
			}

			return null;
		}

		public ArrowButton[] Arrows
		{
			get
			{
				return _arrows;
			}
		}
	}
}
