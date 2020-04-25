using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    public class Player {
        private Location _location;

        public Player(Location location) {
            _location = location;
        }

        public Location Location {
            get {
                return _location;
            }
            set {
                _location = value;
            }
        }

    }
}
