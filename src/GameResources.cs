﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using SplashKitSDK;

namespace Swinburneexplorer
{
	public static class GameResources
	{
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
			//Stopwatch sw = new Stopwatch();
			//sw.Start();
			_images.Add(locName, new Bitmap(locName, "campus/" + locName + ".jpg"));
			//sw.Stop();
			//Console.WriteLine($"Took {sw.ElapsedMilliseconds} to load campus/{locName}.jpg");
		}

		private static void LoadImages() {
			_images.Add("SwinLogo", new Bitmap("SwinLogo", "SwinLogo.png"));
			_images.Add("Arrow", new Bitmap("Arrow", "Arrow.png"));
			_images.Add("sArrow", new Bitmap("sArrow", "sArrow.png"));
			_images.Add("sMap", new Bitmap("sMap", "sMap.png"));
			//_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMap.png"));
			_images.Add("SwinMap", new Bitmap("SwinMap", "SwinMapPS.png"));
			//_images.Add("scroll", new Bitmap("scroll", "smallScroll.png"));
			_images.Add("btnBase", new Bitmap("btnBase", "btnBase1.png"));
			_images.Add("infoBtn", new Bitmap("infoBtn", "infoBtn.png"));
			_images.Add("scroll", new Bitmap("scroll", "tinyScroll.png"));

			/*
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
			*/
		}

		private static void LoadLocations() {
			_locations.Add("AD Building", new Location("AD Building")) ;
			_locations.Add("AGSE Building", new Location("AGSE Building"));
			_locations.Add("AMDC Building", new Location("AMDC Building"));
			_locations.Add("AR Building", new Location("AR Building"));
			_locations.Add("AS Building", new Location("AS Building"));
			_locations.Add("ATC Building", new Location("ATC Building"));
			_locations.Add("ATC Building (Back)", new Location("ATC Building (Back)"));
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
			GetLocation("AGSE Building").AddConnectingLocation(GetLocation("tunnel2"), BACKWARD);

			//toTrain1
			GetLocation("toTrain1").AddConnectingLocation(GetLocation("toTrain2"), FORWARD);
			GetLocation("toTrain1").AddConnectingLocation(GetLocation("toAMDC1"), LEFT);
            GetLocation("toTrain1").AddConnectingLocation(GetLocation("sneak1"), BACKWARD);

            //toTrain2
            GetLocation("toTrain2").AddConnectingLocation(GetLocation("toTrain3"), FORWARD);
			GetLocation("toTrain2").AddConnectingLocation(GetLocation("toTrain1"), BACKWARD);

			//toTrain3
			GetLocation("toTrain3").AddConnectingLocation(GetLocation("toTrain4"), FORWARD);
			GetLocation("toTrain3").AddConnectingLocation(GetLocation("toTrain2"), BACKWARD);
			GetLocation("toTrain3").AddConnectingLocation(GetLocation("toEN1"), LEFT);

			//toTrain4
			GetLocation("toTrain4").AddConnectingLocation(GetLocation("toTrain5"), FORWARD);
			GetLocation("toTrain4").AddConnectingLocation(GetLocation("toTrain3"), BACKWARD);

			//toTrain5
			GetLocation("toTrain5").AddConnectingLocation(GetLocation("toTrain6"), FORWARD);
			GetLocation("toTrain5").AddConnectingLocation(GetLocation("toTrain4"), BACKWARD);
			GetLocation("toTrain5").AddConnectingLocation(GetLocation("AR Building"), LEFT);

			//toTrain6
			GetLocation("toTrain6").AddConnectingLocation(GetLocation("toTrain7"), FORWARD);
			GetLocation("toTrain6").AddConnectingLocation(GetLocation("toTrain5"), BACKWARD);
			GetLocation("toTrain6").AddConnectingLocation(GetLocation("trainPark6"), LEFT);

			//toTrain7
			GetLocation("toTrain7").AddConnectingLocation(GetLocation("toTrain8"), RIGHT);
			GetLocation("toTrain7").AddConnectingLocation(GetLocation("toTrain6"), BACKWARD);

			//toTrain8
			GetLocation("toTrain8").AddConnectingLocation(GetLocation("toTrain7"), LEFT);
			GetLocation("toTrain8").AddConnectingLocation(GetLocation("Train"), RIGHT);
			GetLocation("toTrain8").AddConnectingLocation(GetLocation("toTrain9"), BACKWARD);

			//toTrain9
			GetLocation("toTrain9").AddConnectingLocation(GetLocation("toTrain8"), FORWARD);
			GetLocation("toTrain9").AddConnectingLocation(GetLocation("toTrain10"), BACKWARD);
            GetLocation("toTrain9").AddConnectingLocation(GetLocation("toTrain10"), RIGHT);
            GetLocation("toTrain9").AddConnectingLocation(GetLocation("toTrain11"), LEFT);

            //toTrain10
            GetLocation("toTrain10").AddConnectingLocation(GetLocation("toTrain11"), FORWARD);
			GetLocation("toTrain10").AddConnectingLocation(GetLocation("toTrain9"), RIGHT);

			//toTrain11
			GetLocation("toTrain11").AddConnectingLocation(GetLocation("toTrain12"), FORWARD);
			GetLocation("toTrain11").AddConnectingLocation(GetLocation("toTrain10"), BACKWARD);

			//toTrain12
			GetLocation("toTrain12").AddConnectingLocation(GetLocation("spw4"), FORWARD);
			GetLocation("toTrain12").AddConnectingLocation(GetLocation("toTrain11"), BACKWARD);

			//Train
			GetLocation("Train").AddConnectingLocation(GetLocation("toTrain8"), LEFT);
            GetLocation("Train").AddConnectingLocation(GetLocation("toTrain9"), RIGHT);

            //spw1
            GetLocation("spw1").AddConnectingLocation(GetLocation("spw2"), BACKWARD);

			//spw2
			GetLocation("spw2").AddConnectingLocation(GetLocation("spw1"), RIGHT);
			GetLocation("spw2").AddConnectingLocation(GetLocation("spw3"), FORWARD);
			GetLocation("spw2").AddConnectingLocation(GetLocation("instreet9"), LEFT);

			//spw3
			GetLocation("spw3").AddConnectingLocation(GetLocation("spw4"), FORWARD);
			GetLocation("spw3").AddConnectingLocation(GetLocation("spw2"), BACKWARD);

			//spw4
			GetLocation("spw4").AddConnectingLocation(GetLocation("spw3"), LEFT);
			GetLocation("spw4").AddConnectingLocation(GetLocation("spw5"), FORWARD);
			GetLocation("spw4").AddConnectingLocation(GetLocation("toTrain12"), BACKWARD);

			//spw5
			GetLocation("spw5").AddConnectingLocation(GetLocation("spw6"), FORWARD);
			GetLocation("spw5").AddConnectingLocation(GetLocation("spw4"), BACKWARD);

			//spw6
			GetLocation("spw6").AddConnectingLocation(GetLocation("spw7"), FORWARD);
			GetLocation("spw6").AddConnectingLocation(GetLocation("spw5"), BACKWARD);

			//spw7
			GetLocation("spw7").AddConnectingLocation(GetLocation("grassPath1"), FORWARD);
			GetLocation("spw7").AddConnectingLocation(GetLocation("spw8"), LEFT);
			GetLocation("spw7").AddConnectingLocation(GetLocation("spw6"), BACKWARD);

			//spw8
			GetLocation("spw8").AddConnectingLocation(GetLocation("spw7"), FORWARD);
			GetLocation("spw8").AddConnectingLocation(GetLocation("spw9"), BACKWARD);

			//spw9
			GetLocation("spw9").AddConnectingLocation(GetLocation("spw8"), FORWARD);
			GetLocation("spw9").AddConnectingLocation(GetLocation("instreet8"), BACKWARD);

			//grassPath1
			GetLocation("grassPath1").AddConnectingLocation(GetLocation("grassPath2"), FORWARD);
			GetLocation("grassPath1").AddConnectingLocation(GetLocation("spw7"), BACKWARD);

			//grassPath2
			GetLocation("grassPath2").AddConnectingLocation(GetLocation("grassPath3"), FORWARD);
			GetLocation("grassPath2").AddConnectingLocation(GetLocation("grassPath1"), BACKWARD);

			//grassPath3
			GetLocation("grassPath3").AddConnectingLocation(GetLocation("grassPath4"), FORWARD);
			GetLocation("grassPath3").AddConnectingLocation(GetLocation("grassPath2"), BACKWARD);

			//grassPath4
			GetLocation("grassPath4").AddConnectingLocation(GetLocation("grassPath5"), FORWARD);
			GetLocation("grassPath4").AddConnectingLocation(GetLocation("grassPath3"), BACKWARD);

			//grassPath5
			GetLocation("grassPath5").AddConnectingLocation(GetLocation("grassPath6"), FORWARD);
			GetLocation("grassPath5").AddConnectingLocation(GetLocation("grassPath4"), BACKWARD);

			//grassPath6
			GetLocation("grassPath6").AddConnectingLocation(GetLocation("tunnel3"), LEFT);
			GetLocation("grassPath6").AddConnectingLocation(GetLocation("tunnel4"), RIGHT);
			GetLocation("grassPath6").AddConnectingLocation(GetLocation("grassPath5"), BACKWARD);
            GetLocation("grassPath6").AddConnectingLocation(GetLocation("side1"), FORWARD);

            //tunnel1
            GetLocation("tunnel1").AddConnectingLocation(GetLocation("tunnel2"), FORWARD);
			GetLocation("tunnel1").AddConnectingLocation(GetLocation("toSouthSide1"), BACKWARD);
            GetLocation("tunnel1").AddConnectingLocation(GetLocation("instreet2"), RIGHT);
            GetLocation("tunnel1").AddConnectingLocation(GetLocation("instreet1"), LEFT);

            //tunnel2
            GetLocation("tunnel2").AddConnectingLocation(GetLocation("tunnel3"), FORWARD);
			GetLocation("tunnel2").AddConnectingLocation(GetLocation("AGSE Building"), LEFT);
			GetLocation("tunnel2").AddConnectingLocation(GetLocation("tunnel1"), BACKWARD);

			//tunnel3
			GetLocation("tunnel3").AddConnectingLocation(GetLocation("tunnel4"), FORWARD);
			GetLocation("tunnel3").AddConnectingLocation(GetLocation("tunnel2"), BACKWARD);
			GetLocation("tunnel3").AddConnectingLocation(GetLocation("grassPath6"), RIGHT);
			GetLocation("tunnel3").AddConnectingLocation(GetLocation("side1"), LEFT);

			//tunnel4
			GetLocation("tunnel4").AddConnectingLocation(GetLocation("toAMDC1"), FORWARD);
			GetLocation("tunnel4").AddConnectingLocation(GetLocation("tunnel3"), BACKWARD);
			GetLocation("tunnel4").AddConnectingLocation(GetLocation("grassPath6"), RIGHT);

			//toAMDC1
			GetLocation("toAMDC1").AddConnectingLocation(GetLocation("toAMDC2"), FORWARD);
			GetLocation("toAMDC1").AddConnectingLocation(GetLocation("tunnel4"), BACKWARD);
			GetLocation("toAMDC1").AddConnectingLocation(GetLocation("toTrain1"), RIGHT);
			GetLocation("toAMDC1").AddConnectingLocation(GetLocation("sneak1"), LEFT);

			//toAMDC2
			GetLocation("toAMDC2").AddConnectingLocation(GetLocation("toAMDC3"), FORWARD);
			GetLocation("toAMDC2").AddConnectingLocation(GetLocation("toAMDC1"), BACKWARD);
			GetLocation("toAMDC2").AddConnectingLocation(GetLocation("AD Building"), RIGHT);

			//toAMDC3
			GetLocation("toAMDC3").AddConnectingLocation(GetLocation("toAMDC4"), FORWARD);
			GetLocation("toAMDC3").AddConnectingLocation(GetLocation("toAMDC2"), BACKWARD);
			GetLocation("toAMDC3").AddConnectingLocation(GetLocation("study"), LEFT);
			GetLocation("toAMDC3").AddConnectingLocation(GetLocation("toATC1"), RIGHT);

			//toAMDC4
			GetLocation("toAMDC4").AddConnectingLocation(GetLocation("toAMDC5"), FORWARD);
			GetLocation("toAMDC4").AddConnectingLocation(GetLocation("toAMDC3"), BACKWARD);
			GetLocation("toAMDC4").AddConnectingLocation(GetLocation("toWestSide1"), LEFT);

			//toAMDC5
			GetLocation("toAMDC5").AddConnectingLocation(GetLocation("northSide3"), FORWARD);
			GetLocation("toAMDC5").AddConnectingLocation(GetLocation("toAMDC4"), BACKWARD);
			GetLocation("toAMDC5").AddConnectingLocation(GetLocation("AMDC Building"), LEFT);

			//AMDC
			GetLocation("AMDC Building").AddConnectingLocation(GetLocation("toWestSide1"), LEFT);
			GetLocation("AMDC Building").AddConnectingLocation(GetLocation("toAMDC4"), BACKWARD);
			GetLocation("AMDC Building").AddConnectingLocation(GetLocation("toAMDC5"), RIGHT);

			//study
			GetLocation("study").AddConnectingLocation(GetLocation("toAMDC3"), BACKWARD);
            GetLocation("study").AddConnectingLocation(GetLocation("toAMDC4"), RIGHT);
            GetLocation("study").AddConnectingLocation(GetLocation("toAMDC2"), LEFT);

            //toWestSide1
            GetLocation("toWestSide1").AddConnectingLocation(GetLocation("FS Building"), FORWARD);
			GetLocation("toWestSide1").AddConnectingLocation(GetLocation("toAMDC4"), BACKWARD);
			GetLocation("toWestSide1").AddConnectingLocation(GetLocation("AMDC Building"), RIGHT);

			//toWestSide2
			GetLocation("toWestSide2").AddConnectingLocation(GetLocation("westSide1"), LEFT);
            GetLocation("toWestSide2").AddConnectingLocation(GetLocation("westSide2"), FORWARD);
            GetLocation("toWestSide2").AddConnectingLocation(GetLocation("FS Building"), BACKWARD);

			//FS
			GetLocation("FS Building").AddConnectingLocation(GetLocation("toWestSide2"), FORWARD);
			GetLocation("FS Building").AddConnectingLocation(GetLocation("toWestSide1"), BACKWARD);

			//westSide1
			GetLocation("westSide1").AddConnectingLocation(GetLocation("stairs"), BACKWARD);
			GetLocation("westSide1").AddConnectingLocation(GetLocation("westSide2"), FORWARD);

			//westSide2
			GetLocation("westSide2").AddConnectingLocation(GetLocation("westSide1"), BACKWARD);
			GetLocation("westSide2").AddConnectingLocation(GetLocation("toWestSide2"), RIGHT);

			//stairs
			GetLocation("stairs").AddConnectingLocation(GetLocation("westSide1"), LEFT);
			GetLocation("stairs").AddConnectingLocation(GetLocation("westSide3"), RIGHT);

			//westSide3
			GetLocation("westSide3").AddConnectingLocation(GetLocation("stairs"), FORWARD);
			GetLocation("westSide3").AddConnectingLocation(GetLocation("westSide4"), BACKWARD);

			//westSide4
			GetLocation("westSide4").AddConnectingLocation(GetLocation("sneak3"), RIGHT);
			GetLocation("westSide4").AddConnectingLocation(GetLocation("westSide5"), BACKWARD);
			GetLocation("westSide4").AddConnectingLocation(GetLocation("westSide3"), FORWARD);

            //westSide5
            GetLocation("westSide5").AddConnectingLocation(GetLocation("side3"), RIGHT);
            GetLocation("westSide5").AddConnectingLocation(GetLocation("westSide4"), FORWARD);
			GetLocation("westSide5").AddConnectingLocation(GetLocation("westSide6"), BACKWARD);

			//westSide6
			GetLocation("westSide6").AddConnectingLocation(GetLocation("westSide7"), BACKWARD);
			GetLocation("westSide6").AddConnectingLocation(GetLocation("westSide5"), FORWARD);

			//westSide7
			GetLocation("westSide7").AddConnectingLocation(GetLocation("westSide6"), FORWARD);
			GetLocation("westSide7").AddConnectingLocation(GetLocation("westSide8"), BACKWARD);

			//westSide8
			GetLocation("westSide8").AddConnectingLocation(GetLocation("westSide9"), RIGHT);
			GetLocation("westSide8").AddConnectingLocation(GetLocation("westSide7"), FORWARD);

			//westSide9
			GetLocation("westSide9").AddConnectingLocation(GetLocation("westSide8"), RIGHT);
			GetLocation("westSide9").AddConnectingLocation(GetLocation("westSide11"), LEFT);
			GetLocation("westSide9").AddConnectingLocation(GetLocation("westSide10"), BACKWARD);

			//westSide10
			GetLocation("westSide10").AddConnectingLocation(GetLocation("westSide9"), FORWARD);
			GetLocation("westSide10").AddConnectingLocation(GetLocation("instreet1"), BACKWARD);

			//westSide11
			GetLocation("westSide11").AddConnectingLocation(GetLocation("westSide12"), FORWARD);
			GetLocation("westSide11").AddConnectingLocation(GetLocation("westSide9"), BACKWARD);

			//westSide12
			GetLocation("westSide12").AddConnectingLocation(GetLocation("westSide13"), FORWARD);
			GetLocation("westSide12").AddConnectingLocation(GetLocation("westSide11"), BACKWARD);

			//westSide13
			GetLocation("westSide13").AddConnectingLocation(GetLocation("southSide6"), LEFT);
			GetLocation("westSide13").AddConnectingLocation(GetLocation("westSide12"), BACKWARD);

			//sneak1
			GetLocation("sneak1").AddConnectingLocation(GetLocation("toAMDC1"), RIGHT);
			GetLocation("sneak1").AddConnectingLocation(GetLocation("sneak2"), BACKWARD);
            GetLocation("sneak1").AddConnectingLocation(GetLocation("toTrain1"), FORWARD);

            //sneak2
            GetLocation("sneak2").AddConnectingLocation(GetLocation("sneak1"), FORWARD);
			GetLocation("sneak2").AddConnectingLocation(GetLocation("sneak3"), BACKWARD);

			//sneak3
			GetLocation("sneak3").AddConnectingLocation(GetLocation("sneak2"), FORWARD);
			GetLocation("sneak3").AddConnectingLocation(GetLocation("westSide4"), BACKWARD);

			//southSide1
			GetLocation("southSide1").AddConnectingLocation(GetLocation("toLodges1"), LEFT);
			GetLocation("southSide1").AddConnectingLocation(GetLocation("southSide2"), BACKWARD);

			//southSide2
			GetLocation("southSide2").AddConnectingLocation(GetLocation("southSide1"), FORWARD);
			GetLocation("southSide2").AddConnectingLocation(GetLocation("southSide3"), BACKWARD);

			//southSide3
			GetLocation("southSide3").AddConnectingLocation(GetLocation("southSide2"), FORWARD);
			GetLocation("southSide3").AddConnectingLocation(GetLocation("toSouthSide4"), LEFT);
			GetLocation("southSide3").AddConnectingLocation(GetLocation("southSide4"), BACKWARD);

			//southSide4
			GetLocation("southSide4").AddConnectingLocation(GetLocation("southSide3"), FORWARD);
			GetLocation("southSide4").AddConnectingLocation(GetLocation("TC Building"), LEFT);
			GetLocation("southSide4").AddConnectingLocation(GetLocation("southSide5"), BACKWARD);

			//southSide5
			GetLocation("southSide5").AddConnectingLocation(GetLocation("southSide4"), FORWARD);
			GetLocation("southSide5").AddConnectingLocation(GetLocation("southSide6"), BACKWARD);

			//southSide6
			GetLocation("southSide6").AddConnectingLocation(GetLocation("southSide5"), FORWARD);
			GetLocation("southSide6").AddConnectingLocation(GetLocation("westSide13"), BACKWARD);

			//TA
			GetLocation("TA Building").AddConnectingLocation(GetLocation("toSouthSide1"), LEFT);
            GetLocation("TA Building").AddConnectingLocation(GetLocation("toSouthSide2"), RIGHT);

            //TB
            GetLocation("TB Building").AddConnectingLocation(GetLocation("toSouthSide1"), RIGHT);
            GetLocation("TB Building").AddConnectingLocation(GetLocation("toSouthSide2"), LEFT);

            //TC
            GetLocation("TC Building").AddConnectingLocation(GetLocation("southSide4"), BACKWARD);

			//TD
			GetLocation("TD Building").AddConnectingLocation(GetLocation("toLodges3"), BACKWARD);

			//toSouthSide1
			GetLocation("toSouthSide1").AddConnectingLocation(GetLocation("toSouthSide2"), FORWARD);
			GetLocation("toSouthSide1").AddConnectingLocation(GetLocation("instreet2"), BACKWARD);
			GetLocation("toSouthSide1").AddConnectingLocation(GetLocation("TA Building"), LEFT);
			GetLocation("toSouthSide1").AddConnectingLocation(GetLocation("TB Building"), RIGHT);

			//toSouthSide2
			GetLocation("toSouthSide2").AddConnectingLocation(GetLocation("toSouthSide3"), FORWARD);
			GetLocation("toSouthSide2").AddConnectingLocation(GetLocation("toSouthSide1"), BACKWARD);

			//toSouthSide3
			GetLocation("toSouthSide3").AddConnectingLocation(GetLocation("toSouthSide4"), FORWARD);
			GetLocation("toSouthSide3").AddConnectingLocation(GetLocation("toSouthSide2"), BACKWARD);

			//toSouthSide4
			GetLocation("toSouthSide4").AddConnectingLocation(GetLocation("southSide3"), FORWARD);
			GetLocation("toSouthSide4").AddConnectingLocation(GetLocation("toSouthSide3"), BACKWARD);

			//toLodges1
			GetLocation("toLodges1").AddConnectingLocation(GetLocation("toLodges2"), FORWARD);
			GetLocation("toLodges1").AddConnectingLocation(GetLocation("southSide1"), BACKWARD);

			//toLodges2
			GetLocation("toLodges2").AddConnectingLocation(GetLocation("toLodges3"), FORWARD);
			GetLocation("toLodges2").AddConnectingLocation(GetLocation("toLodges1"), BACKWARD);

			//toLodges3
			GetLocation("toLodges3").AddConnectingLocation(GetLocation("toLodges4"), FORWARD);
			GetLocation("toLodges3").AddConnectingLocation(GetLocation("toLodges2"), BACKWARD);
			GetLocation("toLodges3").AddConnectingLocation(GetLocation("TD Building"), LEFT);

			//toLodges4
			GetLocation("toLodges4").AddConnectingLocation(GetLocation("toLodges5"), FORWARD);
			GetLocation("toLodges4").AddConnectingLocation(GetLocation("toLodges3"), BACKWARD);

			//toLodges5
			GetLocation("toLodges5").AddConnectingLocation(GetLocation("toLodges6"), FORWARD);
			GetLocation("toLodges5").AddConnectingLocation(GetLocation("toLodges4"), BACKWARD);

			//toLodges6
			GetLocation("toLodges6").AddConnectingLocation(GetLocation("instreet7"), FORWARD);
			GetLocation("toLodges6").AddConnectingLocation(GetLocation("toLodges5"), BACKWARD);

			//instreet1
			GetLocation("instreet1").AddConnectingLocation(GetLocation("westSide10"), FORWARD);
			GetLocation("instreet1").AddConnectingLocation(GetLocation("instreet2"), BACKWARD);

			//instreet2
			GetLocation("instreet2").AddConnectingLocation(GetLocation("instreet1"), FORWARD);
			GetLocation("instreet2").AddConnectingLocation(GetLocation("toSouthSide1"), LEFT);
			GetLocation("instreet2").AddConnectingLocation(GetLocation("tunnel1"), RIGHT);
			GetLocation("instreet2").AddConnectingLocation(GetLocation("instreet3"), BACKWARD);

			//instreet3
			GetLocation("instreet3").AddConnectingLocation(GetLocation("instreet2"), FORWARD);
			GetLocation("instreet3").AddConnectingLocation(GetLocation("George Building"), BACKWARD);

			//George
			GetLocation("George Building").AddConnectingLocation(GetLocation("instreet3"), FORWARD);
			GetLocation("George Building").AddConnectingLocation(GetLocation("instreet4"), BACKWARD);

			//instreet4
			GetLocation("instreet4").AddConnectingLocation(GetLocation("George Building"), FORWARD);
			GetLocation("instreet4").AddConnectingLocation(GetLocation("instreet5"), BACKWARD);

			//instreet5
			GetLocation("instreet5").AddConnectingLocation(GetLocation("instreet4"), FORWARD);
			GetLocation("instreet5").AddConnectingLocation(GetLocation("instreet6"), BACKWARD);

			//instreet6
			GetLocation("instreet6").AddConnectingLocation(GetLocation("instreet5"), FORWARD);
			GetLocation("instreet6").AddConnectingLocation(GetLocation("instreet7"), BACKWARD);

			//instreet7
			GetLocation("instreet7").AddConnectingLocation(GetLocation("instreet6"), FORWARD);
			GetLocation("instreet7").AddConnectingLocation(GetLocation("toLodges6"), LEFT);
			GetLocation("instreet7").AddConnectingLocation(GetLocation("instreet8"), BACKWARD);

			//instreet8
			GetLocation("instreet8").AddConnectingLocation(GetLocation("instreet7"), FORWARD);
			GetLocation("instreet8").AddConnectingLocation(GetLocation("spw9"), RIGHT);
			GetLocation("instreet8").AddConnectingLocation(GetLocation("instreet9"), BACKWARD);

			//instreet9
			GetLocation("instreet9").AddConnectingLocation(GetLocation("instreet8"), FORWARD);
			GetLocation("instreet9").AddConnectingLocation(GetLocation("spw2"), BACKWARD);
			GetLocation("instreet9").AddConnectingLocation(GetLocation("studentCarPark"), LEFT);

			//studentCarPark
			GetLocation("studentCarPark").AddConnectingLocation(GetLocation("instreet9"), BACKWARD);

			//AD
			GetLocation("AD Building").AddConnectingLocation(GetLocation("toAMDC2"), BACKWARD);

			//backOfAD
			GetLocation("backOfAD").AddConnectingLocation(GetLocation("toEN2"), BACKWARD);

			//toEN1
			GetLocation("toEN1").AddConnectingLocation(GetLocation("toTrain3"), BACKWARD);
			GetLocation("toEN1").AddConnectingLocation(GetLocation("toEN2"), FORWARD);

			//toEN2
			GetLocation("toEN2").AddConnectingLocation(GetLocation("toEN3"), FORWARD);
			GetLocation("toEN2").AddConnectingLocation(GetLocation("backOfAD"), LEFT);
			GetLocation("toEN2").AddConnectingLocation(GetLocation("toEN1"), BACKWARD);

			//toEN3
			GetLocation("toEN3").AddConnectingLocation(GetLocation("toEN4"), FORWARD);
			GetLocation("toEN3").AddConnectingLocation(GetLocation("toEN2"), BACKWARD);

			//toEN4
			GetLocation("toEN4").AddConnectingLocation(GetLocation("EN Building"), FORWARD);
			GetLocation("toEN4").AddConnectingLocation(GetLocation("toATC4"), LEFT);
			GetLocation("toEN4").AddConnectingLocation(GetLocation("toEN3"), BACKWARD);

			//toEN5
			GetLocation("toEN5").AddConnectingLocation(GetLocation("toATC4"), FORWARD);
			GetLocation("toEN5").AddConnectingLocation(GetLocation("toAS1"), LEFT);

			//AR
			GetLocation("AR Building").AddConnectingLocation(GetLocation("toTrain5"), BACKWARD);

			//toATC1
			GetLocation("toATC1").AddConnectingLocation(GetLocation("toAMDC3"), FORWARD);
			GetLocation("toATC1").AddConnectingLocation(GetLocation("toATC2"), BACKWARD);

			//toATC2
			GetLocation("toATC2").AddConnectingLocation(GetLocation("toATC1"), FORWARD);
			GetLocation("toATC2").AddConnectingLocation(GetLocation("toATC4"), BACKWARD);
			GetLocation("toATC2").AddConnectingLocation(GetLocation("toATC3"), RIGHT);

			//toATC3
			GetLocation("toATC3").AddConnectingLocation(GetLocation("ATC Building"), FORWARD);
			GetLocation("toATC3").AddConnectingLocation(GetLocation("toATC2"), BACKWARD);

			//toATC4
			GetLocation("toATC4").AddConnectingLocation(GetLocation("toATC2"), FORWARD);
			GetLocation("toATC4").AddConnectingLocation(GetLocation("toEN4"), LEFT);
			GetLocation("toATC4").AddConnectingLocation(GetLocation("toEN5"), RIGHT);

			//ATC
			GetLocation("ATC Building").AddConnectingLocation(GetLocation("toATC3"), BACKWARD);

			//toAS1
			GetLocation("toAS1").AddConnectingLocation(GetLocation("toEN5"), FORWARD);
			GetLocation("toAS1").AddConnectingLocation(GetLocation("toAS2"), BACKWARD);

			//toAS2
			GetLocation("toAS2").AddConnectingLocation(GetLocation("toAS1"), FORWARD);
			GetLocation("toAS2").AddConnectingLocation(GetLocation("toAS3"), BACKWARD);

			//toAS3
			GetLocation("toAS3").AddConnectingLocation(GetLocation("toAS2"), FORWARD);
			GetLocation("toAS3").AddConnectingLocation(GetLocation("AS Building"), BACKWARD);

			//toAS4
			GetLocation("toAS4").AddConnectingLocation(GetLocation("AS Building"), FORWARD);
			GetLocation("toAS4").AddConnectingLocation(GetLocation("toAS5"), BACKWARD);

			//toAS5
			GetLocation("toAS5").AddConnectingLocation(GetLocation("toAS4"), FORWARD);
			GetLocation("toAS5").AddConnectingLocation(GetLocation("northSide1"), RIGHT);

			//AS
			GetLocation("AS Building").AddConnectingLocation(GetLocation("toAS3"), LEFT);
			GetLocation("AS Building").AddConnectingLocation(GetLocation("toAS4"), BACKWARD);

			//ATCOtherSide
			GetLocation("ATC Building (Back)").AddConnectingLocation(GetLocation("northSide2"), BACKWARD);

			//northSide1
			GetLocation("northSide1").AddConnectingLocation(GetLocation("northSide2"), FORWARD);
			GetLocation("northSide1").AddConnectingLocation(GetLocation("toAS5"), LEFT);

			//northSide2
			GetLocation("northSide2").AddConnectingLocation(GetLocation("northSide3"), FORWARD);
			GetLocation("northSide2").AddConnectingLocation(GetLocation("ATC Building (Back)"), LEFT);
			GetLocation("northSide2").AddConnectingLocation(GetLocation("northSide1"), BACKWARD);

			//northSide3
			GetLocation("northSide3").AddConnectingLocation(GetLocation("northSide4"), FORWARD);
			GetLocation("northSide3").AddConnectingLocation(GetLocation("toAMDC5"), LEFT);
			GetLocation("northSide3").AddConnectingLocation(GetLocation("northSide2"), BACKWARD);

			//northSide4
			GetLocation("northSide4").AddConnectingLocation(GetLocation("northSide3"), BACKWARD);

			//trainPark1
			GetLocation("trainPark1").AddConnectingLocation(GetLocation("trainPark4"), FORWARD);
			GetLocation("trainPark1").AddConnectingLocation(GetLocation("trainPark3"), LEFT);
			GetLocation("trainPark1").AddConnectingLocation(GetLocation("trainPark2"), RIGHT);

			//trainPark2
			GetLocation("trainPark2").AddConnectingLocation(GetLocation("trainPark3"), BACKWARD);
            GetLocation("trainPark2").AddConnectingLocation(GetLocation("trainPark1"), LEFT);

            //trainPark3
            GetLocation("trainPark3").AddConnectingLocation(GetLocation("trainPark2"), BACKWARD);
            GetLocation("trainPark3").AddConnectingLocation(GetLocation("trainPark1"), RIGHT);

			//trainPark4
			GetLocation("trainPark4").AddConnectingLocation(GetLocation("trainPark5"), FORWARD);
			GetLocation("trainPark4").AddConnectingLocation(GetLocation("trainPark1"), BACKWARD);

			//trainPark5
			GetLocation("trainPark5").AddConnectingLocation(GetLocation("trainPark6"), FORWARD);
			GetLocation("trainPark5").AddConnectingLocation(GetLocation("trainPark4"), BACKWARD);

			//trainPark6
			GetLocation("trainPark6").AddConnectingLocation(GetLocation("toTrain6"), FORWARD);
			GetLocation("trainPark6").AddConnectingLocation(GetLocation("trainPark5"), BACKWARD);

			//side1
			GetLocation("side1").AddConnectingLocation(GetLocation("side2"), FORWARD);
			GetLocation("side1").AddConnectingLocation(GetLocation("tunnel3"), BACKWARD);

			//side2
			GetLocation("side2").AddConnectingLocation(GetLocation("side3"), FORWARD);
			GetLocation("side2").AddConnectingLocation(GetLocation("side1"), BACKWARD);

			//side3
			GetLocation("side3").AddConnectingLocation(GetLocation("westSide5"), FORWARD);
			GetLocation("side3").AddConnectingLocation(GetLocation("side2"), BACKWARD);

			//EN
			GetLocation("EN Building").AddConnectingLocation(GetLocation("toEN4"), BACKWARD);
		}

		private static void LoadSounds()
		{
			_sounds.Add("incorrect", new SoundEffect("incorrect", "wrong_direction.wav"));
			_sounds.Add("correct", new SoundEffect("correct", "correct_direction.wav"));
			_sounds.Add("menuSelect", new SoundEffect("menuSelect", "menu_select.wav"));
			_sounds.Add("toggleMap", new SoundEffect("toggleMap", "openMap.wav"));
			_sounds.Add("objectiveComplete", new SoundEffect("objectiveComplete", "objectiveComplete.wav"));
		}

		private static void LoadMusic()
		{
			_music.Add("bgm", new Music("bgm", "sogno.mp3"));
			_music.Add("USSR", new Music("USSR", "USSR.ogg"));
		}

		private static void LoadFonts()
		{
			_fonts.Add("gameFont", new Font("gameFont", "gameFont.ttf"));
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

		public static Location GetLocation(string locationName) {
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
