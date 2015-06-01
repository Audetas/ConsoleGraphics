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
    public static partial class Renderer
    {
        public static int Width;
        public static int Height;
        public static int FPS = 0;
        private static SafeFileHandle _h;
        private static CharInfo[] _buf;
        private static SmallRect _rect;
        private static byte[] _table;
        private static string[][] _map;
        private static char _whiteSpace = '_';
        private static int _drawnFrames = 0;
        private static int _lastTime = 0;

        static Renderer()
        {
            _initLookupTable();
            _initFontMap();
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
            // Fill buffer
            for (int i = 0; i < _buf.Length; i++)
            {
                _buf[i].Attributes = 0xFF;
                _buf[i].Char.UnicodeChar = (char)' ';
            }
        }

        public static void EndScene()
        {
            // Write buffer to stdout
            ConIONative.WriteConsoleOutput(_h, _buf, 
                new Coord((short)Console.WindowWidth, (short)Console.WindowHeight), 
                new Coord((short)0, (short)0), ref _rect);

            _drawnFrames++;
            if (Environment.TickCount - _lastTime > 1000)
            {
                FPS = _drawnFrames;
                _drawnFrames = 0;
                _lastTime = Environment.TickCount;
            }
        }

        #region Core Renderer
        // Renderer cache
        private static GameObject __obj;
        private static double __cos;
        private static double __sin;
        private static double __cX;
        private static double __cY;
        //
        private static void _render(GameObject obj, float x, float y, char character, short color)
        {
            int newX, newY;
            if (obj.Rotation == 0f)
            {
                newX = (int)(obj.X + x);
                newY = (int)(obj.Y + y);
                goto Render;
            }
            else if (obj == __obj)
            {
                //goto Compute;
            }

        Cache:
            __obj = obj;
            __cos = Math.Cos(obj.Rotation);
            __sin = Math.Sin(obj.Rotation);
            __cX = obj.Size.X / 2.0 + obj.X;
            __cY = obj.Size.Y / 2.0 + obj.Y;

        Compute:
            double dx = obj.X + x;
            double dy = obj.Y + y;
            newX = (int)(__cos * dx - __sin * dy + __cX);
            newY = (int)(__sin * dx + __cos * dy + __cY);

        Render:
            if (newX > Width - 1 || newX < 0 || newY > Height - 1 || newY < 0) return;
            int position = newX + Width * newY;
            _buf[position].Attributes = color;
            _buf[position].Char.AsciiChar = character > 9000 && character < 10000
                ? (byte)_table[character]
                : (byte)character;
        }

        private static void _render(int x, int y, char character, short color)
        {
            if (x > Width - 1 || x < 0 || y > Height - 1 || y < 0) return;
            int position = x + Width * y;
            _buf[position].Attributes = color;
            _buf[position].Char.AsciiChar = character > 9000 && character < 10000
                ? (byte)_table[character]
                : (byte)character;
        }

        private static void _renderOLD(GameObject obj, int x, int y, char c, short color)
        {
            Vector2 center = obj.Center;
            double cos = Math.Cos(obj.Rotation);
            double sin = Math.Sin(obj.Rotation);

            if (obj.Rotation == 0f)
            {
                int newX = (int)(obj.X + x);
                int newY = (int)(obj.Y + y);
                if (newX > Width - 1 || newX < 0 || newY > Height - 1 || newY < 0) return;
                _buf[newX + Width * newY].Char.AsciiChar = c < 9000 || c > 10000 ? (byte)c : _table[c];
                _buf[newX + Width * newY].Attributes = color;
            }
            else
            {
                double dx = obj.X + x - center.X;
                double dy = obj.Y + y - center.Y;
                int newX = (int)(cos * dx - sin * dy + center.X);
                int newY = (int)(sin * dx + cos * dy + center.Y);
                if (newX < 0 || newY < 0 ||
                    newX > Width - 1 || newY > Height - 1 ||
                    newX > obj.X + obj.Width || newY > obj.Y + obj.Width) return;
                //if (newX > Width - 1 || newX < 0 || newY > Height - 1 || newY < 0) return;
                _buf[newX + Width * newY].Char.AsciiChar = c < 9000 || c > 10000 ? (byte)c : _table[c];
                _buf[newX + Width * newY].Attributes = color;
            }
        }

        #endregion

        #region Surface Writes
        public static void Draw(string text, int x, int y, short color = 1)
        {
            for (int i = 0; i < text.Length; i++)
                _render(x + i, y, text[i], color);
        }

        public static void Draw(GameObject obj, string text, int x, int y, short color = 1)
        {
            for (int i = 0; i < text.Length; i++)
                _render(obj, x + i, y, text[i], color);
        }

        public static void Render(GameObject obj, string text, int x, int y, short color = 1)
        {
            int fontWidth = _map[(int)text[0]][0].Length;
            int fontHeight = _map[(int)text[0]].Length;
            
            for (int cIndex = 0; cIndex < text.Length; cIndex++)
                for (int dy = 0; dy < fontHeight; dy++)
                    for (int dx = 0; dx < fontWidth; dx++)
                    {
                        char c = _map[(int)text[cIndex]][dy][dx];
                        if (c != _whiteSpace)
                            _renderOLD(obj, x + (cIndex * fontWidth) + dx + cIndex, y + dy, c, color);
                    }
        }

        public static Vector2 Measure(string text)
        {
            int fontWidth = _map[(int)text[0]][0].Length;
            int fontHeight = _map[(int)text[0]].Length;
            return new Vector2(fontWidth * text.Length + text.Length, fontHeight);
        }

        public static void DrawLine(GameObject obj, Vector2 pt1, Vector2 pt2, char c = ' ', short color = 1)
        {
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
                _render(obj, x1, y1, c, color);
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
            char side = c;
            char top = c;

            if (c == '║' || c == '═')
            {
                side = '║'; top = '═';
            }
            if (c == '│' || c == '─')
            {
                side = '│'; top = '─';
            }

            for (byte i = 0; i <= width; i++) //TOP
                _render(obj, x + i, y, top, color);

            for (byte i = 1; i <= width; i++) //BOTTOM
                _render(obj, x + i, y + height, top, color);

            for (byte i = 0; i <= height; i++) //LEFT
                _render(obj, x, y + i, side, color);

            for (byte i = 0; i <= height; i++) //RIGHT
                _render(obj, x + width, y + i, side, color);

            if (c == '║' || c == '═')
            {
                _render(obj, 0, 0, '╔', color); // Top Left
                _render(obj, width, 0, '╗', color); // Top Right
                _render(obj, 0, height, '╚', color); // Bottom Left
                _render(obj, width, height, '╝', color); // Bottom Right
            }
            else if (c == '│' || c == '─')
            {
                _render(obj, 0, 0, '┌', color); // Top Left
                _render(obj, width, 0, '┐', color); // Top Right
                _render(obj, 0, height, '└', color); // Bottom Left
                _render(obj, width, height, '┘', color); // Bottom Right
            }
        }

        public static void FillRect(GameObject obj, int x, int y, int width, int height, char c = ' ', short color = 1)
        {
            for (int rx = 0; rx <= width; rx++)
                for (int ry = 0; ry <= height; ry++)
                    _render(obj, rx + x, ry + y, c, color);
        }

        public static void DrawCircle(GameObject obj, int x, int y, float radius, char c = ' ', short color = 1)
        {
            for (double i = 0.0; i < 360.0; i += 50.0 / radius)
            {
                double angle = i * Math.PI / 180;
                int newX = (int)(radius * Math.Cos(angle));
                int newY = (int)(radius * Math.Sin(angle));
                _render(obj, newX + x, newY + y, c, color);
            }
        }

        public static void FillCircle(GameObject obj, int x, int y, float radius, char c = ' ', short color = 1)
        {
            for (float dy = -radius; dy <= radius; dy++)
                for (float dx = -radius; dx <= radius; dx++)
                    if (dx * dx + dy * dy <= radius * radius)
                        _render(obj, x + dx, y + dy, c, color);
        }

        public static void FillCircleOLD(GameObject obj, int x, int y, float radius, char c = ' ', short color = 1)
        {
            int r2 = (int)(radius * radius);
            int area = r2 << 2;
            int rr = (int)radius << 1;

            for (int i = 0; i < area; i++)
            {
                int tx = (i % rr) - (int)radius;
                int ty = (i / rr) - (int)radius;

                if (tx * tx + ty * ty <= r2)
                    _render(obj, x + tx, y + ty, c, color);
            }
        }
        #endregion
    }
}
