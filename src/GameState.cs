using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
    public enum GameState
    {
        //Player is at main menu
        MainMenu,

        //Player is moving between locations/main game
        Travelling,

        //Player is looking at the map
        FullscreenMap,

        //Exiting game
        Exit,
    }
}
