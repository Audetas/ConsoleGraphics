using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Net
{
    public static class ExtensionMethods
    {
        public static byte[] SetUInt16(this byte[] data, int index, UInt16 number)
        {
            byte[] temp = data;
            byte[] b = BitConverter.GetBytes(number);

            Buffer.BlockCopy(b, 0, temp, index, b.Length);
            return temp;
        }

        public static byte[] SetUInt32(this byte[] data, int index, uint number)
        {
            byte[] temp = data;
            byte[] b = BitConverter.GetBytes(number);

            Buffer.BlockCopy(b, 0, temp, index, b.Length);
            return temp;
        }

        public static byte[] SetFloat(this byte[] data, int index, double number)
        {
            byte[] temp = data;
            byte[] b = BitConverter.GetBytes(number);

            Buffer.BlockCopy(b, 0, temp, index, b.Length);
            return temp;
        }
    }
}
