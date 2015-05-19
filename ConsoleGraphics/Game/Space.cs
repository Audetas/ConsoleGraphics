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
    public class Space
    {
        public Level Level;
        public Camera Camera;
        public List<UIObject> UIObjects;
        public List<GameObject> Objects;

        public Space(Level level)
        {
            Level = level;
            Camera = new Camera(this);
            UIObjects = new List<UIObject>();
            Objects = new List<GameObject>();
            EventManager.Hook(level, "Update", Update);
        }

        public GameObject Create()
        {
            GameObject o = new GameObject(this);
            Objects.Add(o);
            return o;
        }

        public GameObject Create(Vector2 position, Vector2 size)
        {
            GameObject o = new GameObject(this, position, size);
            Objects.Add(o);
            return o;
        }

        private void Update(object sender, Event e)
        {
            // UI Updates
            foreach (UIObject uio in UIObjects)
            {
                if (uio.Contains(Mouse.Position))
                {
                    EventManager.Fire(this, uio, Mouse.Left ? "MouseDown" : "MouseMove");
                }
            }
            // Collision Updates
            EventManager.Fire(this, "Update");
        }
    }
}
