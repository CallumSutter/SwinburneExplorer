using System;
using SplashKitSDK;
using Resources;

public class Program
{
	public const int WINDOW_WIDTH = 1920;
	public const int WINDOW_HEIGHT = 1080;

	public static Window gameWindow = new Window("Swinburne Explorer", WINDOW_WIDTH, WINDOW_HEIGHT);

    public static void Main(string[] args)
    {
		gameWindow.MoveTo(0, 0);

		gameWindow.Clear(Color.White);
		gameWindow.FillEllipse(Color.BrightGreen, 0, 400, 800, 400);
		gameWindow.FillRectangle(Color.Gray, 300, 300, 200, 200);
		gameWindow.FillTriangle(Color.Red, 250, 300, 400, 150, 550, 300);
		gameWindow.Refresh();

		GameResources.LoadingScreen();
		GameResources.PlayBGM();

		Console.ReadLine();
    }
}
