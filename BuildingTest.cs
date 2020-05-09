using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public class BuildingTest
	{
		Location l1;
		Location l2;
		Location l3;
		Location l4;
		Building b1;
		Building b2;

		[SetUp]
		public void SetUp() {
			// initialise locations
			l1 = new Location("l1", "l1Info");
			l2 = new Location("l2");
			l3 = new Location("l3");
			l4 = new Location("l4");

			// initialise building
			b1 = new Building("b1");
			b2 = new Building("b2");

			// add connecting locations from l1
			l1.AddConnectingLocation(l2, 0);
			l1.AddConnectingLocation(l3, 1);
			l1.AddConnectingLocation(l4, 2);

			// set b1 as L1's building
			l1.SetBuilding(b1);
		}

		[Test]
		public void BuildingInLocation() {
			// enter building from l1
			Assert.AreEqual(b1, l1.EnterBuilding);
		}

		[Test]
		public void NoBuildingInLocation() {
			// fail enter if no building exists
			Assert.IsNull(l2.EnterBuilding);
		}

		[Test]
		public void ExitBuilding_Success() {
			// get original location from building
			Assert.AreEqual(l1, b1.ParentLoc);
		}

		[Test]
		public void BuildingInformation_Success()	{
			// get original location info from building's location
			Assert.AreEqual("l1Info", b1.GetInfo);
		}

		[Test]
		public void BuildingInformation_None() {
			// no info should return null
			Assert.IsNull(b2.GetInfo);
		}
	}
}
