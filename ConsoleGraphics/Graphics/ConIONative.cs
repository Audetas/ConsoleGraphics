using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        private short X;
        private short Y;

        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };

    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        [FieldOffset(0)]
        public char UnicodeChar;
        [FieldOffset(0)]
        public byte AsciiChar;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        [FieldOffset(0)]
        public CharUnion Char;
        [FieldOffset(2)]
        public short Attributes;

        public CharInfo(char c, short color)
        {
            Char = new CharUnion();
            Char.AsciiChar = (byte)c;
            Attributes = color;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        private short Left;
        private short Top;
        private short Right;
        private short Bottom;
        public void SetDrawCord(short l, short t)
        {
            Left = l;
            Top = t;
        }
        public void SetWindowSize(short r, short b)
        {
            Right = r;
            Bottom = b;
        }
    }

    public static class ConIONative
    {
        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);
    }
}
