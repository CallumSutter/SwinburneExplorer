﻿using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer {
	/// <summary>
	/// Map class used for drawing the map to the screen
	/// and handling input when map has been clicked
	/// </summary>
	public class Map : IDraw {
		/// <summary>
		/// offset x for large map
		/// </summary>
		public const int MAP_X_OFFSET = GameController.WINDOW_WIDTH / 2 - 350;
		/// <summary>
		/// offset y for large map 
		/// </summary>
		public const int MAP_Y_OFFSET = 0;
		/// <summary>
		/// offset x for map icon
		/// </summary>
		public const int MAP_ICON_X_OFFSET = 10;
		/// <summary>
		/// offet y for map icon
		/// </summary>
		public const int MAP_ICON_Y_OFFSET = 10;
		/// <summary>
		/// Offset for scroll image
		/// </summary>
		public const int SCROLL_OFFSET = 15;

		private Bitmap _mapIco;
		private Bitmap _mapImg;

		private Rectangle _mapMask;
		private bool _fullscreen = false;

		private Font _mapFont;
		private Color _textColor = Color.Black;

		/// <summary>
		/// Initialiser for Map class
		/// </summary>
		public Map() {
			SetMapResources();

			_mapMask = new Rectangle();
			_mapMask.Height = _mapIco.Height;
			_mapMask.Width = _mapIco.Width;
			_mapMask.X = 10;
			_mapMask.Y = 10;
		}

		/// <summary>
		/// Draws map to screen
		/// </summary>
		public void Draw() {
			if (!_fullscreen) {
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

			GameController.gameWindow.DrawBitmap(_mapImg, MAP_X_OFFSET, MAP_Y_OFFSET);
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

			bool _inMap = false;

			//mask to check area for mouse click

			//check left click
			if (SplashKit.MouseClicked(MouseButton.LeftButton)) {
				//check click on map
				if (!_fullscreen) {
					_inMap = SplashKit.PointInRectangle(SplashKit.MousePosition(), _mapMask);
				}
				else {
					_inMap = true;
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
		private void SetMapResources() {
			_mapIco = GameResources.GetImage("sMap");
			_mapImg = GameResources.GetImage("SwinMap");
			_mapFont = GameResources.GetFont("arial");
		}

		/// <summary>
		/// public setter for _fullscreen vlaue
		/// </summary>
		public bool Fullscreen {
			set {
				_fullscreen = value;
			}
		}
	}
}
