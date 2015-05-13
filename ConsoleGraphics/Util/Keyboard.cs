using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleGraphics.Util
{
    static class Keyboard
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        public static bool IsPressed(Keys key)
        {
            return GetAsyncKeyState(key) != 0;
        }
    }
}
