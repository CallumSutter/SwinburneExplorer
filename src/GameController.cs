using System;
using SplashKitSDK;
using Swinburneexplorer;
using System.Resources;

/// <summary>
/// This class controls the entire game - holds main function
/// </summary>
public class GameController {
    /// <summary>
    /// the game window
    /// </summary>
    public static Window gameWindow;
    /// <summary>
    /// the player of the game
    /// </summary>
    public static Player _player;
    /// <summary>
    /// the main menu class - draws to screen
    /// </summary>
    public static MainMenu _mainMenu;
    /// <summary>
    /// draws to screen when in main game
    /// </summary>
    public static UI _ui;
    /// <summary>
    /// shows fullscreen map, and draws map to screen
    /// </summary>
    public static Map theMap;
    /// <summary>
    /// constant for window height
    /// </summary>
    public const int WINDOW_HEIGHT = 583;
    /// <summary>
    /// constant for window width
    /// </summary>
    public const int WINDOW_WIDTH = 1235;
    /// <summary>
    /// Constant for forward direction	
    /// </summary>
    public const int FORWARD = 0;
    /// <summary>
    /// Constant for backward direction	
    /// </summary>
    public const int BACKWARD = 1;
    /// <summary>
    /// Constant for left direction	
    /// </summary>
    public const int LEFT = 2;
    /// <summary>
    /// Constant for right direction	
    /// </summary>
    public const int RIGHT = 3;
    /// <summary>
    /// current state of the game	
    /// </summary>
    public static string _currentState;


    /// <summary>
    /// main game loop - responsible to controlling the game
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args) {
        //initialise varaiables
        //new game window
        gameWindow = new Window("SwinExplorer", WINDOW_WIDTH, WINDOW_HEIGHT);

        //Displays loading screen
        GameResources.LoadingScreen();

        //initialise Main Menu
        _mainMenu = new MainMenu();

        //initialise UI
        _ui = new UI();

        //initialse map
        theMap = new Map();

        //starting with travelling state
        _currentState = GameState.MainMenu.ToString();


        do {
            //control for drawing to screen
            gameWindow.Clear(Color.White);
            switch (_currentState) {
                case ("MainMenu"):
                    _mainMenu.Draw();
                    break;
                case ("Travelling"):
                case ("InClassroom"):
                case ("InBuilding"):
                    _ui.Draw();
                    break;
                case ("FullscreenMap"):
                    theMap.Draw();
                    break;
                default:
                    break;
            }

			Point2D pos = SplashKit.MousePosition();
			gameWindow.DrawText(pos.X.ToString(), Color.Black, 10, 10);
			gameWindow.DrawText(pos.Y.ToString(), Color.Black, 10, 30);
			gameWindow.Refresh();

            //get user input and process events
            SplashKit.ProcessEvents();
            switch (_currentState) {
                case ("MainMenu"):
                    MainMenuController.HandleInput();
                    break;
                case ("Travelling"):
                case ("InClassroom"):
                case ("InBuilding"):
                    TravellingController.HandleInput();
                    break;
                case ("FullscreenMap"):
                    MapController.HandleInput();
                    break;
                default:
                    break;
            }
        }
        while (!SplashKit.WindowCloseRequested(gameWindow) && _currentState != "Exit");
    }

	/// <summary>
	/// Getter for player
	/// </summary>
	public static Player Player {
		get {
			return _player;
		}
	}

	/// <summary>
	/// Getter for ui
	/// </summary>
	public static UI UI {
		get {
			return _ui;
		}
	}
}
