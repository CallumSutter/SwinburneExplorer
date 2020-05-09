using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer
{
    public class UI:IDraw
    {
		private const double LOC_X_SCALING = (double)GameController.WINDOW_WIDTH / 1300;
		private const double LOC_Y_SCALING = (double)GameController.WINDOW_HEIGHT / 614;

		private const int LOC_IMAGE_X_OFFSET = GameController.WINDOW_WIDTH - 1300;
		private const int LOC_IMAGE_Y_OFFSET = GameController.WINDOW_HEIGHT - 614;

        public void Draw()
        {
			/*
			 * Note: Image sizes are: 1300x614
			 */

			//draws players location
            GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, LOC_IMAGE_X_OFFSET / 2, LOC_IMAGE_Y_OFFSET / 2, SplashKit.OptionScaleBmp(LOC_X_SCALING,LOC_Y_SCALING));

            //draws current location
            GameController.gameWindow.DrawRectangle(Color.Black, GameController.WINDOW_WIDTH/2 - 150, 0, 300, 50);
            GameController.gameWindow.FillRectangle(Color.Black, GameController.WINDOW_WIDTH / 2 - 150, 0, 300, 50);
            string _location = "Current Location: " + GameController._player.Location.Name;
            GameController.gameWindow.DrawText(_location, Color.White, GameController.WINDOW_WIDTH / 2 - 120, 20);

            //draw map
            GameController.theMap.Draw();

            //draw objectives

            //draws arrows
            GameResources.DrawDirectionArrows();

            //target 60 fps
            GameController.gameWindow.Refresh(60);
        }
        
    }
}
