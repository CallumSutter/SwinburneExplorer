using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
    public class Building : Location
    {
        private int _floorCount;
        private List<Classroom> _classrooms = new List<Classroom>();

		// parent location
		private Location _parentLoc;

        public Building(string name) : base(name){ 
        
        }

        public int FloorCount {
            get { 
                return _floorCount; 
            }
            set {
                _floorCount = value;
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
    }
}
