using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer {
	/// <summary>
	/// This class controls the user input when in 
	/// the travelling section of the game
	/// </summary>
	public class TravellingController {
		/// <summary>
		/// Sound played when travel valid
		/// </summary>
		public static void PlayCorrectSound() {
			SplashKit.PlaySoundEffect(GameResources.GetSound("correct"), (float)0.8);
		}

		/// <summary>
		/// Sound played when travel invalid
		/// </summary>
		public static void PlayIncorrectSound() {
			SplashKit.PlaySoundEffect(GameResources.GetSound("incorrect"), (float)0.2);
		}

		/// <summary>
		/// Map toggle sound
		/// </summary>
		public static void PlayMapToggleSound() {
			SplashKit.PlaySoundEffect(GameResources.GetSound("toggleMap"));
		}

		/// <summary>
		/// Check that a travel in a given direction is valid
		/// </summary>
		/// <param name="direction"></param>
		/// <returns>if location exists in given direction</returns>
		public static bool CheckLocationValid(int direction) {
			return (GameController.Player.Location.GetLocationInDirection(direction) != null);
		}

		/// <summary>
		/// Moves player in a given direction
		/// </summary>
		/// <param name="direction"></param>
		public static void MovePlayer(int direction) {
			GameController.Player.Location = GameController.Player.Location.GetLocationInDirection(direction);

			LoadLocationImage(GameController.Player.Location);
		}

		/// <summary>
		/// Load location image to GameResources
		/// and add location image to current location
		/// </summary>
		/// <param name="aLoc"></param>
		public static void LoadLocationImage(Location aLoc) {
			if (aLoc.LocationImage == null) {
				GameResources.LoadLocationImage(aLoc.Name);
				aLoc.LocationImage = GameResources.GetImage(aLoc.Name);
			}
		}

		/// <summary>
		/// Parse direction from string to int
		/// </summary>
		/// <param name="dirStr"></param>
		/// <returns>parsed direction</returns>
		public static int ParseDirection(string dirStr) {
			int parsedDirection = default;

			switch (dirStr) {
				case "Up":
					parsedDirection = GameController.FORWARD;
					break;
				case "Down":
					parsedDirection = GameController.BACKWARD;
					break;
				case "Left":
					parsedDirection = GameController.LEFT;
					break;
				case "Right":
					parsedDirection = GameController.RIGHT;
					break;
				default:
					break;
			}

			return parsedDirection;
		}

		/// <summary>
		/// Try moving in a direction
		/// Play corresponding sounds if failed
		/// </summary>
		/// <param name="dirStr">direction as string</param>
		public static void TryMove(string dirStr) {
			int direction = ParseDirection(dirStr);

			if (CheckLocationValid(direction)) {
				MovePlayer(direction);
				PlayCorrectSound();
			} else if (GameController._currentState == GameState.InBuilding.ToString()) {
				if (direction == GameController.FORWARD) {
					GameController.Player.ReturnBuildingIfExists().UpFloor();
					Console.WriteLine("Moved to Floor " + GameController.Player.ReturnBuildingIfExists().CurrentFloor.ToString());
				} else if (direction == GameController.BACKWARD) {
					GameController.Player.ReturnBuildingIfExists().DownFloor();
					Console.WriteLine("Moved to Floor " + GameController.Player.ReturnBuildingIfExists().CurrentFloor.ToString());
				}
			}
		}

		/// <summary>
		/// Handle inputs for map
		/// </summary>
		public static void HandleMapInput() {
			//check if the map icon was clicked on
			if (GameController.theMap.CheckMapClicked()) {
				GameController._currentState = GameState.FullscreenMap.ToString();
				GameController.theMap.Fullscreen = true;
				PlayMapToggleSound();
			}
		}

		/// <summary>
		/// Handle inputs for travelling with mouse click
		/// </summary>
		public static void HandleMouseTravelInput() {
			//if mouse click on arrow, move to linked location (if there is one)
			if (SplashKit.MouseClicked(MouseButton.LeftButton)) {
				string arrowDirectionClicked = GameController.UI.CheckMouseInArrow().ToString();

				if (arrowDirectionClicked != "") {
					TryMove(arrowDirectionClicked);
				}

				if (GameController.UI.EnterButton.Visible) {
					if (GameController.UI.CheckMouseInEnterButton()) {
						if (GameController._currentState == GameState.Travelling.ToString()) {
							Console.WriteLine("Clicked Enter");
							GameController.Player.Location = GameController.Player.Location.EnterBuilding;
							GameController._currentState = GameState.InBuilding.ToString();
						} 
						else if (GameController._currentState == GameState.InBuilding.ToString()) {
							GameController.Player.ReturnBuildingIfExists().EnterClassroom();
							GameController._currentState = GameState.InClassroom.ToString();
						}
					}
				}

				if (GameController.UI.EnterButton2.Visible) {
					if (GameController.UI.CheckMouseInEnter2Button()) {
						GameController.Player.ReturnBuildingIfExists().EnterClassroom();
						GameController._currentState = GameState.InClassroom.ToString();
					} 
				}

				if (GameController.UI.ExitButton.Visible) {
					if (GameController.UI.CheckMouseInEnterButton()) {
						if (GameController._currentState == GameState.InClassroom.ToString()) {
							Console.WriteLine("Clicked Exit");
							GameController.Player.ReturnBuildingIfExists().ExitClassroom();
							GameController._currentState = GameState.InBuilding.ToString();
						}
					}
				}

				if (GameController.UI.EnterButton2.Visible) {
					if (GameController.UI.CheckMouseInExit2Button()) {
						GameController.Player.Location = GameController.Player.ReturnBuildingIfExists().ParentLoc;
						GameController._currentState = GameState.Travelling.ToString();
					}
				}

				if (GameController.UI.CheckMouseInInfoButton()) {
					GameController.UI.DrawBuildingInfo();
				}

				if (GameController._ui.CheckMouseInQuitButton()) {
					Console.WriteLine("Clicked Quit");
					GameController._player.ResetPlayer();
					GameController._currentState = GameState.MainMenu.ToString();
					GameResources.PlayBGM();
				}
			}
		}

		/// <summary>
		/// Handle inputs for travelling with keyboard inputs
		/// </summary>
		public static void HandleKeyboardTravelInput() {
			//if proper key is pressed, change locations in a direction
			if (SplashKit.KeyTyped(KeyCode.WKey) || SplashKit.KeyTyped(KeyCode.UpKey)) {
				TryMove("Up");
			}

			if (SplashKit.KeyTyped(KeyCode.SKey) || SplashKit.KeyTyped(KeyCode.DownKey)) {
				TryMove("Down");
			}

			if (SplashKit.KeyTyped(KeyCode.DKey) || SplashKit.KeyTyped(KeyCode.RightKey)) {
				TryMove("Right");
			}

			if (SplashKit.KeyTyped(KeyCode.AKey) || SplashKit.KeyTyped(KeyCode.LeftKey)) {
				TryMove("Left");
			}

			if (SplashKit.KeyTyped(KeyCode.IKey)) {
			GameController.UI.DrawBuildingInfo();
			}
		}

		/// <summary>
		/// Check if objective is complete
		/// </summary>
		public static void CheckIfObjectiveIsComplete() {
			if (GameController._currentState == GameState.Travelling.ToString()) {
				if (GameController.Player.CurrentObjective.CheckIfObjectiveIsComplete(GameController.Player.Location.Name)) {
					CompleteObjective();
				}
			}
			else if (GameController._currentState == GameState.InClassroom.ToString()) {
				if (GameController.Player.CurrentObjective.CheckIfObjectiveIsComplete(GameController.Player.ReturnBuildingIfExists().CurrentClassroom.RoomId)) {
					CompleteObjective();
				}
			}
		}

		/// <summary>
		/// Add new objective if all objectives have
		/// not been completed
		/// </summary>
		public static void CompleteObjective() {
			if (GameController.Player.ObjectiveCount <= 6) {
				GameController.UI.DrawObjectiveComplete();
				GameController.Player.AssignNewObjective();
			}
		}

		/// <summary>
		/// Handle all inputs related to travel
		/// </summary>
		public static void HandleInput() {
			HandleMapInput();
			HandleMouseTravelInput();
			HandleKeyboardTravelInput();
			CheckIfObjectiveIsComplete();
        }
    }
}
