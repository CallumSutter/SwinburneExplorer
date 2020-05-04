using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public enum ArrowDir
	{
		Up,
		Left,
		Down,
		Right
	}

	public static class GameResources
	{
		public const int ARROW_SIZE = 72;

		public const double ARROW_Y = GameController.WINDOW_HEIGHT * (3.8 / 5.0);
		public const double ARROW_X = GameController.WINDOW_WIDTH / 2.0 - ARROW_SIZE / 2.0;
		public const double ARROW_X_OFFSET = 80;
		public const double ARROW_Y_OFFSET = ARROW_SIZE / 2 + 10;

		public const double ARROW_SCALE = 0.2;

		private static Dictionary<string, Font> _fonts;
		private static Dictionary<string, SoundEffect> _sounds;
		private static Dictionary<string, Bitmap> _images;
		private static Dictionary<string, Music> _music;

		private static Font _loadingFont;

		/// <summary>
		/// Initialise resource collections
		/// </summary>
		static GameResources()
		{
			_fonts = new Dictionary<string, Font>();
			_sounds = new Dictionary<string, SoundEffect>();
			_images = new Dictionary<string, Bitmap>();
			_music = new Dictionary<string, Music>();
			_loadingFont = new Font("arial", "/arial.ttf"); //default font.
			_fonts.Add("arial", _loadingFont);
		}

		/// <summary>
		/// Play BGM
		/// </summary>
		public static void PlayBGM()
		{
			if(SplashKit.MusicPlaying())
			{
				SplashKit.StopMusic();
			}

			SplashKit.PlayMusic(GetMusic("USSR"),10,(float)0.5);
		}

		/// <summary>
		/// Display loading screen
		/// </summary>
		public static void LoadingScreen()
		{
			//place holder
			Rectangle loadbar = new Rectangle();
			loadbar.Height = 13;
			loadbar.Width = 100;
			loadbar.X = GameController.WINDOW_WIDTH / 2 - 200;
			loadbar.Y = 3 * GameController.WINDOW_HEIGHT / 4;

			uint loadBarDelay = 1000; //loadbar delay in ms
			int barNum = 1;

			SplashKit.Delay(loadBarDelay);
			DrawLoadingBar(barNum++, loadbar);
			DrawLoadingText("Loading Fonts");
			GameController.gameWindow.Refresh();
			LoadFonts();

			GameController.gameWindow.Clear(Color.White);

			SplashKit.Delay(loadBarDelay);
			DrawLoadingBar(barNum++, loadbar);
			DrawLoadingText("Loading Images");
			GameController.gameWindow.Refresh();
			LoadImages();

			GameController.gameWindow.Clear(Color.White);

			SplashKit.Delay(loadBarDelay);
			DrawLoadingBar(barNum++, loadbar);
			DrawLoadingText("Loading Music");
			GameController.gameWindow.Refresh();
			LoadMusic();

			GameController.gameWindow.Clear(Color.White);

			SplashKit.Delay(loadBarDelay);
			DrawLoadingBar(barNum++, loadbar);
			DrawLoadingText("Loading Sounds");
			GameController.gameWindow.Refresh();
			LoadSounds();

			SplashKit.Delay(loadBarDelay);

			GameController.gameWindow.Clear(Color.White);
			GameController.gameWindow.DrawBitmap(GetImage("SwinLogo"), GameController.WINDOW_WIDTH / 2 - GetImage("SwinLogo").Width / 2, GameController.WINDOW_HEIGHT / 2 - GetImage("SwinLogo").Height / 2 - 50);
			Sprite s = new Sprite(GetImage("SwinLogo"));
			GameController.gameWindow.Refresh();

			SplashKit.Delay(loadBarDelay);

			GameController.gameWindow.Refresh();
		}

		public static void DrawDirectionArrows()
		{
			Bitmap arrow = GetImage("sArrow");

			GameController.gameWindow.DrawBitmap(arrow, ARROW_X + ARROW_X_OFFSET, ARROW_Y, SplashKit.OptionRotateBmp(0));
			GameController.gameWindow.DrawBitmap(arrow, ARROW_X - ARROW_X_OFFSET, ARROW_Y, SplashKit.OptionRotateBmp(180));

			GameController.gameWindow.DrawBitmap(arrow, ARROW_X, ARROW_Y + ARROW_Y_OFFSET, SplashKit.OptionRotateBmp(90));
			GameController.gameWindow.DrawBitmap(arrow, ARROW_X, ARROW_Y - ARROW_Y_OFFSET, SplashKit.OptionRotateBmp(270));
		}

		public static ArrowDir? MouseInArrow()
		{
			Point2D mousePos = SplashKit.MousePosition();
			Rectangle arrowMask = new Rectangle();
			arrowMask.Height = ARROW_SIZE;
			arrowMask.Width = ARROW_SIZE;

			//position of left arrow
			arrowMask.X = ARROW_X - ARROW_X_OFFSET;
			arrowMask.Y = ARROW_Y;

			if (SplashKit.PointInRectangle(mousePos, arrowMask))
			{
				return ArrowDir.Left;
			}

			//position of right arrow
			arrowMask.X = ARROW_X + ARROW_X_OFFSET;
			arrowMask.Y = ARROW_Y;

			if (SplashKit.PointInRectangle(mousePos, arrowMask))
			{
				return ArrowDir.Right;
			}

			//position of up arrow
			arrowMask.X = ARROW_X;
			arrowMask.Y = ARROW_Y + ARROW_Y_OFFSET;

			if (SplashKit.PointInRectangle(mousePos, arrowMask))
			{
				return ArrowDir.Down;
			}

			//position of down arrow
			arrowMask.X = ARROW_X;
			arrowMask.Y = ARROW_Y - ARROW_Y_OFFSET;

			if (SplashKit.PointInRectangle(mousePos, arrowMask))
			{
				return ArrowDir.Up;
			}

			return null;
		}

		/// <summary>
		/// Text for loading screen
		/// </summary>
		/// <param name="loadText"></param>
		private static void DrawLoadingText(string loadText)
		{
			GameController.gameWindow.DrawText(loadText, Color.Black, "arial", 12, GameController.WINDOW_WIDTH / 2 - 195, 3 * GameController.WINDOW_HEIGHT / 4 + 15);
		}

		/// <summary>
		/// Draw in loading bar
		/// </summary>
		/// <param name="count"></param>
		/// <param name="loadBar"></param>
		private static void DrawLoadingBar(int count, Rectangle loadBar)
		{
			while(count > 0)
			{
				GameController.gameWindow.FillRectangle(Color.LimeGreen, loadBar);
				loadBar.X += loadBar.Width;
				count--;
			}
		}

		/// <summary>
		/// Release resources from system
		/// </summary>
		private static void FreeResources()
		{
			SplashKit.FreeAllBitmaps();
			SplashKit.FreeAllFonts();
			SplashKit.FreeAllMusic();
			SplashKit.FreeAllSoundEffects();
			SplashKit.ProcessEvents();
		}

		private static void LoadImages()
		{
			_images.Add("SwinLogo", new Bitmap("SwinLogo", "SwinLogo.png"));
			_images.Add("Arrow", new Bitmap("Arrow", "Arrow.png"));
			_images.Add("sArrow", new Bitmap("sArrow", "sArrow.png"));
			_images.Add("sMap", new Bitmap("sMap", "sMap.png"));
			//_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMap.png"));
			_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMapPS.png"));
			_images.Add("scroll", new Bitmap("scroll", "smallScroll.png"));
		}

		private static void LoadSounds()
		{
			_sounds.Add("incorrect", new SoundEffect("incorrect", "wrong_direction.wav"));
			_sounds.Add("correct", new SoundEffect("correct", "correct_direction.wav"));
			_sounds.Add("menuSelect", new SoundEffect("menuSelect", "menu_select.wav"));
			_sounds.Add("toggleMap", new SoundEffect("toggleMap", "openMap.wav"));
		}

		private static void LoadMusic()
		{
			_music.Add("bgm", new Music("bgm", "sogno.mp3"));
			_music.Add("USSR", new Music("USSR", "USSR.ogg"));
		}

		private static void LoadFonts()
		{
			//_fonts.Add("fontName", new Font("fontName", "Path"));
		}

		public static Font GetFont(string fontName)
		{
			try
			{
				return _fonts[fontName];
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("Cannot find font: " + fontName);
				Console.WriteLine(e.Message);
			}

			return null;
		}

		public static SoundEffect GetSound(string soundName)
		{
			try
			{
				return _sounds[soundName];
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("Cannot find sound effect: " + soundName);
				Console.WriteLine(e.Message);
			}
			
			return null;
		}

		public static Music GetMusic(string musicName)
		{
			try
			{
				return _music[musicName];
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("Cannot find music: " + musicName);
				Console.WriteLine(e.Message);
			}

			return null;
		}

		public static Bitmap GetImage(string imgName)
		{
			try
			{
				return _images[imgName];
			}
			catch (KeyNotFoundException e)
			{
				Console.WriteLine("Cannot find font: " + imgName);
				Console.WriteLine(e.Message);
			}

			return null;
		}
	}
}
