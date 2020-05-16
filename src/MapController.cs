using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    /// <summary>
    /// Handles input when map is drawn to fullscreen
    /// </summary>
    class MapController {
        public static void HandleInput() {
            if (GameController.theMap.CheckMapClicked()) {
                GameController._currentState = GameState.Travelling.ToString();
                GameController.theMap.Fullscreen = false;
                SplashKit.PlaySoundEffect(GameResources.GetSound("toggleMap"));
            }
        }
    }
}
