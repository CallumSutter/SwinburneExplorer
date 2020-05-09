using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    public class Player {
        private Location _location;
        private string _startingLocation;
        private List<Objective> _objectives;

        public Player(Location location) {
            _location = location;
            _startingLocation = location.Name;
            _objectives = new List<Objective>();
        }

        public void AddNewObjective(Objective objective) {
            _objectives.Add(objective);
        }

        public Objective CurrentObjective {
            get {
                int size = _objectives.Count - 1;
                return _objectives[size];
            }
        }

        public int ObjectiveCount {
            get {
                return _objectives.Count;
            }
        }


        public Location Location {
            get {
                return _location;
            }
            set {
                _location = value;
            }
        }

        public string StartingLocation {
            get {
                return _startingLocation;
            }
            set {
                _startingLocation = value;
            }
        }

        public void AssignNewObjective() {
            Objective newObjective = new Objective(_objectives.Count + 1);
            AddNewObjective(newObjective);
        }
    }
}
