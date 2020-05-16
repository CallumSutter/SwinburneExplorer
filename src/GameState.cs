using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
    /// <summary>
    /// Enum that holds a game states
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// Player is at main menu
        /// </summary>
        MainMenu,

        /// <summary>
        /// Player is moving between locations/main game
        /// </summary>
        Travelling,

        /// <summary>
        /// player is in a building
        /// </summary>
        InBuilding,

        /// <summary>
        /// player is in a classroom
        /// </summary>
        InClassroom,

        /// <summary>
        /// Player is looking at the map
        /// </summary>
        FullscreenMap,

        /// <summary>
        /// Exiting game
        /// </summary>
        Exit,
    }
}
