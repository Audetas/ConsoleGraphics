using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGraphics.Game;

namespace ConsoleGraphics.Events
{
    class EventListener
    {
        public Type SenderType;
        public string Listener;

        public EventListener(Type type, string listener)
        {
            SenderType = type;
            Listener = listener;
        }
    }

    public static class EventManager
    {
        private static Dictionary<Action<object, Event>, EventListener> _listeners;

        static EventManager()
        {
            _listeners = new Dictionary<Action<object, Event>, EventListener>();
        }

        public static void Hook(object sender, string listener, Action<object, Event> callback)
        {
            Type senderType = sender.GetType().BaseType != typeof(object)
                ? sender.GetType().BaseType
                : sender.GetType();

            lock (_listeners)
            {
                _listeners.Add(
                    callback,
                    new EventListener(senderType, listener));
            }
        }

        public static void Unhook(Action<object, Event> callback)
        {
            lock (_listeners)
            {
                _listeners.Remove(callback);
            }
        }

        public static void Unhook(GameObject parent)
        {
            lock (_listeners)
            {
                foreach (var key in _listeners.Keys)
                {
                    if (key.Target == parent)
                    {
                        _listeners.Remove(key);
                    }
                }
            }
        }

        public static void Fire(object sender, object target, string listener, Event e = null)
        {
            Type senderType = sender.GetType();
            foreach (var pair in _listeners.Where(p => (p.Value.SenderType == senderType && p.Value.Listener == listener && p.Key.Target == target)))
                pair.Key(sender, e == null ? new Event() : e);
        }

        public static void Fire(object sender, string listener, Event e = null)
        {
            Type senderType = sender.GetType();
            foreach (var pair in _listeners.Where(p => (p.Value.SenderType == senderType && p.Value.Listener == listener)))
                pair.Key(sender, e == null ? new Event() : e);
        }
    }
}
