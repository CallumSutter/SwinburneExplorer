using System;
using SplashKitSDK;
using Swinburneexplorer;
using Resources;

public class GameController
{
	public static Window gameWindow;
	public const int WINDOW_HEIGHT = 600;
	public const int WINDOW_WIDTH = 800;
	
    public static void Main(string[] args)
    {
        //Constants for directions
        const int FORWARD = 0;
        const int BACKWARD = 1;
        const int LEFT = 2;
        const int RIGHT = 3;

        //initialise varaiables
        Location location1 = new Location("location1.jpg", "1");
        Location location2 = new Location("location2.jpg", "2");
        Location location3 = new Location("placeholder.jpg", "3");

        location1.AddConnectingLocation(location2, FORWARD);
        location1.AddConnectingLocation(location3, BACKWARD);

        location2.AddConnectingLocation(location1, BACKWARD);
        location3.AddConnectingLocation(location1, FORWARD);

        Player _player = new Player(location1);

		//new game window
		gameWindow = new Window("SwinExplorer", WINDOW_WIDTH , WINDOW_HEIGHT);

		GameResources.LoadingScreen();

		do {
            //get user input
            SplashKit.ProcessEvents();


            gameWindow.Clear(Color.White);

            //draws players location
            SplashKit.DrawBitmap(_player.Location.LocationImage, 0, 0);
			//SplashKit.DrawBitmap(_player.Location.ForwardArrow, 0, 0);
			//SplashKit.DrawBitmap(_player.Location.OtherArrow, 100, 100);

			GameResources.DrawDirectionArrows();

            //target 60 fps
            gameWindow.Refresh(60);

			if (SplashKit.MouseClicked(MouseButton.LeftButton))
			{
				Console.WriteLine(GameResources.MouseInArrow().ToString());
			}

            //if proper key is pressed, change locations in a direction
            if (SplashKit.KeyTyped(KeyCode.WKey)) {
                if (_player.Location.GetLocationInDirection(FORWARD) != null) {
                    _player.Location = _player.Location.GetLocationInDirection(FORWARD);
                }
                else {
                    gameWindow.Clear(Color.White);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.SKey)) {
                if (_player.Location.GetLocationInDirection(BACKWARD) != null) {
                    _player.Location = _player.Location.GetLocationInDirection(BACKWARD);
                }
                else {
                    gameWindow.Clear(Color.White);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.AKey)) {
                if (_player.Location.GetLocationInDirection(LEFT) != null) {
                    _player.Location = _player.Location.GetLocationInDirection(LEFT);
                }
                else {
                    gameWindow.Clear(Color.White);
                }
            }

            if (SplashKit.KeyTyped(KeyCode.DKey)) {
                if (_player.Location.GetLocationInDirection(RIGHT) != null) {
                    _player.Location = _player.Location.GetLocationInDirection(RIGHT);
                }
                else {
                    gameWindow.Clear(Color.White);
                }
            }


        }
        while (!SplashKit.WindowCloseRequested(gameWindow));

    }
}
