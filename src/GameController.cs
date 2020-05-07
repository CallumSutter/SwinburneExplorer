using System;
using SplashKitSDK;
using Swinburneexplorer;
using System.Resources;

public class GameController
{
	public static Window gameWindow;
    public static Player _player;
    public static UI _ui;
    public static Map theMap;
	public const int WINDOW_HEIGHT = 583;
	public const int WINDOW_WIDTH = 1235;
    //Constants for directions
    public const int FORWARD = 0;
    public const int BACKWARD = 1;
    public const int LEFT = 2;
    public const int RIGHT = 3;
    public static string _currentState;
	
    public static void Main(string[] args)
    {
        //initialise varaiables
        _ui = new UI();

        //Location location1 = new Location("location1.jpg", "1");
        //Location location2 = new Location("location2.jpg", "2");
        //Location location3 = new Location("placeholder.jpg", "3");

        //location1.AddConnectingLocation(location2, FORWARD);
        //location1.AddConnectingLocation(location3, BACKWARD);

        //location2.AddConnectingLocation(location1, BACKWARD);
        //location3.AddConnectingLocation(location1, FORWARD);

		//new game window
		gameWindow = new Window("SwinExplorer", WINDOW_WIDTH , WINDOW_HEIGHT);

        //Displays loading screen
		GameResources.LoadingScreen();

        //play background music
        GameResources.PlayBGM();

        //add test location
        //Location location1 = GameResources.getLocation("toLodges6");

        //initialse player
        _player = new Player(GameResources.getLocation("Train"));
		TravellingController.LoadLocationImage(_player.Location);

        //initialse map
        theMap = new Map();

        //starting with travelling state
        _currentState = GameState.Travelling.ToString();

        do {
            //control for drawing to screen
            gameWindow.Clear(Color.White);           
            switch (_currentState) {
                case ("MainMenu"):
                    //Draw();
                    break;
                case ("Travelling"):
                    _ui.Draw();
                    break;
                case ("FullscreenMap"):
                    theMap.Draw();
                    break;
                default:
                    break;
            }
            gameWindow.Refresh();

            //get user input and process events
            SplashKit.ProcessEvents();
            switch (_currentState) {
                case ("MainMenu"):
                    //MainMenuController.HandleInput();
                    break;
                case ("Travelling"):
                    TravellingController.HandleInput();
                    break;
                case ("FullscreenMap"):
                    MapController.HandleInput();
                    break;
                default:
                    break;
            }
        }
        while (!SplashKit.WindowCloseRequested(gameWindow));
    }
}
