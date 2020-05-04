using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer {
    public class TravellingController {

        public static void HandleInput() {
            //check if the map icon was clicked on
            if (GameController.theMap.CheckMapClicked()) {
                GameController._currentState = GameState.FullscreenMap.ToString();
                GameController.theMap.Fullscreen = true;
                SplashKit.PlaySoundEffect(GameResources.GetSound("toggleMap"));
            }

            //if mouse click on arrow, move to linked location (if there is one)
            if (SplashKit.MouseClicked(MouseButton.LeftButton)) {             
                string arrowDirectionClicked = GameResources.MouseInArrow().ToString();
                switch (arrowDirectionClicked) {
                    case ("Up"):
                        if (GameController._player.Location.GetLocationInDirection(GameController.FORWARD) != null) {
                            GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.FORWARD);
                            SplashKit.PlaySoundEffect(GameResources.GetSound("correct"));
                        }
                        else {
                            SplashKit.PlaySoundEffect(GameResources.GetSound("incorrect"));
                        }
                        break;

                    case ("Down"):
                        if (GameController._player.Location.GetLocationInDirection(GameController.BACKWARD) != null) {
                            GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.BACKWARD);
                            SplashKit.PlaySoundEffect(GameResources.GetSound("correct"));
                        } 
                        else {
                            SplashKit.PlaySoundEffect(GameResources.GetSound("incorrect"));
                        }
                        break;

                    case ("Left"):
                        if (GameController._player.Location.GetLocationInDirection(GameController.LEFT) != null) {
                            GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.LEFT);
                            SplashKit.PlaySoundEffect(GameResources.GetSound("correct"));
                        }
                        else {
                            SplashKit.PlaySoundEffect(GameResources.GetSound("incorrect"));
                        }
                        break;

                    case ("Right"):
                        if (GameController._player.Location.GetLocationInDirection(GameController.RIGHT) != null) {
                            GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.RIGHT);
                            SplashKit.PlaySoundEffect(GameResources.GetSound("correct"));
                        }
                        else {
                            SplashKit.PlaySoundEffect(GameResources.GetSound("incorrect"));
                        }
                        break;

                    default:
                        break;
                }
            }

            //if proper key is pressed, change locations in a direction
            if (SplashKit.KeyTyped(KeyCode.WKey)) {
                if (GameController._player.Location.GetLocationInDirection(GameController.FORWARD) != null) {
                    GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.FORWARD);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.SKey)) {
                if (GameController._player.Location.GetLocationInDirection(GameController.BACKWARD) != null) {
                    GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.BACKWARD);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.AKey)) {
                if (GameController._player.Location.GetLocationInDirection(GameController.LEFT) != null) {
                    GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.LEFT);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.DKey)) {
                if (GameController._player.Location.GetLocationInDirection(GameController.RIGHT) != null) {
                    GameController._player.Location = GameController._player.Location.GetLocationInDirection(GameController.RIGHT);
                }
            }
        }

    }

    
}
