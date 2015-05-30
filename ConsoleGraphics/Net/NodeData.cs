using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Net
{
    class NodeData
    {
        public int Id;
        public double X;
        public double Y;
        public double Size;
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Flags;
        public bool Virus = false;
        public string Name;

        public NodeData(BinaryReader r)
        {
            Id = r.ReadInt32();
            if (Id == 0) return;

            X = r.ReadDouble();
            Y = r.ReadDouble();
            Size = r.ReadDouble();
            Red = r.ReadByte();
            Green = r.ReadByte();
            Blue = r.ReadByte();
            Flags = r.ReadByte();
            /*
            if      ((1 & Flags) != 0) Virus = true;
            else if ((2 & Flags) != 0) r.ReadBytes(4);
            else if ((8 & Flags) != 0) r.ReadBytes(8);
            else if ((128 & Flags) != 0) r.ReadBytes(16);*/
            Virus = ((Flags & 0) == 1);
            //if ((Flags & 2) == 0) r.ReadBytes(4);
            //if ((Flags & 4) == 0) r.ReadBytes(8);
            //if ((Flags & 8) == 0) r.ReadBytes(16);

            while (true)
            {
                var n = r.ReadUInt16();
                if (n == 0) break;
                Name += Convert.ToChar(n);
            }
        }
    }
}
