using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
    public class Building : Location
    {
        private int _floorCount;
        private List<Classroom> _classrooms = new List<Classroom>();

        public Building(string name) : base(name)
        {  }

        public int FloorCount
        {
            get { return _floorCount; }
            set { _floorCount = value; }
        }

    }
}
