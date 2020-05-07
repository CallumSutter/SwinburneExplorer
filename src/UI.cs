using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using SplashKitSDK;

namespace Swinburneexplorer
{
    public class UI:IDraw
    {
        public void Draw()
        {
			//draws players location
            GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, -1613, -760, SplashKit.OptionScaleBmp(0.3,0.3));

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
