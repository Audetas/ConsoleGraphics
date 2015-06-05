﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Net
{
    public class PacketReader : BinaryReader
    {
        public PacketReader(MemoryStream input)
            : base(input, Encoding.UTF8) { }

        public override short ReadInt16()
        {
            return IPAddress.NetworkToHostOrder(base.ReadInt16());
        }

        public override ushort ReadUInt16()
        {
            return (ushort)IPAddress.NetworkToHostOrder((short)base.ReadUInt16());
        }

        public override int ReadInt32()
        {
            return IPAddress.NetworkToHostOrder(base.ReadInt32());
        }

        public override uint ReadUInt32()
        {
            return (uint)IPAddress.NetworkToHostOrder((int)base.ReadUInt32());
        }

        public override float ReadSingle()
        {
            byte[] arr = base.ReadBytes(4);
            Array.Reverse(arr);
            return BitConverter.ToSingle(arr, 0);
        }

        public override double ReadDouble()
        {
            
            byte[] arr = base.ReadBytes(8);
            Array.Reverse(arr);
            return BitConverter.ToSingle(arr, 0);
        }
    }
}
