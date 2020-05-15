using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    public class Player {
        private Location _location;
        private string _startingLocation;
        private List<Objective> _objectives;
        //private bool _inBuilding;

		/// <summary>
		/// Constructor for Player
		/// </summary>
		/// <param name="location">initial location</param>
        public Player(Location location) {
            _location = location;
            _startingLocation = location.Name;
            _objectives = new List<Objective>();
            //_inBuilding = false;
        }

		/// <summary>
		/// Add a new objective
		/// </summary>
		/// <param name="objective"></param>
        public void AddNewObjective(Objective objective) {
            _objectives.Add(objective);
        }

		/// <summary>
		/// Check if a building exists within a location
		/// </summary>
		/// <returns>building in location</returns>
        public Building ReturnBuildingIfExists() {
            if (GameController._player.Location.GetType() != GameResources.GetLocation("Train").GetType()) {
                return (Building)GameController._player.Location;
            }
            
            else {
                return null;
            }
        }

		/// <summary>
		/// Return objective currently being done
		/// </summary>
        public Objective CurrentObjective {
            get {
                int size = _objectives.Count - 1;
                return _objectives[size];
            }
        }

		/// <summary>
		/// Return number of objectives
		/// </summary>
        public int ObjectiveCount {
            get {
                return _objectives.Count;
            }
        }

		/// <summary>
		/// Get/Set current location of player
		/// </summary>
        public Location Location {
            get {
                return _location;
            }
            set {
                _location = value;
            }
        }

		/// <summary>
		/// Get/Set starting location of player
		/// </summary>
        public string StartingLocation {
            get {
                return _startingLocation;
            }
            set {
                _startingLocation = value;
            }
        }

		/// <summary>
		/// Assign new objective to player
		/// </summary>
        public void AssignNewObjective() {
            Objective newObjective = new Objective(_objectives.Count + 1);
            AddNewObjective(newObjective);
        }
    }
}
