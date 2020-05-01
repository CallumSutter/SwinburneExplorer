using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
	public class Player
	{
		//private Location _location;
		//private List<Objective> _objectives;
		//private Map _map;
		private string _name;

		public Player(string aName)
		{
			_name = aName;
		}

		public string Name
		{
			get
			{
				return _name;
			}
		}
	}
}
