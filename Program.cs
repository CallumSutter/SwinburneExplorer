using System;
using SplashKitSDK;
using Resources;

public class Program
{
	public const int WINDOW_WIDTH = 800;
	public const int WINDOW_HEIGHT = 600;

	public const int TARGET_FPS = 60;
	public const uint DELAY = (uint)(1.0 / TARGET_FPS * 1000);

	public static Window gameWindow = new Window("Swinburne Explorer", WINDOW_WIDTH, WINDOW_HEIGHT);

    public static void Main(string[] args)
    {
		//gameWindow.Clear(Color.White);
		//gameWindow.FillEllipse(Color.BrightGreen, 0, 400, 800, 400);
		//gameWindow.FillRectangle(Color.Gray, 300, 300, 200, 200);
		//gameWindow.FillTriangle(Color.Red, 250, 300, 400, 150, 550, 300);
		//gameWindow.Refresh();

		GameResources.LoadingScreen();
		//GameResources.PlayBGM();

		while(!gameWindow.CloseRequested)
		{
			SplashKit.ProcessEvents();

			SplashKit.Delay(DELAY);

			if (SplashKit.MouseClicked(MouseButton.LeftButton))
			{
				Console.WriteLine(GameResources.MouseInArrow().ToString());
			}
		}

		gameWindow.Close();
    }
}
