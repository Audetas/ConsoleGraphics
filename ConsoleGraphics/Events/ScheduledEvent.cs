using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Events
{
    public class ScheduledEvent
    {
        public int Start;
        public int Delay;
        public bool Recurring;
        public bool PoolCallback;
        public Action<int> Callback;
    }
}
