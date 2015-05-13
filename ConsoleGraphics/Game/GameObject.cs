using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;

namespace ConsoleGraphics.Game
{
    public class GameObject
    {
        public Space Space;
        public float Rotation;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Size;
        public Vector2 Center
        {
            get { return new Vector2(Position.X + Size.X / 2f, Position.Y + Size.Y / 2f); }
            set { Position = new Vector2(value.X - Size.X / 2, value.Y - Size.Y / 2); }
        }

        public GameObject(Space space)
        {
            Space = space;
            Rotation = 0f;
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            Size = new Vector2(0, 0);
        }

        public GameObject(Space space, Vector2 position, Vector2 size)
        {
            Space = space;
            Rotation = 0f;
            Position = position;
            Velocity = new Vector2(0, 0);
            Size = size;
        }

        public virtual void Destroy()
        {
            EventManager.Unhook(this);
        }
    }
}
