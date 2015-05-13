using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleGraphics.Util
{
    static class Mouse
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        const int MOUSE_LEFT = 0x01;
        const int MOUSE_RIGHT = 0x02;

        static IntPtr _hConsole;

        static Mouse()
        {
            _hConsole = GetConsoleWindow();
        }

        public static bool Left
        {
            get
            {
                return GetAsyncKeyState(MOUSE_LEFT) != 0;
            }
        }

        public static bool Right
        {
            get
            {
                return GetAsyncKeyState(MOUSE_RIGHT) != 0;
            }
        }

        public static Vector2 Position
        {
            get
            {
                Point conPoint = new Point();
                Rect conSize = new Rect();
                GetClientRect(_hConsole, out conSize);
                ClientToScreen(_hConsole, ref conPoint);

                return new Vector2(
                    (Cursor.Position.X - conPoint.X) / (float)conSize.right * Console.WindowWidth,
                    (Cursor.Position.Y - conPoint.Y) / (float)conSize.bottom * Console.WindowHeight);
            }
        }
    }
}
