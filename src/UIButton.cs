using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
	/// <summary>
	/// Class is used to create buttons to be drawn onto the UI
	/// </summary>
	public class UIButton : UIObject
	{
		private int BTN_TEXT_X_OFFSET;
		private int BTN_TEXT_Y_OFFSET;
		private string _label;

		/// <summary>
		/// Intialiser for the UIButton Class
		/// </summary>
		/// <param name="objectMask"></param>
		/// <param name="label"></param>
		/// <param name="offsetX"></param>
		/// <param name="offsetY"></param>
		public UIButton(Rectangle objectMask, string label, int offsetX, int offsetY) : base(objectMask) {
			_label = label;
			ObjectImage = GameResources.GetImage("btnBase");
			BTN_TEXT_X_OFFSET = offsetX;
			BTN_TEXT_Y_OFFSET = offsetY;
		}

		/// <summary>
		/// Draws object to game screen
		/// </summary>
		public override void Draw() {
			base.Draw();
			GameWindow.DrawText(_label, Color.DarkRed, GameResources.GetFont("gameFont"), 24, Position.X + BTN_TEXT_X_OFFSET, Position.Y + BTN_TEXT_Y_OFFSET);
		}
	}
}
