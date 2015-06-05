using ConsoleGraphics.Util;
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
        public static float SCALE = 0.01f;

        public int Id;
        public ushort X;
        public ushort Y;
        public ushort Size;
        public byte Red;
        public byte Green;
        public byte Blue;
        public byte Flags;
        public bool Virus = false;
        public string Name;

        public NodeData() { }

        public NodeData(BinaryReader r)
        {
            Id = r.ReadInt32();
            if (Id == 0) return;

            X = (ushort)(r.ReadUInt16() / SCALE);
            Y = (ushort)(r.ReadUInt16() / SCALE);
            Size = (ushort)(r.ReadUInt16() / SCALE);
            Red = r.ReadByte();
            Green = r.ReadByte();
            Blue = r.ReadByte();
            Flags = r.ReadByte();
            Virus = ((Flags & 1) != 0);

            if (Convert.ToBoolean(Flags & 2)) r.ReadBytes(2);
            if (Convert.ToBoolean(Flags & 2)) r.ReadBytes(4);
            if (Convert.ToBoolean(Flags & 2)) r.ReadBytes(8);

            while (true)
            {
                var n = r.ReadUInt16();
                if (n == 0) break;
                Name += Convert.ToChar(n);
            }
        }
    }
}
