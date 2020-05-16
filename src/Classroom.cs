using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
    /// <summary>
    /// used to hold the room ID of the classroom - used by building class
    /// </summary>
    public class Classroom
    {
        private string _roomId;

        /// <summary>
        /// initialiser for the Classroom class
        /// </summary>
        /// <param name="roomId"></param>
        public Classroom(string roomId) {
            _roomId = roomId;          
        }

        /// <summary>
        /// returns room ID of Classroom
        /// </summary>
        public string RoomId {
            get { 
                return _roomId; 
            }
        }
    }
}
