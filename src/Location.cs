using System;
using System.Collections.Generic;
using SplashKitSDK;
using Swinburneexplorer;

namespace Swinburneexplorer {
	public class Location {

		private Bitmap _locationImage;
		private bool [] _canGoInDirection;
		private Location [] _connectedLocation;
		private string _name;

		/// <summary>
		/// Initialiser for Location class
		/// </summary>
		/// <param name="locationImage">path of the image file, defaults to image folder in bin folder</param>
		/// <param name="name">name of the location</param>
		/// <returns></returns>
		public Location(string name) {
			_locationImage = null;
			_name = name;
			_canGoInDirection = new bool[4];
			_connectedLocation = new Location[4];
		}

		/// <summary>
		/// add a connection location to the current location (one location for each direction)
		/// also sets a bool value to true for that direction
		/// </summary>
		/// <param name="location">Location to be added</param>
		/// <param name="direction">Direction of the new location from current location</param>
		/// <returns></returns>
		public void AddConnectingLocation(Location location, int direction) {
			_canGoInDirection[direction] = true;
			_connectedLocation[direction] = location;
		}

		/// <summary>
		/// add a connection location to the current location (one location for each direction)
		/// </summary>
		/// <param name="direction">
		/// direction to get location from (0 for forward, 1 for backward
		/// 2 for left, 3 for right)
		/// </param>
		/// <returns>location in a specified direction, or null</returns>
		public Location GetLocationInDirection(int direction) {
			if (_canGoInDirection[direction]) {
				return _connectedLocation[direction];
			}
			else {
				return null;
			}
		}

		/// <summary>
		/// getter for location image bitmap
		/// </summary>
		/// <returns></returns>
		public Bitmap LocationImage {
			get {
				return _locationImage;
			}
			set	{
				_locationImage = value;
			}
		}

		/// <summary>
		/// getter for location name
		/// </summary>
		/// <returns></returns>
		public string Name {
			get {
				return _name;
			}
		}



	}


}
