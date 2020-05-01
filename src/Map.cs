﻿using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer {
	public class Map : IDraw {
		public const int MAP_OFFSET_X = 50;
		public const int MAP_OFFSET_Y = 5;

		public const int SCROLL_OFFSET = 15;

		private Bitmap _mapIco;
		private Bitmap _mapImg;

		private Rectangle _mapMask;
		private bool _inMap = false;

		private Font _mapFont;
		private Color _textColor = Color.Black;
		private int _textSize = 12;

		public Map() {
			GetImages();

			_mapMask = new Rectangle();
			_mapMask.Height = _mapIco.Height;
			_mapMask.Width = _mapIco.Width;
			_mapMask.X = 10;
			_mapMask.Y = 10;
		}

		public void Draw() {
			if (!_inMap) {
				DrawMapIcon();
			}
			else {
				DrawMap();
			}
		}

		/// <summary>
		/// Draw map
		/// </summary>
		private void DrawMap() {
			GameController.gameWindow.Clear(Color.White);

			GameController.gameWindow.DrawBitmap(_mapImg, MAP_OFFSET_X, MAP_OFFSET_Y);
			GameController.gameWindow.DrawText("Click on screen to go back to game screen", _textColor, GameController.WINDOW_WIDTH / 2, GameController.WINDOW_HEIGHT - 45);
			GameController.gameWindow.DrawText("Press M or Esc to toggle map", _textColor, GameController.WINDOW_WIDTH / 2, GameController.WINDOW_HEIGHT - 30);
		}

		/// <summary>
		/// Draw map icon
		/// </summary>
		private void DrawMapIcon() {
			GameController.gameWindow.DrawBitmap(_mapIco, 10, 10);
		}

		/// <summary>
		/// Check for map toggle
		/// </summary>
		/// <returns>whether to map should be displayed</returns>
		public bool CheckMapClicked() {
			//mask to check area for mouse click

			//check left click
			if (SplashKit.MouseClicked(MouseButton.LeftButton)) {
				//check click on map
				if (!_inMap) {
					_inMap = SplashKit.PointInRectangle(SplashKit.MousePosition(), _mapMask);
				}
				else {
					_inMap = false;
				}
			}

			//check 'M' or 'Esc'
			if ((SplashKit.KeyTyped(KeyCode.MKey)) |
				(SplashKit.KeyTyped(KeyCode.EscapeKey))) {
				_inMap = !_inMap; //toggle map
			}

			return _inMap;
		}

		/// <summary>
		/// Retrieve required resources
		/// </summary>
		private void GetImages() {
			_mapIco = GameResources.GetImage("sMap");
			_mapImg = GameResources.GetImage("SwinMap");
			_mapFont = GameResources.GetFont("arial");
		}
	}
}
