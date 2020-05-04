﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer {
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
			return (GameController._player.Location.GetLocationInDirection(direction) != null);
		}

		/// <summary>
		/// Moves player in a given direction
		/// </summary>
		/// <param name="direction"></param>
		public static void MovePlayer(int direction) {
			GameController._player.Location = GameController._player.Location.GetLocationInDirection(direction);
		}

		/// <summary>
		/// Parse direction from string to int
		/// </summary>
		/// <param name="dirStr"></param>
		/// <returns>parsed direction</returns>
		public static int ParseDirection(string dirStr) {
			int parsedDirection = default;

			switch(dirStr) {
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
					throw new InvalidOperationException($"{dirStr} is an invalid direction");
			}

			return parsedDirection;
		}

		/// <summary>
		/// Try moving in a direction
		/// Play corresponding sounds if failed
		/// </summary>
		/// <param name="dirStr"></param>
		public static void TryMove(string dirStr) {
			int direction = ParseDirection(dirStr);

			if (CheckLocationValid(direction)) {
				MovePlayer(direction);
				PlayCorrectSound();
			}
			else {
				PlayIncorrectSound();
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
				string arrowDirectionClicked = GameResources.MouseInArrow().ToString();

				TryMove(arrowDirectionClicked);
			}
		}

		/// <summary>
		/// Handle inputs for travelling with keyboard inputs
		/// </summary>
		public static void HandleKeyboardTravelInput() {
			//if proper key is pressed, change locations in a direction
			if (SplashKit.KeyTyped(KeyCode.WKey)) {
				TryMove("Up");
			}

			if (SplashKit.KeyTyped(KeyCode.SKey)) {
				TryMove("Down");
			}

			if (SplashKit.KeyTyped(KeyCode.AKey)) {
				TryMove("Right");
			}

			if (SplashKit.KeyTyped(KeyCode.DKey)) {
				TryMove("Left");
			}
		}

		/// <summary>
		/// Handle all inputs related to travel
		/// </summary>
        public static void HandleInput() {
			HandleMapInput();
			HandleMouseTravelInput();
			HandleKeyboardTravelInput();
        }
    }
}
