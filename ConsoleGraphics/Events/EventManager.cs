using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGraphics.Game;

namespace ConsoleGraphics.Events
{
    public static class EventManager
    {
        private static Thread _worker;
        private static Dictionary<Action<Event>, Type> _listeners;
        private static Queue<ScheduledEvent> _eventQueue;
        private static List<ScheduledEvent> _events;

        static EventManager()
        {
            _listeners = new Dictionary<Action<Event>, Type>();
            _eventQueue = new Queue<ScheduledEvent>();
            _events = new List<ScheduledEvent>();

            _worker = new Thread(DoWork);
            _worker.IsBackground = true;
            _worker.Start();
        }

        public static void Hook(Action<Event> callback, Type eventType)
        {
            lock (_listeners)
                _listeners.Add(callback, eventType);
        }

        public static void Unhook(Action<Event> callback, Type eventType = null)
        {
            lock (_listeners)
            {
                if (eventType == null || _listeners[callback] == eventType)
                    _listeners.Remove(callback);
            }
        }

        public static void Unhook(GameObject parent)
        {
            lock (_listeners)
            {
                foreach (var pair in _listeners)
                    if (pair.Key.Target == parent)
                    {
                        _listeners.Remove(pair.Key);
                        break;
                    }
            }
        }

        public static void Schedule(ScheduledEvent s)
        {
            lock (_eventQueue)
                _eventQueue.Enqueue(s);
        }

        public static void Fire(Event e)
        {
            Type eventType = e.GetType();
            foreach (var pair in _listeners.Where(p => p.Value == eventType))
                pair.Key(e);
        }

        private static void DoWork()
        {
            while (true)
            {
                // TODO: Accurate Sleep
                Thread.Sleep(1);

                lock (_eventQueue)
                {
                    _events.AddRange(_eventQueue.ToArray());
                    _eventQueue.Clear();
                }

                for (int i = _events.Count; i > 0; i--)
                {
                    ScheduledEvent e = _events[i];
                    if (Environment.TickCount - e.Start >= e.Delay)
                    {
                        if (e.PoolCallback)
                            Task.Run(() => e.Callback(Environment.TickCount));
                        else
                            e.Callback(Environment.TickCount); // This is running on the eventmanager thread... look into

                        if (e.Recurring)
                            e.Start = Environment.TickCount;
                        else
                            _events.RemoveAt(i);
                    }
                }
            }
        }
    }
}
