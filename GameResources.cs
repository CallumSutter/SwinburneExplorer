using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Resources
{
	public static class GameResources
	{
		private static Dictionary<string, Font> _fonts;
		private static Dictionary<string, SoundEffect> _sounds;
		private static Dictionary<string, Bitmap> _images;
		private static Dictionary<string, Music> _music;

		private static Font _loadingFont;

		static GameResources()
		{
			_fonts = new Dictionary<string, Font>();
			_sounds = new Dictionary<string, SoundEffect>();
			_images = new Dictionary<string, Bitmap>();
			_music = new Dictionary<string, Music>();
			_loadingFont = new Font("arial", "/arial.ttf");
		}

		public static void PlayBGM()
		{
			if(SplashKit.MusicPlaying())
			{
				SplashKit.StopMusic();
			}

			SplashKit.PlayMusic(GetMusic("bgm"));
		}

		public static void LoadingScreen()
		{
			Rectangle loadbar = new Rectangle();
			loadbar.Height = 13;
			loadbar.Width = 100;
			loadbar.X = Program.WINDOW_WIDTH / 2;
			loadbar.Y = Program.WINDOW_HEIGHT / 2;

			SplashKit.Delay(250);
			Program.gameWindow.FillRectangle(Color.LimeGreen ,loadbar);
			Program.gameWindow.Refresh();
			LoadFonts();

			loadbar.X += loadbar.Width;

			SplashKit.Delay(250);
			Program.gameWindow.FillRectangle(Color.LimeGreen, loadbar);
			Program.gameWindow.Refresh();
			LoadImages();

			loadbar.X += loadbar.Width;

			SplashKit.Delay(250);
			Program.gameWindow.FillRectangle(Color.LimeGreen, loadbar);
			Program.gameWindow.Refresh();
			LoadMusic();

			loadbar.X += loadbar.Width;

			SplashKit.Delay(250);
			Program.gameWindow.FillRectangle(Color.LimeGreen, loadbar);
			Program.gameWindow.Refresh();
			LoadSounds();
		}

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
			//_images.Add("loadbar", new Bitmap("bmp", "path"));
		}

		private static void LoadSounds()
		{
			//_sounds.Add("soundName", new SoundEffect("soundName", "Path"));
		}

		private static void LoadMusic()
		{
			_music.Add("bgm", new Music("bgm", "sogno.mp3"));
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
