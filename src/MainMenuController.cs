using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer
{
	public static class MainMenuController
	{
		/// <summary>
		/// Process mouse inputs in MainMenu
		/// </summary>
		public static void HandleMouseInput()
		{
			//if mouse click on arrow, move to linked location (if there is one)
			if (!SplashKit.MouseClicked(MouseButton.LeftButton))
			{
				return;
			}

			if (GameController._mainMenu.PlayPressed)
			{
				if (GameController._mainMenu.CheckMouseInTrainButton())
				{
					Console.WriteLine("Clicked Train");

					//initialse player
					GameController._player = new Player(GameResources.GetLocation("Train"));

					SetupStartGame();
				}

				if (GameController._mainMenu.CheckMouseInCampusButton())
				{
					Console.WriteLine("Clicked Campus");

					//initialise player
					GameController._player = new Player(GameResources.GetLocation("instreet5"));

					SetupStartGame();
				}
			}

			else
			{
				if (GameController._mainMenu.CheckMouseInPlayButton())
				{
					Console.WriteLine("Clicked Play");
					GameController._mainMenu.PlayPressed = true;
					PlayMenuSelectSound();
				}

				if (GameController._mainMenu.CheckMouseInExitButton())
				{
					Console.WriteLine("Clicked Exit");
					GameController._currentState = GameState.Exit.ToString();
					PlayMenuSelectSound();
				}
			}
		}

		/// <summary>
		/// Handles all user inputs in MainMenu
		/// </summary>
		public static void HandleInput()
		{
			HandleMouseInput();
		}

		/// <summary>
		/// Perform necessary actions to start game
		/// </summary>
		private static void SetupStartGame()
		{
			GameController._currentState = GameState.Travelling.ToString();
			GameResources.PlayBGM();
			PlayMenuSelectSound();

			TravellingController.LoadLocationImage(GameController._player.Location);

			//set up objectives
			Objective newObjective = new Objective(1);
			GameController._player.AddNewObjective(newObjective);
		}

		/// <summary>
		/// Play sound indicating menu button pressed
		/// </summary>
		private static void PlayMenuSelectSound()
		{
			SplashKit.PlaySoundEffect(GameResources.GetSound("menuSelect"));
		}
	}
}
