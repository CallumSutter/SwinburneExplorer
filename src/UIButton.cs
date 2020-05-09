using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public class UIButton : UIObject
	{
		private const int BTN_TEXT_X_OFFSET = 20;
		private const int BTN_TEXT_Y_OFFSET = 10;
		private string _label;

		public UIButton(Rectangle objectMask, string label) : base(objectMask) {
			_label = label;
			ObjectImage = GameResources.GetImage("btnBase");
		}

		public override void Draw() {
			base.Draw();
			GameWindow.DrawText(_label, Color.DarkRed, GameResources.GetFont("gameFont"), 24, Position.X + BTN_TEXT_X_OFFSET, Position.Y + BTN_TEXT_Y_OFFSET);
		}
	}
}
