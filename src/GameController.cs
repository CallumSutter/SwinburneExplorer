using System;
using SplashKitSDK;
using Swinburneexplorer;
using Resources;

public class GameController
{
	public static Window gameWindow;
    public static Player _player;
    public static UI _ui;
	public const int WINDOW_HEIGHT = 600;
	public const int WINDOW_WIDTH = 800;
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

        Location location1 = new Location("location1.jpg", "1");
        Location location2 = new Location("location2.jpg", "2");
        Location location3 = new Location("placeholder.jpg", "3");

        location1.AddConnectingLocation(location2, FORWARD);
        location1.AddConnectingLocation(location3, BACKWARD);

        location2.AddConnectingLocation(location1, BACKWARD);
        location3.AddConnectingLocation(location1, FORWARD);

        _player = new Player(location1);

		//new game window
		gameWindow = new Window("SwinExplorer", WINDOW_WIDTH , WINDOW_HEIGHT);

        //Displays loading screen
		GameResources.LoadingScreen();

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
                    //Draw();
                    break;
                default:
                    break;
            }

            //get user input and process events
            SplashKit.ProcessEvents();
            switch (_currentState) {
                case ("MainMenu"):
                    //HandleInput();
                    break;
                case ("Travelling"):
                    TravellingController.HandleInput();
                    break;
                case ("FullscreenMap"):
                    //HandleInput();
                    break;
                default:
                    break;
            }
        }
        while (!SplashKit.WindowCloseRequested(gameWindow));
    }
}
