using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGraphics.Events;
using ConsoleGraphics.Game;
using ConsoleGraphics.Util;

namespace ConsoleGraphics.Graphics
{
    public class Camera : GameObject
    {
        public Camera(Space space) : base(space)
        {
            EventManager.Hook(UserDraw, typeof(UserDrawEvent));
        }

        private void UserDraw(Event e)
        {
            // This may seem like a cheap or dirty way to do this,
            // But it's actually the cheapest way of calculating the viewport.

            // Changing the position temporarily has no negative effects as all 
            // object updating is gauranteed to be done before the scene is drawn.
            foreach (GameObject go in Space.Objects)
            {
                go.Position -= this.Position;
                go.Rotation += this.Rotation;
            }

            EventManager.Fire(new DrawEvent());

            foreach (GameObject go in Space.Objects)
            {
                go.Position += this.Position;
                go.Rotation -= this.Rotation;
            }
        }
    }
}
