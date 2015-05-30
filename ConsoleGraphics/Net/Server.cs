using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Net
{
    public class Server
    {
        public string Name;
        public int Players;
        public int Realms;
        public int Servers;

        public override string ToString()
        {
            return Name + "[" + Players + "]";
        }
    }
}
