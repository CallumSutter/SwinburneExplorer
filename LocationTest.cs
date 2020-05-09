using NUnit.Framework;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public class LocationTest
	{
		// constructor overload for description
		Location b0 = new Location("b0", "i0");
		Location b1 = new Location("b1");
		Location b2 = new Location("b2");
		Location b3 = new Location("b3");
		Location b4 = new Location("b4");

		const int UP = 0;
		const int DOWN = 1;
		const int LEFT = 2;
		const int RIGHT = 3;

		[SetUp]
		public void Setup()
		{
			b0.AddConnectingLocation(b1, UP);
			b0.AddConnectingLocation(b2, DOWN);
			b0.AddConnectingLocation(b3, LEFT);
			b0.AddConnectingLocation(b4, RIGHT);
		}

		[TestCase(UP, "b1")]
		[TestCase(DOWN, "b2")]
		[TestCase(LEFT, "b3")]
		[TestCase(RIGHT, "b4")]
		public void TestLocationConnection_ConnectedLocations(int dir, string dest)
		{
			Assert.AreEqual(b0.GetLocationInDirection(dir).Name, dest);
		}

		[TestCase(UP, null)]
		[TestCase(DOWN, null)]
		[TestCase(LEFT, null)]
		[TestCase(RIGHT, null)]
		public void TestLocationConnection_UnconnectedLocations(int dir, string dest)
		{
			Assert.AreEqual(b1.GetLocationInDirection(dir), dest);
		}

		[Test]
		public void ShowInformation_InfoExist()
		{
			// building info will be accessed via GetInfo property
			Assert.AreEqual(b0.GetInfo, "i0");
		}

		[Test]
		public void ShowInformation_InfoNull()
		{
			// GetInfo should return null (do in constructor)
			Assert.AreEqual(b2.GetInfo, null);
		}
	}
}