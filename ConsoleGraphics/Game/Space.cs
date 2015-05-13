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
        public Camera Camera;
        public List<GameObject> Objects;

        public Space()
        {
            Camera = new Camera(this);
            Objects = new List<GameObject>();
            EventManager.Hook(UserUpdate, typeof(UserUpdateEvent));
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

        private void UserUpdate(Event e)
        {
            EventManager.Fire(new UpdateEvent());
        }
    }
}
