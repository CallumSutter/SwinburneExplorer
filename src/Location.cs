using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Swinburneexplorer {
	public class Location {
		private Bitmap _locationImage;
		private Location[] _paths;
		private Building _building;
		private string _name;
		private string _desc;

		/// <summary>
		/// Initialiser for Location class
		/// </summary>
		/// <param name="locationImage">path of the image file, defaults to image folder in bin folder</param>
		/// <param name="name">name of the location</param>
		/// <returns></returns>
		public Location(string name) {
			_name = name;
			_desc = null;

			InitialiseLocation();
		}

		/// <summary>
		/// Constructor overload for description
		/// </summary>
		/// <param name="name"></param>
		/// <param name="desc"></param>
		public Location(string name, string desc) {
			_name = name;
			_desc = desc;

			InitialiseLocation();
		}

		/// <summary>
		/// Initialise members not passed in through arguments
		/// </summary>
		private void InitialiseLocation() {
			_locationImage = null;
			_paths = new Location[4];

			// initialise paths to null
			for(int i = 0; i < 4; i++) {
				_paths[i] = null;
			}
		}

		/// <summary>
		/// add a connection location to the current location (one location for each direction)
		/// also sets a bool value to true for that direction
		/// </summary>
		/// <param name="location">Location to be added</param>
		/// <param name="direction">Direction of the new location from current location</param>
		/// <returns></returns>
		public void AddConnectingLocation(Location location, int direction) {
			if (direction < 0 | direction > 3) {
				return;
			}

			_paths[direction] = location;
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
			if (direction < 0 | direction > 3) {
				return null;
			}

			return _paths[direction];
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

		/// <summary>
		/// Getter for location information
		/// </summary>
		public string GetInfo {
			get {
				return _desc;
			}
		}
		
		/// <summary>
		/// Return contained building
		/// </summary>
		public Building EnterBuilding {
			get	{
				return _building;
			}
		}

		public Location[] Paths
		{
			get
			{
				return _paths;
			}
		}
	}
}
