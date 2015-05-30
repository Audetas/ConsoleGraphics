using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGraphics.Events;
using ConsoleGraphics.Game;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using ConsoleGraphics.Game.UI;
using ConsoleGraphics.Game.Levels;

namespace ConsoleGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "AGAR.STD.IO";
            Console.SetWindowSize((int)(Console.LargestWindowHeight * 1.3333f), (int)(Console.LargestWindowHeight * .75f));
            Renderer.Init();

            AgarioLevel level = new AgarioLevel();

            while (true)
            {
                level.Update();
                Renderer.BeginScene();
                level.Draw();
                Renderer.EndScene();
            }
        }
    }
}
