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

namespace ConsoleGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Graphics";
            Console.SetWindowSize((int)(Console.LargestWindowHeight / 3f * 2f * 1.3333f), (int)(Console.LargestWindowHeight / 3f * 2f * .75f));
            Renderer.Init();

            Level level = new Level();
            Window window = new Window(level.Space, Style.Default);
            int width = 10;
            int height = 10;
            Random r = new Random();
            for (int x = 0; x < 150; x += width)
            {
                for (int y = 0; y < 150; y += height)
                {
                    GameObject tile = level.Space.Create(new Vector2(x, y), new Vector2(width, height));
                    int color = (short)r.Next(30, 200);
                    EventManager.Hook(level.Space, "Draw", (s, e) =>
                    {
                        Renderer.FillRect(tile, 1, 1, width - 2, height - 2, ' ', (short)color);  
                    });
                }
            }

            while (true)
            {
                level.Update();
                level.Draw();
            }
        }
    }
}
