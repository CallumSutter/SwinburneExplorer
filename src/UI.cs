using System;
using System.Collections.Generic;
using System.Text;
using SplashKitSDK;

namespace Swinburneexplorer
{
    class UI:IDraw
    {
        public void Draw()
        {
            Window shapesWindow;
            shapesWindow = new Window("Swinburne Explorer", 800, 600);

            shapesWindow.Clear(Color.White);
            shapesWindow.DrawText("Welcome to Swinburne Explorer!", Color.Blue, 300, 300);
            shapesWindow.Refresh();

            SplashKit.Delay(5000);
        }
        
    }
}
