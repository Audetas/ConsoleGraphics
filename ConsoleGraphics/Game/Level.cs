using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game
{
    public class Level
    {
        public Space Space;

        public Level()
        {
            Space = new Space(this);
        }

        public void Update()
        {
            EventManager.Fire(this, "Update", new Event());
        }

        public void Draw()
        {
            Renderer.BeginScene();
            EventManager.Fire(this, "Draw", new Event());
            Renderer.EndScene();
        }
    }
}
