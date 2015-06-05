using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Net
{
    class PacketBuffer
    {
        public static int BUFFER_SIZE = 10000;
        public byte[] Buffer = new byte[BUFFER_SIZE];
        public int Received = 0;

        public void Dispose()
        {
            Buffer = null;
        }
    }
}
