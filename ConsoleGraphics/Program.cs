using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGraphics.Events;
using ConsoleGraphics.Game;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;

namespace ConsoleGraphics
{
    class Program
    {
        static GameObject DebugObject;

        static void Main(string[] args)
        {
            Console.Title = "Realm of the Mad God";
            Console.SetWindowSize((int)(Console.LargestWindowHeight / 3f * 2f * 1.3333f), (int)(Console.LargestWindowHeight / 3f * 2f * .75f));
            Renderer.Init();

            Space space = new Space();
            DebugObject = space.Create(new Vector2(25, 25), new Vector2(25, 25));
            EventManager.Hook(DebugObject_Update, typeof(UpdateEvent));
            EventManager.Hook(DebugObject_Draw, typeof(DrawEvent));

            while (true)
            {
                EventManager.Fire(new UserUpdateEvent());
                Renderer.BeginScene();
                EventManager.Fire(new UserDrawEvent());
                Renderer.EndScene();
            }
        }

        private static void DebugObject_Update(Event e)
        {
            DebugObject.Rotation += 0.01f;
        }

        private static void DebugObject_Draw(Event e)
        {
            Renderer.DrawLine(DebugObject, new Vector2(0, 0), new Vector2(25, 25));
        }
    }
}
