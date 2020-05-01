using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
    public enum GameState
    {
        //Player is at primary location
        startLocation,

        //Player is moving between locations
        Travelling,

        //Player has arrived at destination
        finalDestination,
    }
}
