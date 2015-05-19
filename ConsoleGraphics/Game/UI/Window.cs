using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class Window : UIObject
    {
        private short _foreColor = 20;
        public Window(Space space, Style style) : base(space, style) 
        {
            EventManager.Hook(Space, "Update", Update);
            EventManager.Hook(Space, "Draw", Draw);
            Size = new Vector2(20, 30);
            EventManager.Hook(this.Space, "MouseOver", MouseOver);
            Depth = 1;
        }

        public Window(UIObject parent, Style style) : base(parent, style) 
        {
            EventManager.Hook(Space, "Draw", Draw);
        }

        private void Update(object sender, Event e)
        {
            Center = Mouse.Position;
            Space.Camera.Center = Center;
        }

        private void MouseOver(object sender, Event e)
        {
            _foreColor = 60;
        }

        private void Draw(object sender, Event e)
        {
            Renderer.DrawRect(this, 0, 0, (int)Size.X, (int)Size.Y, 'x', _foreColor);
        }
    }
}
