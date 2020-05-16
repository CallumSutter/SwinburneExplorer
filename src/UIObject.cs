using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer {
	/// <summary>
	/// UI Objects are objects that can be placed onto the game screen
	/// </summary>
	public class UIObject : IDraw {
		/// <summary>
		/// static reference to window object will be drawn onto
		/// </summary>
		protected static Window GameWindow = GameController.gameWindow;

		private Rectangle _objectMask;
		private Bitmap _objectImage;
		private bool _visible;
		private Point2D _position;

		/// <summary>
		/// Constructor for when image is set separately
		/// </summary>
		/// <param name="objectMask"></param>
		public UIObject(Rectangle objectMask) {
			_objectMask = objectMask;
			_visible = true;

			SetPosition();
		}

		/// <summary>
		/// Constructor to be used in practice.
		/// Adds image and set objectMask as
		/// </summary>
		/// <param name="objectImage"></param>
		/// <param name="objectMask"></param>
		public UIObject(Bitmap objectImage, Rectangle objectMask) {
			_objectMask = objectMask;
			_objectImage = objectImage;
			_visible = true;

			SetPosition();
		}

		/// <summary>
		/// Set position to where ObjectMask is located
		/// </summary>
		private void SetPosition() {
			_position = new Point2D();
			_position.X = _objectMask.X;
			_position.Y = _objectMask.Y;
		}

		/// <summary>
		/// Check if a point on the screen is over the object's mask
		/// </summary>
		/// <param name="mousePosition"></param>
		/// <returns></returns>
		public bool IsHovering(Point2D mousePosition) {
			return SplashKit.PointInRectangle(mousePosition, _objectMask);
		}

		/// <summary>
		/// Draw object onto screen
		/// </summary>
		public virtual void Draw() {
			GameWindow.DrawBitmap(ObjectImage, Position.X, Position.Y);
		}

		/// <summary>
		/// Return image of object
		/// </summary>
		public Bitmap ObjectImage {
			get	{
				return _objectImage;
			}
			set	{
				_objectImage = value;
			}
		}

		/// <summary>
		/// public getter and setter for _visible property
		/// </summary>
		public bool Visible {
			get	{
				return _visible;
			}
			set	{
				_visible = value;
			}
		}
		
		/// <summary>
		/// Return top-left corner of object
		/// </summary>
		protected Point2D Position {
			get {
				return _position;
			}
			set	{
				_position = value;
			}
		}

		/// <summary>
		/// Rectangle used as mask for UI clicks
		/// </summary>
		protected Rectangle ObjectMask	{
			get	{
				return _objectMask;
			}
		}
	}
}
