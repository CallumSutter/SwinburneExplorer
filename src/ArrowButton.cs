using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
	/// <summary>
	/// Specialised UIObject for arrows
	/// </summary>
	public class ArrowButton : UIObject
	{
		private double _rotation;

		public ArrowButton(Rectangle objectMask, double rotation) : base(objectMask) {
			ObjectImage = GameResources.GetImage("sArrow");
			_rotation = rotation;
		}

		public override void Draw()
		{
			GameWindow.DrawBitmap(ObjectImage, Position.X, Position.Y, SplashKit.OptionRotateBmp(_rotation));
		}
	}
}
