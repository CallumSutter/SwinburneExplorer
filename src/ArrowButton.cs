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

		/// <summary>
		/// Constructor for Arrow button UI object
		/// </summary>
		/// <param name="objectMask">mask for button</param>
		/// <param name="rotation">image rotation</param>
		public ArrowButton(Rectangle objectMask, double rotation) : base(objectMask) {
			ObjectImage = GameResources.GetImage("sArrow");
			_rotation = rotation;
		}

		/// <summary>
		/// Draw button onto window
		/// </summary>
		public override void Draw()
		{
			GameWindow.DrawBitmap(ObjectImage, Position.X, Position.Y, SplashKit.OptionRotateBmp(_rotation));
		}
	}
}
