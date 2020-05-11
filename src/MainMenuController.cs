using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    public class MainMenuController {

        public MainMenuController() {

        }

        public static void HandleMouseInput() {
			//if mouse click on arrow, move to linked location (if there is one)
			if (SplashKit.MouseClicked(MouseButton.LeftButton)) {

				if (GameController._mainMenu.PlayPressed) {
					if (GameController._mainMenu.CheckMouseInTrainButton()) {
						Console.WriteLine("Clicked Train");
						GameController._currentState = GameState.Travelling.ToString();

						//play background music
						GameResources.PlayBGM();

						//initialse player
						GameController._player = new Player(GameResources.GetLocation("Train"));
						TravellingController.LoadLocationImage(GameController._player.Location);
						SplashKit.PlaySoundEffect(GameResources.GetSound("menuSelect"));
						Objective newObjective = new Objective(1);
						GameController._player.AddNewObjective(newObjective);
					}

					if (GameController._mainMenu.CheckMouseInCampusButton()) {
						Console.WriteLine("Clicked Campus");
						GameController._currentState = GameState.Travelling.ToString();

						//play background music
						GameResources.PlayBGM();

						//initialise player
						GameController._player = new Player(GameResources.GetLocation("instreet5"));
						TravellingController.LoadLocationImage(GameController._player.Location);
						SplashKit.PlaySoundEffect(GameResources.GetSound("menuSelect"));
						Objective newObjective = new Objective(1);
						GameController._player.AddNewObjective(newObjective);
					}
				}

				else {
					if (GameController._mainMenu.CheckMouseInPlayButton()) {
						Console.WriteLine("Clicked Play");
						GameController._mainMenu.PlayPressed = true;
						SplashKit.PlaySoundEffect(GameResources.GetSound("menuSelect"));
					}

					if (GameController._mainMenu.CheckMouseInExitButton()) {
						Console.WriteLine("Clicked Exit");
						GameController._currentState = GameState.Exit.ToString();
						SplashKit.PlaySoundEffect(GameResources.GetSound("menuSelect"));
					}
				}				
			}
		}

        public static void HandleInput() {
            HandleMouseInput();
        }
    }
}
