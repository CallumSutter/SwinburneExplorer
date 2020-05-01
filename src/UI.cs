using System;
using System.Collections.Generic;
using System.Text;
using Resources;
using SplashKitSDK;

namespace Swinburneexplorer
{
    public class UI:IDraw
    {
        public void Draw()
        {
            //draws players location
            GameController.gameWindow.DrawBitmap(GameController._player.Location.LocationImage, 0, 0);

            //draws current location
            GameController.gameWindow.DrawRectangle(Color.Black, 250, 0, 300, 50);
            GameController.gameWindow.FillRectangle(Color.Black, 250, 0, 300, 50);
            string _location = "Current Location: " + GameController._player.Location.Name;
            GameController.gameWindow.DrawText(_location, Color.White, 300, 20);

            //draw map

            //draw objectives

            //draws arrows
            GameResources.DrawDirectionArrows();

            //target 60 fps
            GameController.gameWindow.Refresh(60);
        }
        
    }
}
