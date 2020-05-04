using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    class MapController {
        public static void HandleInput() {
            if (GameController.theMap.CheckMapClicked()) {
                GameController._currentState = GameState.Travelling.ToString();
                GameController.theMap.Fullscreen = false;
            }
        }
    }
}
