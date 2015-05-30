using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Net;
using ConsoleGraphics.Util;
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

        public virtual void Update()
        {
            foreach (GameObject go in Space.Objects) go.Update();

            foreach (UIObject uo in Space.UIObjects) 
                if (!uo.Style.Hidden && uo.Style.Enabled) uo.Update();
        }

        public virtual void Draw()
        {
            Space.Camera.Draw();
            foreach (UIObject uo in Space.UIObjects) 
                if (!uo.Style.Hidden) uo.Draw();
        }
    }
}
