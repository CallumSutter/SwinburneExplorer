using NUnit.Framework;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public class UIObjectTests
	{
		// object drawn onto UI
		UIObject buildingIcon;
		Rectangle buildingIconMask;

		[SetUp]
		public void Setup()
		{
			// mask for UI object
			buildingIconMask = new Rectangle();
			buildingIconMask.X = 0;
			buildingIconMask.Y = 0;
			buildingIconMask.Height = 10;
			buildingIconMask.Width = 10;

			// UIObject constructor has two parameters: Image, and mask for image
			buildingIcon = new UIObject(/*Object image,*/ buildingIconMask);
			buildingIcon.Visible = true;
		}

		[TestCase(0,0)]
		[TestCase(10, 0)]
		[TestCase(0, 10)]
		[TestCase(10, 10)]
		[TestCase(5, 5)]
		public void MouseInIcon_IsTrue(double x, double y)
		{
			Point2D mousePosition = new Point2D();
			mousePosition.X = x;
			mousePosition.Y = y;

			Assert.IsTrue(buildingIcon.IsHovering(mousePosition));
		}

		[TestCase(-1, -1)]
		[TestCase(11, -1)]
		[TestCase(-1, 11)]
		[TestCase(11, 11)]
		[TestCase(11, 5)]
		public void MouseInIcon_IsFalse(double X, double Y)
		{
			Point2D mousePosition = new Point2D();
			mousePosition.X = X;
			mousePosition.Y = Y;

			Assert.IsFalse(buildingIcon.IsHovering(mousePosition));
		}
	}
}