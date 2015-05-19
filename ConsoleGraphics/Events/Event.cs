using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Events
{
    public class Event
    {
        public object Sender;
        public string Name;

        public void Fire(object sender, string listener)
        {
            EventManager.Fire(sender, listener, this);
        }
    }
}
