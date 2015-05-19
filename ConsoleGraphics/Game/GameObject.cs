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
        public Level Level;
        public Space Space;
        public float Rotation;
        public float Depth;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Size;
        public Vector2 Center
        {
            get { return new Vector2(Position.X + Size.X / 2f, Position.Y + Size.Y / 2f); }
            set { Position = new Vector2(value.X - Size.X / 2, value.Y - Size.Y / 2); }
        }
        public bool MouseDown;
        public bool MouseOver;

        public GameObject()
        {
            Init(Vector2.Empty, Vector2.Empty);
        }

        public GameObject(Space space)
        {
            Level = space.Level;
            Space = space;
            Init(Vector2.Empty, Vector2.Empty);
        }

        public GameObject(Space space, Vector2 position, Vector2 size)
        {
            Level = space.Level;
            Space = space;
            Init(position, size);
        }

        private void Init(Vector2 position, Vector2 size)
        {
            Rotation = 0f;
            Depth = 0f;
            Position = position;
            Velocity = new Vector2(0, 0);
            Size = size;
        }

        public bool Contains(Vector2 point)
        {
            return point.X >= Position.X && point.X <= Position.X + Size.X && 
                   point.Y >= Position.Y && point.Y <= Position.Y + Size.Y;
        }

        public virtual void Destroy()
        {
            EventManager.Unhook(this);
        }
    }
}
