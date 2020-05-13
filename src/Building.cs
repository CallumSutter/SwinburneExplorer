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

        public Building(string name, string shortName, int floorCount) : base(name){ 
			_parentLoc = null;
			base.LocationImage = GameResources.GetImage("building");
			_floorCount = floorCount;
			_currentFloor = 1;
			_inClassroom = false;

			AssignClassrooms(shortName);
        }

		private void AssignClassrooms(string shortName) {
			for (int i = 1; i <= _floorCount; i++) {
				_classrooms.Add(new Classroom(shortName + " " + i.ToString() + "01"));
			}
		}

		public void EnterClassroom() {
			_inClassroom = true;
		}

		public void ExitClassroom() {
			_inClassroom = false;
		}

		public void UpFloor() {
			if (_currentFloor != _floorCount) {
				_currentFloor++;
			}
		}

		public void DownFloor() {
			if (_currentFloor > 1) {
				_currentFloor--;
			}
		}

		public Classroom CurrentClassroom {
			get {
				return _classrooms[CurrentFloor - 1];
			}
		}

		public int FloorCount {
            get { 
                return _floorCount; 
            }
            set {
                _floorCount = value;
            }
        }

		public int CurrentFloor {
			get {
				return _currentFloor;
			}
		}

		/// <summary>
		/// Return info of parent locaton
		/// </summary>
		public new string GetInfo {
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

		public bool InClassroom {
			get {
				return _inClassroom;
			}
		}
    }
}
