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

			//campus images
			_images.Add("AD", new Bitmap("AD", "campus/AD.jpg"));
			_images.Add("AGSE", new Bitmap("AGSE", "campus/AGSE.jpg"));
			_images.Add("AMDC", new Bitmap("AMDC", "campus/AMDC.jpg"));
			_images.Add("AR", new Bitmap("AR", "campus/AR.jpg"));
			_images.Add("AS", new Bitmap("AS", "campus/AS.jpg"));
			_images.Add("ATC", new Bitmap("ATC", "campus/ATC.jpg"));
			_images.Add("ATCOtherSide", new Bitmap("ATCOtherSide", "campus/ATCOtherSide.jpg"));
			_images.Add("backOfAD", new Bitmap("backOfAD", "campus/backOfAD.jpg"));
			_images.Add("EN", new Bitmap("EN", "campus/EN.jpg"));
			_images.Add("FS", new Bitmap("FS", "campus/FS.jpg"));
			_images.Add("George", new Bitmap("George", "campus/George.jpg"));
			_images.Add("grassPath1", new Bitmap("grassPath1", "campus/grassPath1.jpg"));
			_images.Add("grassPath2", new Bitmap("grassPath2", "campus/grassPath2.jpg"));
			_images.Add("grassPath3", new Bitmap("grassPath3", "campus/grassPath3.jpg"));
			_images.Add("grassPath4", new Bitmap("grassPath4", "campus/grassPath4.jpg"));
			_images.Add("grassPath5", new Bitmap("grassPath5", "campus/grassPath5.jpg"));
			_images.Add("grassPath6", new Bitmap("grassPath6", "campus/grassPath6.jpg"));
			_images.Add("instreet1", new Bitmap("instreet1", "campus/instreet1.jpg"));
			_images.Add("instreet2", new Bitmap("instreet2", "campus/instreet2.jpg"));
			_images.Add("instreet3", new Bitmap("instreet3", "campus/instreet3.jpg"));
			_images.Add("instreet4", new Bitmap("instreet4", "campus/instreet4.jpg"));
			_images.Add("instreet5", new Bitmap("instreet5", "campus/instreet5.jpg"));
			_images.Add("instreet6", new Bitmap("instreet6", "campus/instreet6.jpg"));
			_images.Add("instreet7", new Bitmap("instreet7", "campus/instreet7.jpg"));
			_images.Add("instreet8", new Bitmap("instreet8", "campus/instreet8.jpg"));
			_images.Add("instreet9", new Bitmap("instreet9", "campus/instreet9.jpg"));
			_images.Add("northSide1", new Bitmap("northSide1", "campus/northSide1.jpg"));
			_images.Add("northSide2", new Bitmap("northSide2", "campus/northSide2.jpg"));
			_images.Add("northSide3", new Bitmap("northSide3", "campus/northSide3.jpg"));
			_images.Add("northSide4", new Bitmap("northSide4", "campus/northSide4.jpg"));
			_images.Add("side1", new Bitmap("side1", "campus/side1.jpg"));
			_images.Add("side2", new Bitmap("side2", "campus/side2.jpg"));
			_images.Add("side3", new Bitmap("side3", "campus/side3.jpg"));
			_images.Add("sneak1", new Bitmap("sneak1", "campus/sneak1.jpg"));
			_images.Add("sneak2", new Bitmap("sneak2", "campus/sneak2.jpg"));
			_images.Add("sneak3", new Bitmap("sneak3", "campus/sneak3.jpg"));
			_images.Add("southSide1", new Bitmap("southSide1", "campus/southSide1.jpg"));
			_images.Add("southSide2", new Bitmap("southSide2", "campus/southSide2.jpg"));
			_images.Add("southSide3", new Bitmap("southSide3", "campus/southSide3.jpg"));
			_images.Add("southSide4", new Bitmap("southSide4", "campus/southSide4.jpg"));
			_images.Add("southSide5", new Bitmap("southSide5", "campus/southSide5.jpg"));
			_images.Add("southSide6", new Bitmap("southSide6", "campus/southSide6.jpg"));
			_images.Add("spw1", new Bitmap("spw1", "campus/spw1.jpg"));
			_images.Add("spw2", new Bitmap("spw2", "campus/spw2.jpg"));
			_images.Add("spw3", new Bitmap("spw3", "campus/spw3.jpg"));
			_images.Add("spw4", new Bitmap("spw4", "campus/spw4.jpg"));
			_images.Add("spw5", new Bitmap("spw5", "campus/spw5.jpg"));
			_images.Add("spw6", new Bitmap("spw6", "campus/spw6.jpg"));
			_images.Add("spw7", new Bitmap("spw7", "campus/spw7.jpg"));
			_images.Add("spw8", new Bitmap("spw8", "campus/spw8.jpg"));
			_images.Add("spw9", new Bitmap("spw9", "campus/spw9.jpg"));
			_images.Add("stairs", new Bitmap("stairs", "campus/stairs.jpg"));
			_images.Add("studentCarPark", new Bitmap("studentCarPark", "campus/studentCarPark.jpg"));
			_images.Add("study", new Bitmap("study", "campus/study.jpg"));
			_images.Add("TA", new Bitmap("TA", "campus/TA.jpg"));
			_images.Add("TB", new Bitmap("TB", "campus/TB.jpg"));
			_images.Add("TC", new Bitmap("TC", "campus/TC.jpg"));
			_images.Add("TD", new Bitmap("TD", "campus/TD.jpg"));
			_images.Add("toAMDC1", new Bitmap("toAMDC1", "campus/toAMDC1.jpg"));
			_images.Add("toAMDC2", new Bitmap("toAMDC2", "campus/toAMDC2.jpg"));
			_images.Add("toAMDC3", new Bitmap("toAMDC3", "campus/toAMDC3.jpg"));
			_images.Add("toAMDC4", new Bitmap("toAMDC4", "campus/toAMDC4.jpg"));
			_images.Add("toAMDC5", new Bitmap("toAMDC5", "campus/toAMDC5.jpg"));
			_images.Add("toAS1", new Bitmap("toAS1", "campus/toAS1.jpg"));
			_images.Add("toAS2", new Bitmap("toAS2", "campus/toAS2.jpg"));
			_images.Add("toAS3", new Bitmap("toAS3", "campus/toAS3.jpg"));
			_images.Add("toAS4", new Bitmap("toAS4", "campus/toAS4.jpg"));
			_images.Add("toAS5", new Bitmap("toAS5", "campus/toAS5.jpg"));
			_images.Add("toATC1", new Bitmap("toATC1", "campus/toATC1.jpg"));
			_images.Add("toATC2", new Bitmap("toATC2", "campus/toATC2.jpg"));
			_images.Add("toATC3", new Bitmap("toATC3", "campus/toATC3.jpg"));
			_images.Add("toATC4", new Bitmap("toATC4", "campus/toATC4.jpg"));
			_images.Add("toEN1", new Bitmap("toEN1", "campus/toEN1.jpg"));
			_images.Add("toEN2", new Bitmap("toEN2", "campus/toEN2.jpg"));
			_images.Add("toEN3", new Bitmap("toEN3", "campus/toEN3.jpg"));
			_images.Add("toEN4", new Bitmap("toEN4", "campus/toEN4.jpg"));
			_images.Add("toEN5", new Bitmap("toEN5", "campus/toEN5.jpg"));
			_images.Add("toLodges1", new Bitmap("toLodges1", "campus/toLodges1.jpg"));
			_images.Add("toLodges2", new Bitmap("toLodges2", "campus/toLodges2.jpg"));
			_images.Add("toLodges3", new Bitmap("toLodges3", "campus/toLodges3.jpg"));
			_images.Add("toLodges4", new Bitmap("toLodges4", "campus/toLodges4.jpg"));
			_images.Add("toLodges5", new Bitmap("toLodges5", "campus/toLodges5.jpg"));
			_images.Add("toLodges6", new Bitmap("toLodges6", "campus/toLodges6.jpg"));
			_images.Add("toSouthSide1", new Bitmap("toSouthSide1", "campus/toSouthSide1.jpg"));
			_images.Add("toSouthSide2", new Bitmap("toSouthSide2", "campus/toSouthSide2.jpg"));
			_images.Add("toSouthSide3", new Bitmap("toSouthSide3", "campus/toSouthSide3.jpg"));
			_images.Add("toSouthSide4", new Bitmap("toSouthSide4", "campus/toSouthSide4.jpg"));
			_images.Add("toTrain1", new Bitmap("toTrain1", "campus/toTrain1.jpg"));
			_images.Add("toTrain2", new Bitmap("toTrain2", "campus/toTrain2.jpg"));
			_images.Add("toTrain3", new Bitmap("toTrain3", "campus/toTrain3.jpg"));
			_images.Add("toTrain4", new Bitmap("toTrain4", "campus/toTrain4.jpg"));
			_images.Add("toTrain5", new Bitmap("toTrain5", "campus/toTrain5.jpg"));
			_images.Add("toTrain6", new Bitmap("toTrain6", "campus/toTrain6.jpg"));
			_images.Add("toTrain7", new Bitmap("toTrain7", "campus/toTrain7.jpg"));
			_images.Add("toTrain8", new Bitmap("toTrain8", "campus/toTrain8.jpg"));
			_images.Add("toTrain9", new Bitmap("toTrain9", "campus/toTrain9.jpg"));
			_images.Add("toTrain10", new Bitmap("toTrain10", "campus/toTrain10.jpg"));
			_images.Add("toTrain11", new Bitmap("toTrain11", "campus/toTrain11.jpg"));
			_images.Add("toTrain12", new Bitmap("toTrain12", "campus/toTrain12.jpg"));
			_images.Add("toWestSide1", new Bitmap("toWestSide1", "campus/toWestSide1.jpg"));
			_images.Add("toWestSide2", new Bitmap("toWestSide2", "campus/toWestSide2.jpg"));
			_images.Add("Train", new Bitmap("Train", "campus/Train.jpg"));
			_images.Add("trainPark1", new Bitmap("trainPark1", "campus/trainPark1.jpg"));
			_images.Add("trainPark2", new Bitmap("trainPark2", "campus/trainPark2.jpg"));
			_images.Add("trainPark3", new Bitmap("trainPark3", "campus/trainPark3.jpg"));
			_images.Add("trainPark4", new Bitmap("trainPark4", "campus/trainPark4.jpg"));
			_images.Add("trainPark5", new Bitmap("trainPark5", "campus/trainPark5.jpg"));
			_images.Add("trainPark6", new Bitmap("trainPark6", "campus/trainPark6.jpg"));
			_images.Add("tunnel1", new Bitmap("tunnel1", "campus/tunnel1.jpg"));
			_images.Add("tunnel2", new Bitmap("tunnel2", "campus/tunnel2.jpg"));
			_images.Add("tunnel3", new Bitmap("tunnel3", "campus/tunnel3.jpg"));
			_images.Add("tunnel4", new Bitmap("tunnel4", "campus/tunnel4.jpg"));
			_images.Add("westSide1", new Bitmap("westSide1", "campus/westSide1.jpg"));
			_images.Add("westSide2", new Bitmap("westSide2", "campus/westSide2.jpg"));
			_images.Add("westSide3", new Bitmap("westSide3", "campus/westSide3.jpg"));
			_images.Add("westSide4", new Bitmap("westSide4", "campus/westSide4.jpg"));
			_images.Add("westSide5", new Bitmap("westSide5", "campus/westSide5.jpg"));
			_images.Add("westSide6", new Bitmap("westSide6", "campus/westSide6.jpg"));
			_images.Add("westSide7", new Bitmap("westSide7", "campus/westSide7.jpg"));
			_images.Add("westSide8", new Bitmap("westSide8", "campus/westSide8.jpg"));
			_images.Add("westSide9", new Bitmap("westSide9", "campus/westSide9.jpg"));
			_images.Add("westSide10", new Bitmap("westSide10", "campus/westSide10.jpg"));
			_images.Add("westSide11", new Bitmap("westSide11", "campus/westSide11.jpg"));
			_images.Add("westSide12", new Bitmap("westSide12", "campus/westSide12.jpg"));
			_images.Add("westSide13", new Bitmap("westSide13", "campus/westSide13.jpg"));
		}

		private static void LoadLocations() {
			_locations.Add("AD", new Location(_images["AD"], "AD Building")) ;
			_locations.Add("AGSE", new Location(_images["AGSE"], "AGSE Building"));
			_locations.Add("AMDC", new Location(_images["AMDC"], "AMDC Building"));
			_locations.Add("AR", new Location(_images["AR"], "AR Building"));
			_locations.Add("AS", new Location(_images["AS"], "AS Building"));
			_locations.Add("ATC", new Location(_images["ATC"], "ATC Building"));
			_locations.Add("ATCOtherSide", new Location(_images["ATCOtherSide"], "ATC Bulding (Back)"));
			_locations.Add("backOfAD", new Location(_images["backOfAD"], "backOfAD"));
			_locations.Add("EN", new Location(_images["EN"], "EN Building"));
			_locations.Add("FS", new Location(_images["FS"], "FS Building"));
			_locations.Add("George", new Location(_images["George"], "George Building"));
			_locations.Add("grassPath1", new Location(_images["grassPath1"], "grassPath1"));
			_locations.Add("grassPath2", new Location(_images["grassPath2"], "grassPath2"));
			_locations.Add("grassPath3", new Location(_images["grassPath3"], "grassPath3"));
			_locations.Add("grassPath4", new Location(_images["grassPath4"], "grassPath4"));
			_locations.Add("grassPath5", new Location(_images["grassPath5"], "grassPath5"));
			_locations.Add("grassPath6", new Location(_images["grassPath6"], "grassPath6"));
			_locations.Add("instreet1", new Location(_images["instreet1"], "instreet1"));
			_locations.Add("instreet2", new Location(_images["instreet2"], "instreet2"));
			_locations.Add("instreet3", new Location(_images["instreet3"], "instreet3"));
			_locations.Add("instreet4", new Location(_images["instreet4"], "instreet4"));
			_locations.Add("instreet5", new Location(_images["instreet5"], "instreet5"));
			_locations.Add("instreet6", new Location(_images["instreet6"], "instreet6"));
			_locations.Add("instreet7", new Location(_images["instreet7"], "instreet7"));
			_locations.Add("instreet8", new Location(_images["instreet8"], "instreet8"));
			_locations.Add("instreet9", new Location(_images["instreet9"], "instreet9"));
			_locations.Add("northSide1", new Location(_images["northSide1"], "northSide1"));
			_locations.Add("northSide2", new Location(_images["northSide2"], "northSide2"));
			_locations.Add("northSide3", new Location(_images["northSide3"], "northSide3"));
			_locations.Add("northSide4", new Location(_images["northSide4"], "northSide4"));
			_locations.Add("side1", new Location(_images["side1"], "side1"));
			_locations.Add("side2", new Location(_images["side2"], "side2"));
			_locations.Add("side3", new Location(_images["side3"], "side3"));
			_locations.Add("sneak1", new Location(_images["sneak1"], "sneak1"));
			_locations.Add("sneak2", new Location(_images["sneak2"], "sneak2"));
			_locations.Add("sneak3", new Location(_images["sneak3"], "sneak3"));
			_locations.Add("southSide1", new Location(_images["southSide1"], "southSide1"));
			_locations.Add("southSide2", new Location(_images["southSide2"], "southSide2"));
			_locations.Add("southSide3", new Location(_images["southSide3"], "southSide3."));
			_locations.Add("southSide4", new Location(_images["southSide4"], "southSide4"));
			_locations.Add("southSide5", new Location(_images["southSide5"], "southSide5"));
			_locations.Add("southSide6", new Location(_images["southSide6"], "southSide6"));
			_locations.Add("spw1", new Location(_images["spw1"], "spw1"));
			_locations.Add("spw2", new Location(_images["spw2"], "spw2"));
			_locations.Add("spw3", new Location(_images["spw3"], "spw3"));
			_locations.Add("spw4", new Location(_images["spw4"], "spw4"));
			_locations.Add("spw5", new Location(_images["spw5"], "spw5"));
			_locations.Add("spw6", new Location(_images["spw6"], "spw6"));
			_locations.Add("spw7", new Location(_images["spw7"], "spw7"));
			_locations.Add("spw8", new Location(_images["spw8"], "spw8"));
			_locations.Add("spw9", new Location(_images["spw9"], "spw9"));
			_locations.Add("stairs", new Location(_images["stairs"], "stairs"));
			_locations.Add("studentCarPark", new Location(_images["studentCarPark"], "studentCarPark"));
			_locations.Add("study", new Location(_images["study"], "study"));
			_locations.Add("TA", new Location(_images["TA"], "TA Building"));
			_locations.Add("TB", new Location(_images["TB"], "TB Building"));
			_locations.Add("TC", new Location(_images["TC"], "TC Building"));
			_locations.Add("TD", new Location(_images["TD"], "TD Building"));
			_locations.Add("toAMDC1", new Location(_images["toAMDC1"], "toAMDC1"));
			_locations.Add("toAMDC2", new Location(_images["toAMDC2"], "toAMDC2"));
			_locations.Add("toAMDC3", new Location(_images["toAMDC3"], "toAMDC3"));
			_locations.Add("toAMDC4", new Location(_images["toAMDC4"], "toAMDC4"));
			_locations.Add("toAMDC5", new Location(_images["toAMDC5"], "toAMDC5"));
			_locations.Add("toAS1", new Location(_images["toAS1"], "toAS1"));
			_locations.Add("toAS2", new Location(_images["toAS2"], "toAS2"));
			_locations.Add("toAS3", new Location(_images["toAS3"], "toAS3"));
			_locations.Add("toAS4", new Location(_images["toAS4"], "toAS4"));
			_locations.Add("toAS5", new Location(_images["toAS5"], "toAS5"));
			_locations.Add("toATC1", new Location(_images["toATC1"], "toATC1"));
			_locations.Add("toATC2", new Location(_images["toATC2"], "toATC2"));
			_locations.Add("toATC3", new Location(_images["toATC3"], "toATC3"));
			_locations.Add("toATC4", new Location(_images["toATC4"], "toATC4"));
			_locations.Add("toEN1", new Location(_images["toEN1"], "toEN1"));
			_locations.Add("toEN2", new Location(_images["toEN2"], "toEN2"));
			_locations.Add("toEN3", new Location(_images["toEN3"], "toEN3"));
			_locations.Add("toEN4", new Location(_images["toEN4"], "toEN4"));
			_locations.Add("toEN5", new Location(_images["toEN5"], "toEN5"));
			_locations.Add("toLodges1", new Location(_images["toLodges1"], "toLodges1"));
			_locations.Add("toLodges2", new Location(_images["toLodges2"], "toLodges2"));
			_locations.Add("toLodges3", new Location(_images["toLodges3"], "toLodges3"));
			_locations.Add("toLodges4", new Location(_images["toLodges4"], "toLodges4"));
			_locations.Add("toLodges5", new Location(_images["toLodges5"], "toLodges5"));
			_locations.Add("toLodges6", new Location(_images["toLodges6"], "toLodges6"));
			_locations.Add("toSouthSide1", new Location(_images["toSouthSide1"], "toSouthSide1"));
			_locations.Add("toSouthSide2", new Location(_images["toSouthSide2"], "toSouthSide2"));
			_locations.Add("toSouthSide3", new Location(_images["toSouthSide3"], "toSouthSide3"));
			_locations.Add("toSouthSide4", new Location(_images["toSouthSide4"], "toSouthSide4"));
			_locations.Add("toTrain1", new Location(_images["toTrain1"], "toTrain1"));
			_locations.Add("toTrain2", new Location(_images["toTrain2"], "toTrain2"));
			_locations.Add("toTrain3", new Location(_images["toTrain3"], "toTrain3"));
			_locations.Add("toTrain4", new Location(_images["toTrain4"], "toTrain4"));
			_locations.Add("toTrain5", new Location(_images["toTrain5"], "toTrain5"));
			_locations.Add("toTrain6", new Location(_images["toTrain6"], "toTrain6"));
			_locations.Add("toTrain7", new Location(_images["toTrain7"], "toTrain7"));
			_locations.Add("toTrain8", new Location(_images["toTrain8"], "toTrain8"));
			_locations.Add("toTrain9", new Location(_images["toTrain9"], "toTrain9."));
			_locations.Add("toTrain10", new Location(_images["toTrain10"], "toTrain10"));
			_locations.Add("toTrain11", new Location(_images["toTrain11"], "toTrain11"));
			_locations.Add("toTrain12", new Location(_images["toTrain12"], "toTrain12"));
			_locations.Add("toWestSide1", new Location(_images["toWestSide1"], "toWestSide1"));
			_locations.Add("toWestSide2", new Location(_images["toWestSide2"], "toWestSide2"));
			_locations.Add("Train", new Location(_images["Train"], "Train"));
			_locations.Add("trainPark1", new Location(_images["trainPark1"], "trainPark1"));
			_locations.Add("trainPark2", new Location(_images["trainPark2"], "trainPark2"));
			_locations.Add("trainPark3", new Location(_images["trainPark3"], "trainPark3"));
			_locations.Add("trainPark4", new Location(_images["trainPark4"], "trainPark4"));
			_locations.Add("trainPark5", new Location(_images["trainPark5"], "trainPark5"));
			_locations.Add("trainPark6", new Location(_images["trainPark6"], "trainPark6."));
			_locations.Add("tunnel1", new Location(_images["tunnel1"], "tunnel1"));
			_locations.Add("tunnel2", new Location(_images["tunnel2"], "tunnel2"));
			_locations.Add("tunnel3", new Location(_images["tunnel3"], "tunnel3"));
			_locations.Add("tunnel4", new Location(_images["tunnel4"], "tunnel4"));
			_locations.Add("westSide1", new Location(_images["westSide1"], "westSide1"));
			_locations.Add("westSide2", new Location(_images["westSide2"], "westSide2"));
			_locations.Add("westSide3", new Location(_images["westSide3"], "westSide3"));
			_locations.Add("westSide4", new Location(_images["westSide4"], "westSide4"));
			_locations.Add("westSide5", new Location(_images["westSide5"], "westSide5"));
			_locations.Add("westSide6", new Location(_images["westSide6"], "westSide6"));
			_locations.Add("westSide7", new Location(_images["westSide7"], "westSide7"));
			_locations.Add("westSide8", new Location(_images["westSide8"], "westSide8"));
			_locations.Add("westSide9", new Location(_images["westSide9"], "westSide9"));
			_locations.Add("westSide10", new Location(_images["westSide10"], "westSide10"));
			_locations.Add("westSide11", new Location(_images["westSide11"], "westSide11"));
			_locations.Add("westSide12", new Location(_images["westSide12"], "westSide12"));
			_locations.Add("westSide13", new Location(_images["westSide13"], "westSide13"));

			ConfigureLocations();
		}

		private static void ConfigureLocations() {
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
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			getLocation("toTrain4").AddConnectingLocation(getLocation("toTrain3"), BACKWARD);

			//toTrain5
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain1"), BACKWARD);

			//toTrain5
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain1"), BACKWARD);

			//toTrain6
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain3"), FORWARD);
			//getLocation("toTrain2").AddConnectingLocation(getLocation("toTrain1"), BACKWARD);
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
