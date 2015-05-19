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
            EventManager.Hook(Level, "Draw", Draw);
            Size = new Vector2(Console.WindowWidth, Console.WindowHeight);
        }

        private void Draw(object sender, Event e)
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

            Space.Objects = Space.Objects.OrderBy(go => go.Depth).ToList();
            EventManager.Fire(this.Space, "Draw", new Event());

            foreach (GameObject go in Space.Objects)
            {
                go.Position += this.Position;
                go.Rotation -= this.Rotation;
            }
        }
    }
}
