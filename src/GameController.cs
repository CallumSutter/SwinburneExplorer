using System;
using SplashKitSDK;
using Swinburneexplorer;
using System.Resources;

public class GameController {
    public static Window gameWindow;
    public static Player _player;
    public static MainMenu _mainMenu;
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

            Point2D pos = SplashKit.MousePosition();
            gameWindow.DrawText(pos.X.ToString(), Color.Black, 10, 10);
            gameWindow.DrawText(pos.Y.ToString(), Color.Black, 10, 30);

            gameWindow.Refresh();
        }
        while (!SplashKit.WindowCloseRequested(gameWindow) && _currentState != "Exit");
    }
}
