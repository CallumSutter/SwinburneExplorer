using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
    public class Building : Location
    {
        private int _floorCount;
		private int _currentFloor;
        private List<Classroom> _classrooms = new List<Classroom>();
		private bool _inClassroom;

		// parent location
		private Location _parentLoc;

		/// <summary>
		/// Building constructor
		/// </summary>
		/// <param name="name">name of building</param>
		/// <param name="shortName">shorted name</param>
		/// <param name="floorCount">number of floors in building</param>
        public Building(string name, string shortName, int floorCount) : base(name){ 
			_parentLoc = null;
			base.LocationImage = GameResources.GetImage("building");
			_floorCount = floorCount;
			_currentFloor = 1;
			_inClassroom = false;

			InitialiseClassrooms(shortName);
        }

		/// <summary>
		/// Generates classrooms depending on number of floors
		/// </summary>
		/// <param name="shortName">name of building</param>
		private void InitialiseClassrooms(string shortName) {
			for (int i = 1; i <= _floorCount; i++) {
				_classrooms.Add(new Classroom(shortName + " " + i.ToString() + "01"));
			}
		}

		/// <summary>
		/// Enter classroom
		/// </summary>
		public void EnterClassroom() {
			_inClassroom = true;
		}

		/// <summary>
		/// Exit classroom
		/// </summary>
		public void ExitClassroom() {
			_inClassroom = false;
		}

		/// <summary>
		/// Go to next floor
		/// </summary>
		public void UpFloor() {
			if (_currentFloor != _floorCount) {
				_currentFloor++;
			}
		}

		/// <summary>
		/// Go to previous floor
		/// </summary>
		public void DownFloor() {
			if (_currentFloor > 1) {
				_currentFloor--;
			}
		}

		/// <summary>
		/// Get current classroom
		/// </summary>
		public Classroom CurrentClassroom {
			get {
				return _classrooms[CurrentFloor - 1];
			}
		}

		/// <summary>
		/// Get number of floors in building
		/// </summary>
		public int FloorCount {
            get { 
                return _floorCount; 
            }
            set {
                _floorCount = value;
            }
        }

		/// <summary>
		/// Get current floor player is at
		/// </summary>
		public int CurrentFloor {
			get {
				return _currentFloor;
			}
		}

		/// <summary>
		/// Return info of parent locaton
		/// </summary>
		public override string GetInfo {
			get	{
				if (_parentLoc != null)	{
					return _parentLoc.GetInfo;
				}
				else {
					return null;
				}
			}
		}

		/// <summary>
		/// Return parent location
		/// </summary>
		public Location ParentLoc {
			get	{
				return _parentLoc;
			}
			set {
				_parentLoc = value;
			}
		}

		/// <summary>
		/// Getter for inClassroom
		/// </summary>
		public bool InClassroom {
			get {
				return _inClassroom;
			}
		}
    }
}
