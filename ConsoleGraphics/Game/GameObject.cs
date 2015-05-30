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
        // Public independent members
        public Level Level;
        public Space Space;
        public GameObject Parent = null;
        public Vector2 Size;
        public Vector2 Velocity;
        // State members
        public bool RestrainDrawSpace = true;
        public bool MouseDown = false;
        public bool MouseOver = false;
        // Private raw members
        private float _rotation;
        private float _depth;
        private Vector2 _position;
        // Public dependent members
        public float Rotation
        {
            get { return Parent == null ? _rotation : Parent.Rotation + _rotation; }
            set { _rotation = value; }
        }
        public float Depth
        {
            get { return Parent == null ? _depth : Parent.Depth + _depth; }
            set { _depth = value; }
        }
        public float X
        {
            get { return Parent == null ? _position.X : Parent.X + _position.X; }
            set { _position.X = value; }
        }
        public float Y
        {
            get { return Parent == null ? _position.Y : Parent.Y + _position.Y; }
            set { _position.Y = value; }
        }
        public float LocalX
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        public float LocalY
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }
        public Vector2 Position
        {
            get { return Parent == null ? _position : Parent.Position + _position; }
            set { _position = value; }
        }
        public Vector2 LocalPosition
        {
            get { return _position; }
            set { _position = value; }
        }
        public int Width
        {
            get { return (int)Size.X; }
            set { Size.X = value; }
        }
        public int Height
        {
            get { return (int)Size.Y; }
            set { Size.Y = value; }
        }
        public Vector2 Center
        {
            get { return Position + new Vector2(Width / 2f, Height / 2f); }
            set { _position = value - new Vector2(Width / 2f, Height / 2f); }
        }

        public GameObject()
        {
            Init(Vector2.Empty, Vector2.Empty);
        }

        public GameObject(Space space)
        {
            Level = space.Level;
            Space = space;
            if (this is UIObject) Space.UIObjects.Add((UIObject)this);
            else Space.Objects.Add(this);
            Init(Vector2.Empty, Vector2.Empty);
        }

        public GameObject(Space space, Vector2 position, Vector2 size)
        {
            Level = space.Level;
            Space = space;
            if (this is UIObject) Space.UIObjects.Add((UIObject)this);
            else Space.Objects.Add(this);
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
            return point.X >= X && point.X <= X + Width && 
                   point.Y >= Y && point.Y <= Y + Height;
        }

        public virtual void Destroy()
        {
            EventManager.Unhook(this);
        }

        public virtual void Update()
        {
            MouseOver = Contains(Mouse.Position);
            MouseDown = MouseOver && Mouse.Left;
            //Position += Velocity;
        }

        public virtual void Draw()
        {
        }
    }
}
