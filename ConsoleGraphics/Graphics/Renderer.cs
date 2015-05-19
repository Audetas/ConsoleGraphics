using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGraphics.Game;
using ConsoleGraphics.Util;

namespace ConsoleGraphics.Graphics
{
    public static class Renderer
    {
        public static int Width;
        public static int Height;

        public static Stopwatch BeginWatch = new Stopwatch();
        public static Stopwatch SceneWatch = new Stopwatch();
        public static Stopwatch EndWatch = new Stopwatch();

        private static SafeFileHandle _h;
        private static CharInfo[] _buf;
        private static SmallRect _rect;
        private static char[] _table;

        static Renderer()
        {
            _table = new char[10000];
            _table[(int)'☺'] = (char)1;
            _table[(int)'☻'] = (char)2;//☻ 2
            _table[(int)'♥'] = (char)3;//♥ 3
            _table[(int)'♬'] = (char)14;//♬ 14
            _table[(int)'►'] = (char)16;//► 16
            _table[(int)'◄'] = (char)17;//◄ 17
            _table[(int)'↑'] = (char)24;//↑ 24
            _table[(int)'↓'] = (char)25;//↓ 25
            _table[(int)'→'] = (char)26;//→ 26
            _table[(int)'←'] = (char)27;//← 27
            _table[(int)'▲'] = (char)30;//▲ 30
            _table[(int)'▼'] = (char)31;//▼ 31
            _table[(int)'«'] = (char)174;//« 174
            _table[(int)'»'] = (char)175;//» 175
            _table[(int)'░'] = (char)176;//░ 176
            _table[(int)'▒'] = (char)177;//▒ 177
            _table[(int)'▓'] = (char)178;//▓ 178
            _table[(int)'│'] = (char)179;//│ 179
            _table[(int)'║'] = (char)186;//║ 186
            _table[(int)'╗'] = (char)187;//╗ 187
            _table[(int)'╝'] = (char)188;//╝ 188
            _table[(int)'┐'] = (char)191;//┐ 191
            _table[(int)'└'] = (char)192;//└ 192
            _table[(int)'─'] = (char)196;//─ 196
            _table[(int)'╚'] = (char)200;//╚ 200
            _table[(int)'╔'] = (char)201;//╔ 201
            _table[(int)'═'] = (char)205;//═ 205
            _table[(int)'┘'] = (char)217;//┘ 217
            _table[(int)'┌'] = (char)218;//┌ 218
            _table[(int)'█'] = (char)219;//█ 219
            _table[(int)'▄'] = (char)220;//▄ 220
            _table[(int)'▌'] = (char)221;//▌ 221
            _table[(int)'▐'] = (char)222;//▐ 222
            _table[(int)'▀'] = (char)223;//▀ 223
            _table[(int)'∞'] = (char)236;//∞ 236
            _table[(int)'√'] = (char)251;//√ 251
        }

        public static void Init()
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Width = Console.WindowWidth; Height = Console.WindowHeight;

            // Create the buffer
            _h = ConIONative.CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            _buf = new CharInfo[Console.WindowWidth * Console.WindowHeight];
            _rect = new SmallRect();
            _rect.SetDrawCord(0, 0);
            _rect.SetWindowSize((short)Console.WindowWidth, (short)Console.WindowHeight);
        }

        public static void BeginScene()
        {
            BeginWatch.Restart();

            // Fill buffer
            for (int i = 0; i < _buf.Length; i++)
            {
                _buf[i].Attributes = 0;
                _buf[i].Char.UnicodeChar = (char)' ';
            }

            BeginWatch.Stop();
            SceneWatch.Restart();
        }

        public static void EndScene()
        {
            SceneWatch.Stop();
            EndWatch.Restart();

            // Write buffer to stdout
            ConIONative.WriteConsoleOutput(_h, _buf, 
                new Coord((short)Console.WindowWidth, (short)Console.WindowHeight), 
                new Coord((short)0, (short)0), ref _rect);

            EndWatch.Stop();
        }

        #region Surface Writes
        private static void Draw(GameObject obj, double cos, double sin, float centerX, float centerY, int x, int y, char c, short color)
        {
            if (obj.Rotation == 0f)
            {
                int newX = (int)(obj.Position.X + x);
                int newY = (int)(obj.Position.Y + y);
                if (newX > Width - 1 || newX < 0 || newY > Height - 1 || newY < 0) return;
                _buf[newX + Width * newY] = new CharInfo(c > 10000 ? c : _table[c], color);
            }
            else
            {
                int dx = (int)(obj.Position.X + x - centerX);
                int dy = (int)(obj.Position.Y + y - centerY);
                int newX = (int)(cos * dx - sin * dy + centerX);
                int newY = (int)(sin * dx + cos * dy + centerY);
                if (newX < 0 || newY < 0 ||
                    newX > Width - 1 || newY > Height - 1 ||
                    newX > obj.Position.X + obj.Size.X || newY > obj.Position.Y + obj.Size.Y) return;
                //if (newX > Width - 1 || newX < 0 || newY > Height - 1 || newY < 0) return;
                _buf[newX + Width * newY] = new CharInfo(c > 10000 ? c : _table[c], color);
            }
        }

        public static void Draw(GameObject obj, string text, int x, int y, short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            for (int i = 0; i < text.Length; i++)
                Draw(obj, cos, sin, center.X, center.Y, x + i, y, text[i], color);
        }

        public static void DrawLine(GameObject obj, Vector2 pt1, Vector2 pt2, char c = ' ', short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);
            int x1 = (int)pt1.X; int y1 = (int)pt1.Y;
            int x2 = (int)pt2.X; int y2 = (int)pt2.Y;
            int w = x2 - x1;
            int h = y2 - y1;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                Draw(obj, cos, sin, center.X, center.Y, x1, y1, c, color);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x1 += dx1;
                    y1 += dy1;
                }
                else
                {
                    x1 += dx2;
                    y1 += dy2;
                }
            }
        }

        public static void DrawRect(GameObject obj, int x, int y, int width, int height, char c = ' ', short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            for (byte i = 0; i < width; i++) //TOP
                Draw(obj, cos, sin, center.X, center.Y, x + i, y, c, color);

            for (byte i = 1; i < width; i++) //BOTTOM
                Draw(obj, cos, sin, center.X, center.Y, x + i, y + height - 1, c, color);

            for (byte i = 0; i < height; i++) //LEFT
                Draw(obj, cos, sin, center.X, center.Y, x, y + i, c, color);

            for (byte i = 0; i < height; i++) //RIGHT
                Draw(obj, cos, sin, center.X, center.Y, x + width - 1, y + i, c, color);
        }

        public static void FillRect(GameObject obj, int x, int y, int width, int height, char c = ' ', short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            for (int rx = 0; rx < width; rx++)
                for (int ry = 0; ry < height; ry++)
                    Draw(obj, cos, sin, center.X, center.Y, rx + x, ry + y, c, color);
        }

        public static void DrawCircle(GameObject obj, int x, int y, float radius, char c = ' ', short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            for (double i = 0.0; i < 360.0; i += 0.1)
            {
                double angle = i * System.Math.PI / 180;
                int newX = (int)(radius * Math.Cos(angle));
                int newY = (int)(radius * Math.Sin(angle));
                Draw(obj, cos, sin, center.X, center.Y, newX + x, newY + y, c, color);
            }
        }

        public static void FillCircle(GameObject obj, int x, int y, float radius, char c = ' ', short color = 1)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            int r2 = (int)(radius * radius);
            int area = r2 << 2;
            int rr = (int)radius << 1;

            for (int i = 0; i < area; i++)
            {
                int tx = (i % rr) - (int)radius;
                int ty = (i / rr) - (int)radius;

                if (tx * tx + ty * ty <= r2)
                    Draw(obj, sin, cos, center.X, center.Y, x + tx, y + ty, c, color);
            }
        }
        #endregion
    }
}
