using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public static class GameResources
	{
		public const int ARROW_SIZE = 72;

		public const double ARROW_Y = GameController.WINDOW_HEIGHT * (3.8 / 5.0);
		public const double ARROW_X = GameController.WINDOW_WIDTH / 2.0 - ARROW_SIZE / 2.0;
		public const double ARROW_X_OFFSET = 80;
		public const double ARROW_Y_OFFSET = ARROW_SIZE / 2 + 10;

		public const double ARROW_SCALE = 0.2;

		public const int FORWARD = 0;
		public const int BACKWARD = 1;
		public const int LEFT = 2;
		public const int RIGHT = 3;

		private static Dictionary<string, Font> _fonts;
		private static Dictionary<string, SoundEffect> _sounds;
		private static Dictionary<string, Bitmap> _images;
		private static Dictionary<string, Music> _music;
		private static Dictionary<string, Location> _locations;

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
			_locations = new Dictionary<string, Location>();
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
			LoadLocations();

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
		private static void FreeResources() {
			SplashKit.FreeAllBitmaps();
			SplashKit.FreeAllFonts();
			SplashKit.FreeAllMusic();
			SplashKit.FreeAllSoundEffects();
			SplashKit.ProcessEvents();
		}

		/// <summary>
		/// Load image and add to image dictionary
		/// </summary>
		/// <param name="locName"></param>
		public static void LoadLocationImage(string locName) {
			Stopwatch sw = new Stopwatch();
			sw.Start();
			_images.Add(locName, new Bitmap(locName, "campus/" + locName + ".jpg"));
			sw.Stop();
			Console.WriteLine($"Took {sw.ElapsedMilliseconds} to load campus/{locName}.jpg");
		}

		private static void LoadImages() {
			_images.Add("SwinLogo", new Bitmap("SwinLogo", "SwinLogo.png"));
			_images.Add("Arrow", new Bitmap("Arrow", "Arrow.png"));
			_images.Add("sArrow", new Bitmap("sArrow", "sArrow.png"));
			_images.Add("sMap", new Bitmap("sMap", "sMap.png"));
			//_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMap.png"));
			_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMapPS.png"));
			_images.Add("scroll", new Bitmap("scroll", "smallScroll.png"));
			_images.Add("btnBase", new Bitmap("btnBase", "btnBase1.png"));
			_images.Add("infoBtn", new Bitmap("infoBtn", "infoBtn.png"));

			////campus images
			//_images.Add("AD", new Bitmap("AD", "campus/AD.jpg"));
			//_images.Add("AGSE", new Bitmap("AGSE", "campus/AGSE.jpg"));
			//_images.Add("AMDC", new Bitmap("AMDC", "campus/AMDC.jpg"));
			//_images.Add("AR", new Bitmap("AR", "campus/AR.jpg"));
			//_images.Add("AS", new Bitmap("AS", "campus/AS.jpg"));
			//_images.Add("ATC", new Bitmap("ATC", "campus/ATC.jpg"));
			//_images.Add("ATCOtherSide", new Bitmap("ATCOtherSide", "campus/ATCOtherSide.jpg"));
			//_images.Add("backOfAD", new Bitmap("backOfAD", "campus/backOfAD.jpg"));
			//_images.Add("EN", new Bitmap("EN", "campus/EN.jpg"));
			//_images.Add("FS", new Bitmap("FS", "campus/FS.jpg"));
			//_images.Add("George", new Bitmap("George", "campus/George.jpg"));
			//_images.Add("grassPath1", new Bitmap("grassPath1", "campus/grassPath1.jpg"));
			//_images.Add("grassPath2", new Bitmap("grassPath2", "campus/grassPath2.jpg"));
			//_images.Add("grassPath3", new Bitmap("grassPath3", "campus/grassPath3.jpg"));
			//_images.Add("grassPath4", new Bitmap("grassPath4", "campus/grassPath4.jpg"));
			//_images.Add("grassPath5", new Bitmap("grassPath5", "campus/grassPath5.jpg"));
			//_images.Add("grassPath6", new Bitmap("grassPath6", "campus/grassPath6.jpg"));
			//_images.Add("instreet1", new Bitmap("instreet1", "campus/instreet1.jpg"));
			//_images.Add("instreet2", new Bitmap("instreet2", "campus/instreet2.jpg"));
			//_images.Add("instreet3", new Bitmap("instreet3", "campus/instreet3.jpg"));
			//_images.Add("instreet4", new Bitmap("instreet4", "campus/instreet4.jpg"));
			//_images.Add("instreet5", new Bitmap("instreet5", "campus/instreet5.jpg"));
			//_images.Add("instreet6", new Bitmap("instreet6", "campus/instreet6.jpg"));
			//_images.Add("instreet7", new Bitmap("instreet7", "campus/instreet7.jpg"));
			//_images.Add("instreet8", new Bitmap("instreet8", "campus/instreet8.jpg"));
			//_images.Add("instreet9", new Bitmap("instreet9", "campus/instreet9.jpg"));
			//_images.Add("northSide1", new Bitmap("northSide1", "campus/northSide1.jpg"));
			//_images.Add("northSide2", new Bitmap("northSide2", "campus/northSide2.jpg"));
			//_images.Add("northSide3", new Bitmap("northSide3", "campus/northSide3.jpg"));
			//_images.Add("northSide4", new Bitmap("northSide4", "campus/northSide4.jpg"));
			//_images.Add("side1", new Bitmap("side1", "campus/side1.jpg"));
			//_images.Add("side2", new Bitmap("side2", "campus/side2.jpg"));
			//_images.Add("side3", new Bitmap("side3", "campus/side3.jpg"));
			//_images.Add("sneak1", new Bitmap("sneak1", "campus/sneak1.jpg"));
			//_images.Add("sneak2", new Bitmap("sneak2", "campus/sneak2.jpg"));
			//_images.Add("sneak3", new Bitmap("sneak3", "campus/sneak3.jpg"));
			//_images.Add("southSide1", new Bitmap("southSide1", "campus/southSide1.jpg"));
			//_images.Add("southSide2", new Bitmap("southSide2", "campus/southSide2.jpg"));
			//_images.Add("southSide3", new Bitmap("southSide3", "campus/southSide3.jpg"));
			//_images.Add("southSide4", new Bitmap("southSide4", "campus/southSide4.jpg"));
			//_images.Add("southSide5", new Bitmap("southSide5", "campus/southSide5.jpg"));
			//_images.Add("southSide6", new Bitmap("southSide6", "campus/southSide6.jpg"));
			//_images.Add("spw1", new Bitmap("spw1", "campus/spw1.jpg"));
			//_images.Add("spw2", new Bitmap("spw2", "campus/spw2.jpg"));
			//_images.Add("spw3", new Bitmap("spw3", "campus/spw3.jpg"));
			//_images.Add("spw4", new Bitmap("spw4", "campus/spw4.jpg"));
			//_images.Add("spw5", new Bitmap("spw5", "campus/spw5.jpg"));
			//_images.Add("spw6", new Bitmap("spw6", "campus/spw6.jpg"));
			//_images.Add("spw7", new Bitmap("spw7", "campus/spw7.jpg"));
			//_images.Add("spw8", new Bitmap("spw8", "campus/spw8.jpg"));
			//_images.Add("spw9", new Bitmap("spw9", "campus/spw9.jpg"));
			//_images.Add("stairs", new Bitmap("stairs", "campus/stairs.jpg"));
			//_images.Add("studentCarPark", new Bitmap("studentCarPark", "campus/studentCarPark.jpg"));
			//_images.Add("study", new Bitmap("study", "campus/study.jpg"));
			//_images.Add("TA", new Bitmap("TA", "campus/TA.jpg"));
			//_images.Add("TB", new Bitmap("TB", "campus/TB.jpg"));
			//_images.Add("TC", new Bitmap("TC", "campus/TC.jpg"));
			//_images.Add("TD", new Bitmap("TD", "campus/TD.jpg"));
			//_images.Add("toAMDC1", new Bitmap("toAMDC1", "campus/toAMDC1.jpg"));
			//_images.Add("toAMDC2", new Bitmap("toAMDC2", "campus/toAMDC2.jpg"));
			//_images.Add("toAMDC3", new Bitmap("toAMDC3", "campus/toAMDC3.jpg"));
			//_images.Add("toAMDC4", new Bitmap("toAMDC4", "campus/toAMDC4.jpg"));
			//_images.Add("toAMDC5", new Bitmap("toAMDC5", "campus/toAMDC5.jpg"));
			//_images.Add("toAS1", new Bitmap("toAS1", "campus/toAS1.jpg"));
			//_images.Add("toAS2", new Bitmap("toAS2", "campus/toAS2.jpg"));
			//_images.Add("toAS3", new Bitmap("toAS3", "campus/toAS3.jpg"));
			//_images.Add("toAS4", new Bitmap("toAS4", "campus/toAS4.jpg"));
			//_images.Add("toAS5", new Bitmap("toAS5", "campus/toAS5.jpg"));
			//_images.Add("toATC1", new Bitmap("toATC1", "campus/toATC1.jpg"));
			//_images.Add("toATC2", new Bitmap("toATC2", "campus/toATC2.jpg"));
			//_images.Add("toATC3", new Bitmap("toATC3", "campus/toATC3.jpg"));
			//_images.Add("toATC4", new Bitmap("toATC4", "campus/toATC4.jpg"));
			//_images.Add("toEN1", new Bitmap("toEN1", "campus/toEN1.jpg"));
			//_images.Add("toEN2", new Bitmap("toEN2", "campus/toEN2.jpg"));
			//_images.Add("toEN3", new Bitmap("toEN3", "campus/toEN3.jpg"));
			//_images.Add("toEN4", new Bitmap("toEN4", "campus/toEN4.jpg"));
			//_images.Add("toEN5", new Bitmap("toEN5", "campus/toEN5.jpg"));
			//_images.Add("toLodges1", new Bitmap("toLodges1", "campus/toLodges1.jpg"));
			//_images.Add("toLodges2", new Bitmap("toLodges2", "campus/toLodges2.jpg"));
			//_images.Add("toLodges3", new Bitmap("toLodges3", "campus/toLodges3.jpg"));
			//_images.Add("toLodges4", new Bitmap("toLodges4", "campus/toLodges4.jpg"));
			//_images.Add("toLodges5", new Bitmap("toLodges5", "campus/toLodges5.jpg"));
			//_images.Add("toLodges6", new Bitmap("toLodges6", "campus/toLodges6.jpg"));
			//_images.Add("toSouthSide1", new Bitmap("toSouthSide1", "campus/toSouthSide1.jpg"));
			//_images.Add("toSouthSide2", new Bitmap("toSouthSide2", "campus/toSouthSide2.jpg"));
			//_images.Add("toSouthSide3", new Bitmap("toSouthSide3", "campus/toSouthSide3.jpg"));
			//_images.Add("toSouthSide4", new Bitmap("toSouthSide4", "campus/toSouthSide4.jpg"));
			//_images.Add("toTrain1", new Bitmap("toTrain1", "campus/toTrain1.jpg"));
			//_images.Add("toTrain2", new Bitmap("toTrain2", "campus/toTrain2.jpg"));
			//_images.Add("toTrain3", new Bitmap("toTrain3", "campus/toTrain3.jpg"));
			//_images.Add("toTrain4", new Bitmap("toTrain4", "campus/toTrain4.jpg"));
			//_images.Add("toTrain5", new Bitmap("toTrain5", "campus/toTrain5.jpg"));
			//_images.Add("toTrain6", new Bitmap("toTrain6", "campus/toTrain6.jpg"));
			//_images.Add("toTrain7", new Bitmap("toTrain7", "campus/toTrain7.jpg"));
			//_images.Add("toTrain8", new Bitmap("toTrain8", "campus/toTrain8.jpg"));
			//_images.Add("toTrain9", new Bitmap("toTrain9", "campus/toTrain9.jpg"));
			//_images.Add("toTrain10", new Bitmap("toTrain10", "campus/toTrain10.jpg"));
			//_images.Add("toTrain11", new Bitmap("toTrain11", "campus/toTrain11.jpg"));
			//_images.Add("toTrain12", new Bitmap("toTrain12", "campus/toTrain12.jpg"));
			//_images.Add("toWestSide1", new Bitmap("toWestSide1", "campus/toWestSide1.jpg"));
			//_images.Add("toWestSide2", new Bitmap("toWestSide2", "campus/toWestSide2.jpg"));
			//_images.Add("Train", new Bitmap("Train", "campus/Train.jpg"));
			//_images.Add("trainPark1", new Bitmap("trainPark1", "campus/trainPark1.jpg"));
			//_images.Add("trainPark2", new Bitmap("trainPark2", "campus/trainPark2.jpg"));
			//_images.Add("trainPark3", new Bitmap("trainPark3", "campus/trainPark3.jpg"));
			//_images.Add("trainPark4", new Bitmap("trainPark4", "campus/trainPark4.jpg"));
			//_images.Add("trainPark5", new Bitmap("trainPark5", "campus/trainPark5.jpg"));
			//_images.Add("trainPark6", new Bitmap("trainPark6", "campus/trainPark6.jpg"));
			//_images.Add("tunnel1", new Bitmap("tunnel1", "campus/tunnel1.jpg"));
			//_images.Add("tunnel2", new Bitmap("tunnel2", "campus/tunnel2.jpg"));
			//_images.Add("tunnel3", new Bitmap("tunnel3", "campus/tunnel3.jpg"));
			//_images.Add("tunnel4", new Bitmap("tunnel4", "campus/tunnel4.jpg"));
			//_images.Add("westSide1", new Bitmap("westSide1", "campus/westSide1.jpg"));
			//_images.Add("westSide2", new Bitmap("westSide2", "campus/westSide2.jpg"));
			//_images.Add("westSide3", new Bitmap("westSide3", "campus/westSide3.jpg"));
			//_images.Add("westSide4", new Bitmap("westSide4", "campus/westSide4.jpg"));
			//_images.Add("westSide5", new Bitmap("westSide5", "campus/westSide5.jpg"));
			//_images.Add("westSide6", new Bitmap("westSide6", "campus/westSide6.jpg"));
			//_images.Add("westSide7", new Bitmap("westSide7", "campus/westSide7.jpg"));
			//_images.Add("westSide8", new Bitmap("westSide8", "campus/westSide8.jpg"));
			//_images.Add("westSide9", new Bitmap("westSide9", "campus/westSide9.jpg"));
			//_images.Add("westSide10", new Bitmap("westSide10", "campus/westSide10.jpg"));
			//_images.Add("westSide11", new Bitmap("westSide11", "campus/westSide11.jpg"));
			//_images.Add("westSide12", new Bitmap("westSide12", "campus/westSide12.jpg"));
			//_images.Add("westSide13", new Bitmap("westSide13", "campus/westSide13.jpg"));
		}

		private static void LoadLocations() {
			_locations.Add("AD Building", new Location("AD Building")) ;
			_locations.Add("AGSE Building", new Location("AGSE Building"));
			_locations.Add("AMDC Building", new Location("AMDC Building"));
			_locations.Add("AR Building", new Location("AR Building"));
			_locations.Add("AS Building", new Location("AS Building"));
			_locations.Add("ATC Building", new Location("ATC Building"));
			_locations.Add("ATC Building (Back)", new Location("ATC Bulding (Back)"));
			_locations.Add("backOfAD", new Location("backOfAD"));
			_locations.Add("EN Building", new Location("EN Building"));
			_locations.Add("FS Building", new Location("FS Building"));
			_locations.Add("George Building", new Location("George Building"));
			_locations.Add("grassPath1", new Location("grassPath1"));
			_locations.Add("grassPath2", new Location("grassPath2"));
			_locations.Add("grassPath3", new Location("grassPath3"));
			_locations.Add("grassPath4", new Location("grassPath4"));
			_locations.Add("grassPath5", new Location("grassPath5"));
			_locations.Add("grassPath6", new Location("grassPath6"));
			_locations.Add("instreet1", new Location("instreet1"));
			_locations.Add("instreet2", new Location("instreet2"));
			_locations.Add("instreet3", new Location("instreet3"));
			_locations.Add("instreet4", new Location("instreet4"));
			_locations.Add("instreet5", new Location("instreet5"));
			_locations.Add("instreet6", new Location("instreet6"));
			_locations.Add("instreet7", new Location("instreet7"));
			_locations.Add("instreet8", new Location("instreet8"));
			_locations.Add("instreet9", new Location("instreet9"));
			_locations.Add("northSide1", new Location("northSide1"));
			_locations.Add("northSide2", new Location("northSide2"));
			_locations.Add("northSide3", new Location("northSide3"));
			_locations.Add("northSide4", new Location("northSide4"));
			_locations.Add("side1", new Location("side1"));
			_locations.Add("side2", new Location("side2"));
			_locations.Add("side3", new Location("side3"));
			_locations.Add("sneak1", new Location("sneak1"));
			_locations.Add("sneak2", new Location("sneak2"));
			_locations.Add("sneak3", new Location("sneak3"));
			_locations.Add("southSide1", new Location("southSide1"));
			_locations.Add("southSide2", new Location("southSide2"));
			_locations.Add("southSide3", new Location("southSide3"));
			_locations.Add("southSide4", new Location("southSide4"));
			_locations.Add("southSide5", new Location("southSide5"));
			_locations.Add("southSide6", new Location("southSide6"));
			_locations.Add("spw1", new Location("spw1"));
			_locations.Add("spw2", new Location("spw2"));
			_locations.Add("spw3", new Location("spw3"));
			_locations.Add("spw4", new Location("spw4"));
			_locations.Add("spw5", new Location("spw5"));
			_locations.Add("spw6", new Location("spw6"));
			_locations.Add("spw7", new Location("spw7"));
			_locations.Add("spw8", new Location("spw8"));
			_locations.Add("spw9", new Location("spw9"));
			_locations.Add("stairs", new Location("stairs"));
			_locations.Add("studentCarPark", new Location("studentCarPark"));
			_locations.Add("study", new Location("study"));
			_locations.Add("TA Building", new Location("TA Building"));
			_locations.Add("TB Building", new Location("TB Building"));
			_locations.Add("TC Building", new Location("TC Building"));
			_locations.Add("TD Building", new Location("TD Building"));
			_locations.Add("toAMDC1", new Location("toAMDC1"));
			_locations.Add("toAMDC2", new Location("toAMDC2"));
			_locations.Add("toAMDC3", new Location("toAMDC3"));
			_locations.Add("toAMDC4", new Location("toAMDC4"));
			_locations.Add("toAMDC5", new Location("toAMDC5"));
			_locations.Add("toAS1", new Location("toAS1"));
			_locations.Add("toAS2", new Location("toAS2"));
			_locations.Add("toAS3", new Location("toAS3"));
			_locations.Add("toAS4", new Location("toAS4"));
			_locations.Add("toAS5", new Location("toAS5"));
			_locations.Add("toATC1", new Location("toATC1"));
			_locations.Add("toATC2", new Location("toATC2"));
			_locations.Add("toATC3", new Location("toATC3"));
			_locations.Add("toATC4", new Location("toATC4"));
			_locations.Add("toEN1", new Location("toEN1"));
			_locations.Add("toEN2", new Location("toEN2"));
			_locations.Add("toEN3", new Location("toEN3"));
			_locations.Add("toEN4", new Location("toEN4"));
			_locations.Add("toEN5", new Location("toEN5"));
			_locations.Add("toLodges1", new Location("toLodges1"));
			_locations.Add("toLodges2", new Location("toLodges2"));
			_locations.Add("toLodges3", new Location("toLodges3"));
			_locations.Add("toLodges4", new Location("toLodges4"));
			_locations.Add("toLodges5", new Location("toLodges5"));
			_locations.Add("toLodges6", new Location("toLodges6"));
			_locations.Add("toSouthSide1", new Location("toSouthSide1"));
			_locations.Add("toSouthSide2", new Location("toSouthSide2"));
			_locations.Add("toSouthSide3", new Location("toSouthSide3"));
			_locations.Add("toSouthSide4", new Location("toSouthSide4"));
			_locations.Add("toTrain1", new Location("toTrain1"));
			_locations.Add("toTrain2", new Location("toTrain2"));
			_locations.Add("toTrain3", new Location("toTrain3"));
			_locations.Add("toTrain4", new Location("toTrain4"));
			_locations.Add("toTrain5", new Location("toTrain5"));
			_locations.Add("toTrain6", new Location("toTrain6"));
			_locations.Add("toTrain7", new Location("toTrain7"));
			_locations.Add("toTrain8", new Location("toTrain8"));
			_locations.Add("toTrain9", new Location("toTrain9"));
			_locations.Add("toTrain10", new Location("toTrain10"));
			_locations.Add("toTrain11", new Location("toTrain11"));
			_locations.Add("toTrain12", new Location("toTrain12"));
			_locations.Add("toWestSide1", new Location("toWestSide1"));
			_locations.Add("toWestSide2", new Location("toWestSide2"));
			_locations.Add("Train", new Location("Train"));
			_locations.Add("trainPark1", new Location("trainPark1"));
			_locations.Add("trainPark2", new Location("trainPark2"));
			_locations.Add("trainPark3", new Location("trainPark3"));
			_locations.Add("trainPark4", new Location("trainPark4"));
			_locations.Add("trainPark5", new Location("trainPark5"));
			_locations.Add("trainPark6", new Location("trainPark6"));
			_locations.Add("tunnel1", new Location("tunnel1"));
			_locations.Add("tunnel2", new Location("tunnel2"));
			_locations.Add("tunnel3", new Location("tunnel3"));
			_locations.Add("tunnel4", new Location("tunnel4"));
			_locations.Add("westSide1", new Location("westSide1"));
			_locations.Add("westSide2", new Location("westSide2"));
			_locations.Add("westSide3", new Location("westSide3"));
			_locations.Add("westSide4", new Location("westSide4"));
			_locations.Add("westSide5", new Location("westSide5"));
			_locations.Add("westSide6", new Location("westSide6"));
			_locations.Add("westSide7", new Location("westSide7"));
			_locations.Add("westSide8", new Location("westSide8"));
			_locations.Add("westSide9", new Location("westSide9"));
			_locations.Add("westSide10", new Location("westSide10"));
			_locations.Add("westSide11", new Location("westSide11"));
			_locations.Add("westSide12", new Location("westSide12"));
			_locations.Add("westSide13", new Location("westSide13"));

			ConfigureLocations();
		}

		private static void ConfigureLocations() {
			//AGSE
			getLocation("AGSE Building").AddConnectingLocation(getLocation("tunnel2"), BACKWARD);

			//toTrain1
			getLocation("toTrain1").AddConnectingLocation(getLocation("toTrain2"), FORWARD);
			getLocation("toTrain1").AddConnectingLocation(getLocation("toAMDC1"), BACKWARD);

			//toTrain2
			getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain1"), BACKWARD);

			//toTrain3
			getLocation("toTrain3").AddConnectingLocation(getLocation("toTrain4"), FORWARD);
			getLocation("toTrain3").AddConnectingLocation(getLocation("toTrain2"), BACKWARD);
			getLocation("toTrain3").AddConnectingLocation(getLocation("toEN1"), LEFT);

			//toTrain4
			getLocation("toTrain4").AddConnectingLocation(getLocation("toTrain5"), FORWARD);
			getLocation("toTrain4").AddConnectingLocation(getLocation("toTrain3"), BACKWARD);

			//toTrain5
			getLocation("toTrain5").AddConnectingLocation(getLocation("toTrain6"), FORWARD);
			getLocation("toTrain5").AddConnectingLocation(getLocation("toTrain4"), BACKWARD);
			getLocation("toTrain5").AddConnectingLocation(getLocation("AR Building"), LEFT);

			//toTrain6
			getLocation("toTrain6").AddConnectingLocation(getLocation("toTrain7"), FORWARD);
			getLocation("toTrain6").AddConnectingLocation(getLocation("toTrain5"), BACKWARD);
			getLocation("toTrain6").AddConnectingLocation(getLocation("trainPark6"), LEFT);

			//toTrain7
			getLocation("toTrain7").AddConnectingLocation(getLocation("toTrain8"), RIGHT);
			getLocation("toTrain7").AddConnectingLocation(getLocation("toTrain6"), BACKWARD);

			//toTrain8
			getLocation("toTrain8").AddConnectingLocation(getLocation("toTrain7"), LEFT);
			getLocation("toTrain8").AddConnectingLocation(getLocation("Train"), RIGHT);
			getLocation("toTrain8").AddConnectingLocation(getLocation("toTrain9"), BACKWARD);

			//toTrain9
			getLocation("toTrain9").AddConnectingLocation(getLocation("toTrain8"), FORWARD);
			getLocation("toTrain9").AddConnectingLocation(getLocation("toTrain10"), BACKWARD);
            getLocation("toTrain9").AddConnectingLocation(getLocation("toTrain10"), RIGHT);
            getLocation("toTrain9").AddConnectingLocation(getLocation("toTrain11"), LEFT);

            //toTrain10
            getLocation("toTrain10").AddConnectingLocation(getLocation("toTrain11"), FORWARD);
			getLocation("toTrain10").AddConnectingLocation(getLocation("toTrain9"), RIGHT);

			//toTrain11
			getLocation("toTrain11").AddConnectingLocation(getLocation("toTrain12"), FORWARD);
			getLocation("toTrain11").AddConnectingLocation(getLocation("toTrain10"), BACKWARD);

			//toTrain12
			getLocation("toTrain12").AddConnectingLocation(getLocation("spw4"), FORWARD);
			getLocation("toTrain12").AddConnectingLocation(getLocation("toTrain11"), BACKWARD);

			//Train
			getLocation("Train").AddConnectingLocation(getLocation("toTrain8"), LEFT);
            getLocation("Train").AddConnectingLocation(getLocation("toTrain9"), RIGHT);

            //spw1
            getLocation("spw1").AddConnectingLocation(getLocation("spw2"), BACKWARD);

			//spw2
			getLocation("spw2").AddConnectingLocation(getLocation("spw1"), RIGHT);
			getLocation("spw2").AddConnectingLocation(getLocation("instreet9"), LEFT);

			//spw3
			getLocation("spw3").AddConnectingLocation(getLocation("spw4"), FORWARD);
			getLocation("spw3").AddConnectingLocation(getLocation("spw2"), BACKWARD);

			//spw4
			getLocation("spw4").AddConnectingLocation(getLocation("spw3"), LEFT);
			getLocation("spw4").AddConnectingLocation(getLocation("spw5"), FORWARD);
			getLocation("spw4").AddConnectingLocation(getLocation("toTrain12"), BACKWARD);

			//spw5
			getLocation("spw5").AddConnectingLocation(getLocation("spw6"), FORWARD);
			getLocation("spw5").AddConnectingLocation(getLocation("spw4"), BACKWARD);

			//spw6
			getLocation("spw6").AddConnectingLocation(getLocation("spw7"), FORWARD);
			getLocation("spw6").AddConnectingLocation(getLocation("spw5"), BACKWARD);

			//spw7
			getLocation("spw7").AddConnectingLocation(getLocation("grassPath1"), FORWARD);
			getLocation("spw7").AddConnectingLocation(getLocation("instreet8"), LEFT);
			getLocation("spw7").AddConnectingLocation(getLocation("spw6"), BACKWARD);

			//spw8
			getLocation("spw8").AddConnectingLocation(getLocation("spw7"), FORWARD);
			getLocation("spw8").AddConnectingLocation(getLocation("spw9"), BACKWARD);

			//spw9
			getLocation("spw9").AddConnectingLocation(getLocation("spw8"), FORWARD);
			getLocation("spw9").AddConnectingLocation(getLocation("instreet8"), BACKWARD);

			//grassPath1
			getLocation("grassPath1").AddConnectingLocation(getLocation("grassPath2"), FORWARD);
			getLocation("grassPath1").AddConnectingLocation(getLocation("spw7"), BACKWARD);

			//grassPath2
			getLocation("grassPath2").AddConnectingLocation(getLocation("grassPath3"), FORWARD);
			getLocation("grassPath2").AddConnectingLocation(getLocation("grassPath1"), BACKWARD);

			//grassPath3
			getLocation("grassPath3").AddConnectingLocation(getLocation("grassPath4"), FORWARD);
			getLocation("grassPath3").AddConnectingLocation(getLocation("grassPath2"), BACKWARD);

			//grassPath4
			getLocation("grassPath4").AddConnectingLocation(getLocation("grassPath5"), FORWARD);
			getLocation("grassPath4").AddConnectingLocation(getLocation("grassPath3"), BACKWARD);

			//grassPath5
			getLocation("grassPath5").AddConnectingLocation(getLocation("grassPath6"), FORWARD);
			getLocation("grassPath5").AddConnectingLocation(getLocation("grassPath4"), BACKWARD);

			//grassPath6
			getLocation("grassPath6").AddConnectingLocation(getLocation("tunnel3"), LEFT);
			getLocation("grassPath6").AddConnectingLocation(getLocation("tunnel4"), RIGHT);
			getLocation("grassPath6").AddConnectingLocation(getLocation("grassPath5"), BACKWARD);

			//tunnel1
			getLocation("tunnel1").AddConnectingLocation(getLocation("tunnel2"), FORWARD);
			getLocation("tunnel1").AddConnectingLocation(getLocation("toSouthSide1"), BACKWARD);
            getLocation("tunnel1").AddConnectingLocation(getLocation("instreet2"), RIGHT);
            getLocation("tunnel1").AddConnectingLocation(getLocation("instreet1"), LEFT);

            //tunnel2
            getLocation("tunnel2").AddConnectingLocation(getLocation("tunnel3"), FORWARD);
			getLocation("tunnel2").AddConnectingLocation(getLocation("AGSE Building"), LEFT);
			getLocation("tunnel2").AddConnectingLocation(getLocation("tunnel1"), BACKWARD);

			//tunnel3
			getLocation("tunnel3").AddConnectingLocation(getLocation("tunnel4"), FORWARD);
			getLocation("tunnel3").AddConnectingLocation(getLocation("tunnel2"), BACKWARD);
			getLocation("tunnel3").AddConnectingLocation(getLocation("grassPath6"), RIGHT);
			getLocation("tunnel3").AddConnectingLocation(getLocation("side1"), LEFT);

			//tunnel4
			getLocation("tunnel4").AddConnectingLocation(getLocation("toAMDC1"), FORWARD);
			getLocation("tunnel4").AddConnectingLocation(getLocation("tunnel3"), BACKWARD);
			getLocation("tunnel4").AddConnectingLocation(getLocation("grassPath6"), RIGHT);

			//toAMDC1
			getLocation("toAMDC1").AddConnectingLocation(getLocation("toAMDC2"), FORWARD);
			getLocation("toAMDC1").AddConnectingLocation(getLocation("tunnel4"), BACKWARD);
			getLocation("toAMDC1").AddConnectingLocation(getLocation("toTrain1"), RIGHT);
			getLocation("toAMDC1").AddConnectingLocation(getLocation("sneak1"), LEFT);

			//toAMDC2
			getLocation("toAMDC2").AddConnectingLocation(getLocation("toAMDC3"), FORWARD);
			getLocation("toAMDC2").AddConnectingLocation(getLocation("toAMDC1"), BACKWARD);
			getLocation("toAMDC2").AddConnectingLocation(getLocation("AD Building"), RIGHT);

			//toAMDC3
			getLocation("toAMDC3").AddConnectingLocation(getLocation("toAMDC4"), FORWARD);
			getLocation("toAMDC3").AddConnectingLocation(getLocation("toAMDC2"), BACKWARD);
			getLocation("toAMDC3").AddConnectingLocation(getLocation("study"), LEFT);
			getLocation("toAMDC3").AddConnectingLocation(getLocation("toATC1"), RIGHT);

			//toAMDC4
			getLocation("toAMDC4").AddConnectingLocation(getLocation("toAMDC5"), FORWARD);
			getLocation("toAMDC4").AddConnectingLocation(getLocation("toAMDC3"), BACKWARD);
			getLocation("toAMDC4").AddConnectingLocation(getLocation("toWestSide1"), RIGHT);

			//toAMDC5
			getLocation("toAMDC5").AddConnectingLocation(getLocation("northSide3"), FORWARD);
			getLocation("toAMDC5").AddConnectingLocation(getLocation("toAMDC4"), BACKWARD);
			getLocation("toAMDC5").AddConnectingLocation(getLocation("AMDC Building"), LEFT);

			//AMDC
			getLocation("AMDC Building").AddConnectingLocation(getLocation("toWestSide1"), LEFT);
			getLocation("AMDC Building").AddConnectingLocation(getLocation("toAMDC4"), BACKWARD);
			getLocation("AMDC Building").AddConnectingLocation(getLocation("toAMDC5"), RIGHT);

			//study
			getLocation("study").AddConnectingLocation(getLocation("toAMDC3"), BACKWARD);
            getLocation("study").AddConnectingLocation(getLocation("toAMDC4"), RIGHT);
            getLocation("study").AddConnectingLocation(getLocation("toAMDC2"), LEFT);

            //toWestSide1
            getLocation("toWestSide1").AddConnectingLocation(getLocation("FS Building"), FORWARD);
			getLocation("toWestSide1").AddConnectingLocation(getLocation("toAMDC4"), BACKWARD);
			getLocation("toWestSide1").AddConnectingLocation(getLocation("AMDC Building"), RIGHT);

			//toWestSide2
			getLocation("toWestSide2").AddConnectingLocation(getLocation("westSide1"), LEFT);
			getLocation("toWestSide2").AddConnectingLocation(getLocation("FS Building"), BACKWARD);

			//FS
			getLocation("FS Building").AddConnectingLocation(getLocation("toWestSide2"), FORWARD);
			getLocation("FS Building").AddConnectingLocation(getLocation("toWestSide1"), BACKWARD);

			//westSide1
			getLocation("westSide1").AddConnectingLocation(getLocation("toWestSide2"), RIGHT);
			getLocation("westSide1").AddConnectingLocation(getLocation("westSide2"), BACKWARD);

			//westSide2
			getLocation("westSide2").AddConnectingLocation(getLocation("westSide1"), FORWARD);
			getLocation("westSide2").AddConnectingLocation(getLocation("stairs"), BACKWARD);

			//stairs
			getLocation("stairs").AddConnectingLocation(getLocation("westSide2"), LEFT);
			getLocation("stairs").AddConnectingLocation(getLocation("westSide3"), RIGHT);

			//westSide3
			getLocation("westSide3").AddConnectingLocation(getLocation("stairs"), FORWARD);
			getLocation("westSide3").AddConnectingLocation(getLocation("westSide4"), BACKWARD);

			//westSide4
			getLocation("westSide4").AddConnectingLocation(getLocation("sneak3"), RIGHT);
			getLocation("westSide4").AddConnectingLocation(getLocation("westSide5"), BACKWARD);
			getLocation("westSide4").AddConnectingLocation(getLocation("westSide3"), FORWARD);

			//westSide5
			getLocation("westSide5").AddConnectingLocation(getLocation("westSide4"), FORWARD);
			getLocation("westSide5").AddConnectingLocation(getLocation("westSide6"), BACKWARD);

			//westSide6
			getLocation("westSide6").AddConnectingLocation(getLocation("side3"), RIGHT);
			getLocation("westSide6").AddConnectingLocation(getLocation("westSide7"), BACKWARD);
			getLocation("westSide6").AddConnectingLocation(getLocation("westSide5"), FORWARD);

			//westSide7
			getLocation("westSide7").AddConnectingLocation(getLocation("westSide6"), FORWARD);
			getLocation("westSide7").AddConnectingLocation(getLocation("westSide8"), BACKWARD);

			//westSide8
			getLocation("westSide8").AddConnectingLocation(getLocation("westSide9"), RIGHT);
			getLocation("westSide8").AddConnectingLocation(getLocation("westSide7"), FORWARD);

			//westSide9
			getLocation("westSide9").AddConnectingLocation(getLocation("westSide8"), RIGHT);
			getLocation("westSide9").AddConnectingLocation(getLocation("westSide11"), LEFT);
			getLocation("westSide9").AddConnectingLocation(getLocation("westSide10"), BACKWARD);

			//westSide10
			getLocation("westSide10").AddConnectingLocation(getLocation("westSide9"), FORWARD);
			getLocation("westSide10").AddConnectingLocation(getLocation("instreet1"), BACKWARD);

			//westSide11
			getLocation("westSide11").AddConnectingLocation(getLocation("westSide12"), FORWARD);
			getLocation("westSide11").AddConnectingLocation(getLocation("westSide9"), BACKWARD);

			//westSide12
			getLocation("westSide12").AddConnectingLocation(getLocation("westSide13"), FORWARD);
			getLocation("westSide12").AddConnectingLocation(getLocation("westSide11"), BACKWARD);

			//westSide13
			getLocation("westSide13").AddConnectingLocation(getLocation("southSide6"), LEFT);
			getLocation("westSide13").AddConnectingLocation(getLocation("westSide12"), BACKWARD);

			//sneak1
			getLocation("sneak1").AddConnectingLocation(getLocation("toAMDC1"), RIGHT);
			getLocation("sneak1").AddConnectingLocation(getLocation("sneak2"), BACKWARD);

			//sneak2
			getLocation("sneak2").AddConnectingLocation(getLocation("sneak1"), FORWARD);
			getLocation("sneak2").AddConnectingLocation(getLocation("sneak3"), BACKWARD);

			//sneak3
			getLocation("sneak3").AddConnectingLocation(getLocation("sneak2"), FORWARD);
			getLocation("sneak3").AddConnectingLocation(getLocation("westSide4"), BACKWARD);

			//southSide1
			getLocation("southSide1").AddConnectingLocation(getLocation("toLodges1"), LEFT);
			getLocation("southSide1").AddConnectingLocation(getLocation("southSide2"), BACKWARD);

			//southSide2
			getLocation("southSide2").AddConnectingLocation(getLocation("southSide1"), FORWARD);
			getLocation("southSide2").AddConnectingLocation(getLocation("TC Building"), LEFT);
			getLocation("southSide2").AddConnectingLocation(getLocation("southSide3"), BACKWARD);

			//southSide3
			getLocation("southSide3").AddConnectingLocation(getLocation("southSide2"), FORWARD);
			getLocation("southSide3").AddConnectingLocation(getLocation("toSouthSide4"), LEFT);
			getLocation("southSide3").AddConnectingLocation(getLocation("southSide4"), BACKWARD);

			//southSide4
			getLocation("southSide4").AddConnectingLocation(getLocation("southSide3"), FORWARD);
			getLocation("southSide4").AddConnectingLocation(getLocation("toSouthSide4"), LEFT);
			getLocation("southSide4").AddConnectingLocation(getLocation("southSide5"), BACKWARD);

			//southSide5
			getLocation("southSide5").AddConnectingLocation(getLocation("southSide4"), FORWARD);
			getLocation("southSide5").AddConnectingLocation(getLocation("southSide6"), BACKWARD);

			//southSide6
			getLocation("southSide6").AddConnectingLocation(getLocation("southSide5"), FORWARD);
			getLocation("southSide6").AddConnectingLocation(getLocation("westSide13"), BACKWARD);

			//TA
			getLocation("TA Building").AddConnectingLocation(getLocation("toSouthSide1"), LEFT);

			//TB
			getLocation("TB Building").AddConnectingLocation(getLocation("toSouthSide1"), RIGHT);

			//TC
			getLocation("TC Building").AddConnectingLocation(getLocation("southSide2"), BACKWARD);

			//TD
			getLocation("TD Building").AddConnectingLocation(getLocation("toLodges3"), BACKWARD);

			//toSouthSide1
			getLocation("toSouthSide1").AddConnectingLocation(getLocation("toSouthSide2"), FORWARD);
			getLocation("toSouthSide1").AddConnectingLocation(getLocation("instreet2"), BACKWARD);
			getLocation("toSouthSide1").AddConnectingLocation(getLocation("TA Building"), LEFT);
			getLocation("toSouthSide1").AddConnectingLocation(getLocation("TB Building"), RIGHT);

			//toSouthSide2
			getLocation("toSouthSide2").AddConnectingLocation(getLocation("toSouthSide3"), FORWARD);
			getLocation("toSouthSide2").AddConnectingLocation(getLocation("toSouthSide1"), BACKWARD);

			//toSouthSide3
			getLocation("toSouthSide3").AddConnectingLocation(getLocation("toSouthSide4"), FORWARD);
			getLocation("toSouthSide3").AddConnectingLocation(getLocation("toSouthSide2"), BACKWARD);

			//toSouthSide4
			getLocation("toSouthSide4").AddConnectingLocation(getLocation("southSide3"), FORWARD);
			getLocation("toSouthSide4").AddConnectingLocation(getLocation("toSouthSide3"), BACKWARD);

			//toLodges1
			getLocation("toLodges1").AddConnectingLocation(getLocation("toLodges2"), FORWARD);
			getLocation("toLodges1").AddConnectingLocation(getLocation("southSide1"), BACKWARD);

			//toLodges2
			getLocation("toLodges2").AddConnectingLocation(getLocation("toLodges3"), FORWARD);
			getLocation("toLodges2").AddConnectingLocation(getLocation("toLodges1"), BACKWARD);

			//toLodges3
			getLocation("toLodges3").AddConnectingLocation(getLocation("toLodges4"), FORWARD);
			getLocation("toLodges3").AddConnectingLocation(getLocation("toLodges2"), BACKWARD);
			getLocation("toLodges3").AddConnectingLocation(getLocation("TD Building"), LEFT);

			//toLodges4
			getLocation("toLodges4").AddConnectingLocation(getLocation("toLodges5"), FORWARD);
			getLocation("toLodges4").AddConnectingLocation(getLocation("toLodges3"), BACKWARD);

			//toLodges5
			getLocation("toLodges5").AddConnectingLocation(getLocation("toLodges6"), FORWARD);
			getLocation("toLodges5").AddConnectingLocation(getLocation("toLodges4"), BACKWARD);

			//toLodges6
			getLocation("toLodges6").AddConnectingLocation(getLocation("instreet7"), FORWARD);
			getLocation("toLodges6").AddConnectingLocation(getLocation("toLodges5"), BACKWARD);

			//instreet1
			getLocation("instreet1").AddConnectingLocation(getLocation("westSide10"), FORWARD);
			getLocation("instreet1").AddConnectingLocation(getLocation("instreet2"), BACKWARD);

			//instreet2
			getLocation("instreet2").AddConnectingLocation(getLocation("instreet1"), FORWARD);
			getLocation("instreet2").AddConnectingLocation(getLocation("toSouthSide1"), LEFT);
			getLocation("instreet2").AddConnectingLocation(getLocation("tunnel1"), RIGHT);
			getLocation("instreet2").AddConnectingLocation(getLocation("instreet3"), BACKWARD);

			//instreet3
			getLocation("instreet3").AddConnectingLocation(getLocation("instreet2"), FORWARD);
			getLocation("instreet3").AddConnectingLocation(getLocation("George Building"), BACKWARD);

			//George
			getLocation("George Building").AddConnectingLocation(getLocation("instreet3"), FORWARD);
			getLocation("George Building").AddConnectingLocation(getLocation("instreet4"), BACKWARD);

			//instreet4
			getLocation("instreet4").AddConnectingLocation(getLocation("George Building"), FORWARD);
			getLocation("instreet4").AddConnectingLocation(getLocation("instreet5"), BACKWARD);

			//instreet5
			getLocation("instreet5").AddConnectingLocation(getLocation("instreet4"), FORWARD);
			getLocation("instreet5").AddConnectingLocation(getLocation("instreet6"), BACKWARD);

			//instreet6
			getLocation("instreet6").AddConnectingLocation(getLocation("instreet5"), FORWARD);
			getLocation("instreet6").AddConnectingLocation(getLocation("instreet7"), BACKWARD);

			//instreet7
			getLocation("instreet7").AddConnectingLocation(getLocation("instreet6"), FORWARD);
			getLocation("instreet7").AddConnectingLocation(getLocation("toLodges6"), LEFT);
			getLocation("instreet7").AddConnectingLocation(getLocation("instreet8"), BACKWARD);

			//instreet8
			getLocation("instreet8").AddConnectingLocation(getLocation("instreet7"), FORWARD);
			getLocation("instreet8").AddConnectingLocation(getLocation("spw9"), RIGHT);
			getLocation("instreet8").AddConnectingLocation(getLocation("instreet9"), BACKWARD);

			//instreet9
			getLocation("instreet9").AddConnectingLocation(getLocation("instreet8"), FORWARD);
			getLocation("instreet9").AddConnectingLocation(getLocation("spw2"), BACKWARD);
			getLocation("instreet9").AddConnectingLocation(getLocation("studentCarPark"), LEFT);

			//studentCarPark
			getLocation("studentCarPark").AddConnectingLocation(getLocation("instreet9"), BACKWARD);

			//AD
			getLocation("AD Building").AddConnectingLocation(getLocation("toAMDC2"), BACKWARD);

			//backOfAD
			getLocation("backOfAD").AddConnectingLocation(getLocation("toEN2"), BACKWARD);

			//toEN1
			getLocation("toEN1").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			getLocation("toEN1").AddConnectingLocation(getLocation("toEN2"), BACKWARD);

			//toEN2
			getLocation("toEN2").AddConnectingLocation(getLocation("toEN3"), FORWARD);
			getLocation("toEN2").AddConnectingLocation(getLocation("backOfAD"), LEFT);
			getLocation("toEN2").AddConnectingLocation(getLocation("toEN1"), BACKWARD);

			//toEN3
			getLocation("toEN3").AddConnectingLocation(getLocation("toEN4"), FORWARD);
			getLocation("toEN3").AddConnectingLocation(getLocation("toEN2"), BACKWARD);

			//toEN4
			getLocation("toEN4").AddConnectingLocation(getLocation("EN Building"), FORWARD);
			getLocation("toEN4").AddConnectingLocation(getLocation("toATC4"), LEFT);
			getLocation("toEN4").AddConnectingLocation(getLocation("toEN3"), BACKWARD);

			//toEN5
			getLocation("toEN5").AddConnectingLocation(getLocation("toEN4"), FORWARD);
			getLocation("toEN5").AddConnectingLocation(getLocation("toATC4"), LEFT);

			//AR
			getLocation("AR Building").AddConnectingLocation(getLocation("toTrain5"), BACKWARD);

			//toATC1
			getLocation("toATC1").AddConnectingLocation(getLocation("toAMDC3"), FORWARD);
			getLocation("toATC1").AddConnectingLocation(getLocation("toATC2"), BACKWARD);

			//toATC2
			getLocation("toATC2").AddConnectingLocation(getLocation("toATC1"), FORWARD);
			getLocation("toATC2").AddConnectingLocation(getLocation("toATC4"), BACKWARD);
			getLocation("toATC2").AddConnectingLocation(getLocation("toATC3"), RIGHT);

			//toATC3
			getLocation("toATC3").AddConnectingLocation(getLocation("ATC Building"), FORWARD);
			getLocation("toATC3").AddConnectingLocation(getLocation("toATC2"), BACKWARD);

			//toATC4
			getLocation("toATC4").AddConnectingLocation(getLocation("toATC2"), FORWARD);
			getLocation("toATC4").AddConnectingLocation(getLocation("toEN4"), LEFT);
			getLocation("toATC4").AddConnectingLocation(getLocation("toATC1"), RIGHT);

			//ATC
			getLocation("ATC Building").AddConnectingLocation(getLocation("toATC3"), BACKWARD);

			//toAS1
			getLocation("toAS1").AddConnectingLocation(getLocation("toEN5"), FORWARD);
			getLocation("toAS1").AddConnectingLocation(getLocation("toAS2"), BACKWARD);

			//toAS2
			getLocation("toAS2").AddConnectingLocation(getLocation("toAS1"), FORWARD);
			getLocation("toAS2").AddConnectingLocation(getLocation("toAS3"), BACKWARD);

			//toAS3
			getLocation("toAS3").AddConnectingLocation(getLocation("toAS2"), FORWARD);
			getLocation("toAS3").AddConnectingLocation(getLocation("AS Building"), BACKWARD);

			//toAS4
			getLocation("toAS4").AddConnectingLocation(getLocation("AS Building"), FORWARD);
			getLocation("toAS4").AddConnectingLocation(getLocation("toAS5"), BACKWARD);

			//toAS5
			getLocation("toAS5").AddConnectingLocation(getLocation("toAS4"), FORWARD);
			getLocation("toAS5").AddConnectingLocation(getLocation("northSide1"), RIGHT);

			//AS
			getLocation("AS Building").AddConnectingLocation(getLocation("toAS3"), LEFT);
			getLocation("AS Building").AddConnectingLocation(getLocation("toAS4"), BACKWARD);

			//ATCOtherSide
			getLocation("ATC Building (Back)").AddConnectingLocation(getLocation("northSide2"), BACKWARD);

			//northSide1
			getLocation("northSide1").AddConnectingLocation(getLocation("northSide2"), FORWARD);
			getLocation("northSide1").AddConnectingLocation(getLocation("toAS5"), LEFT);

			//northSide2
			getLocation("northSide2").AddConnectingLocation(getLocation("northSide3"), FORWARD);
			getLocation("northSide2").AddConnectingLocation(getLocation("ATC Building (Back)"), LEFT);
			getLocation("northSide2").AddConnectingLocation(getLocation("northSide1"), BACKWARD);

			//northSide3
			getLocation("northSide3").AddConnectingLocation(getLocation("northSide4"), FORWARD);
			getLocation("northSide3").AddConnectingLocation(getLocation("toAMDC5"), LEFT);
			getLocation("northSide3").AddConnectingLocation(getLocation("northSide2"), BACKWARD);

			//northSide4
			getLocation("northSide4").AddConnectingLocation(getLocation("northSide3"), BACKWARD);

			//trainPark1
			getLocation("trainPark1").AddConnectingLocation(getLocation("trainPark4"), FORWARD);
			getLocation("trainPark1").AddConnectingLocation(getLocation("trainPark3"), LEFT);
			getLocation("trainPark1").AddConnectingLocation(getLocation("trainPark2"), RIGHT);

			//trainPark2
			getLocation("trainPark2").AddConnectingLocation(getLocation("trainPark1"), BACKWARD);

			//trainPark3
			getLocation("trainPark3").AddConnectingLocation(getLocation("trainPark1"), BACKWARD);

			//trainPark4
			getLocation("trainPark4").AddConnectingLocation(getLocation("trainPark5"), FORWARD);
			getLocation("trainPark4").AddConnectingLocation(getLocation("trainPark1"), BACKWARD);

			//trainPark5
			getLocation("trainPark5").AddConnectingLocation(getLocation("trainPark6"), FORWARD);
			getLocation("trainPark5").AddConnectingLocation(getLocation("trainPark4"), BACKWARD);

			//trainPark6
			getLocation("trainPark6").AddConnectingLocation(getLocation("toTrain6"), FORWARD);
			getLocation("trainPark6").AddConnectingLocation(getLocation("trainPark5"), BACKWARD);

			//side1
			getLocation("side1").AddConnectingLocation(getLocation("side2"), FORWARD);
			getLocation("side1").AddConnectingLocation(getLocation("tunnel3"), BACKWARD);

			//side2
			getLocation("side2").AddConnectingLocation(getLocation("side3"), FORWARD);
			getLocation("side2").AddConnectingLocation(getLocation("side1"), BACKWARD);

			//side3
			getLocation("side3").AddConnectingLocation(getLocation("westSide6"), FORWARD);
			getLocation("side3").AddConnectingLocation(getLocation("side2"), BACKWARD);

			//EN
			getLocation("EN Building").AddConnectingLocation(getLocation("toEN4"), BACKWARD);
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

		public static Location getLocation(string locationName) {
			try {
				return _locations[locationName];
			} catch (KeyNotFoundException e) {
				Console.WriteLine("Cannot find font: " + locationName);
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
