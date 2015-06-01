using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Graphics
{
    public static partial class Renderer
    {
        private static void _initLookupTable()
        {
            _table = new byte[10000];
            _table[(int)'☺'] = 1;
            _table[(int)'☻'] = 2;//☻ 2
            _table[(int)'♥'] = 3;//♥ 3
            _table[(int)'♬'] = 14;//♬ 14
            _table[(int)'►'] = 16;//► 16
            _table[(int)'◄'] = 17;//◄ 17
            _table[(int)'↑'] = 24;//↑ 24
            _table[(int)'↓'] = 25;//↓ 25
            _table[(int)'→'] = 26;//→ 26
            _table[(int)'←'] = 27;//← 27
            _table[(int)'▲'] = 30;//▲ 30
            _table[(int)'▼'] = 31;//▼ 31
            _table[(int)'«'] = 174;//« 174
            _table[(int)'»'] = 175;//» 175
            _table[(int)'░'] = 176;//░ 176
            _table[(int)'▒'] = 177;//▒ 177
            _table[(int)'▓'] = 178;//▓ 178
            _table[(int)'│'] = 179;//│ 179
            _table[(int)'║'] = 186;//║ 186
            _table[(int)'╗'] = 187;//╗ 187
            _table[(int)'╝'] = 188;//╝ 188
            _table[(int)'┐'] = 191;//┐ 191
            _table[(int)'└'] = 192;//└ 192
            _table[(int)'─'] = 196;//─ 196
            _table[(int)'╚'] = 200;//╚ 200
            _table[(int)'╔'] = 201;//╔ 201
            _table[(int)'═'] = 205;//═ 205
            _table[(int)'┘'] = 217;//┘ 217
            _table[(int)'┌'] = 218;//┌ 218
            _table[(int)'█'] = 219;//█ 219
            _table[(int)'▄'] = 220;//▄ 220
            _table[(int)'▌'] = 221;//▌ 221
            _table[(int)'▐'] = 222;//▐ 222
            _table[(int)'▀'] = 223;//▀ 223
            _table[(int)'∞'] = 236;//∞ 236
            _table[(int)'√'] = 251;//√ 251
        }


        private static void _initFontMap()
        {
            _map = new string[300][];

            _map[(int)'a'] = _map[(int)'A'] = new string[]
            {
               "▒▓▓▓▓▓▓",
               "▒▓▓_▓▓▓",
               "▒▓▓▓▓▓▓",
               "▒▓▓_▓▓▓",
               "▒▓▓_▓▓▓",
            };

            _map[(int)'b'] = _map[(int)'B'] = new string[]
            {
                
                "▒▓▓▓▓▓▓",
                "▒▓_▓▓▓▓",
                "▒▓▓▓▓▓_",
                "▒▓_▓▓▓▓",
                "▒▓▓▓▓▓▓",
            };

            _map[(int)'c'] = _map[(int)'C'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓",
                "▓▓▓▓___",
                "▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓",
            };

            _map[(int)'d'] = _map[(int)'D'] = new string[]
            {
                "▒▓▓▓▓▓_",
                "▒▓▓▓▓▓▓",
                "▒▓__▓▓▓",
                "▒▓▓▓▓▓▓",
                "▒▓▓▓▓▓_",
            };

            _map[(int)'e'] = _map[(int)'E'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "▓▓▓____",
                "▓▓▓▓▓__",
                "▓▓▓____",
                "▓▓▓▓▓▓▓",
            };

            _map[(int)'f'] = _map[(int)'F'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "▓▓▓____",
                "▓▓▓▓▓__",
                "▓▓_____",
                "▓▓_____",
            };

            _map[(int)'g'] = _map[(int)'G'] = new string[]
            {
                "▒▓▓▓▓▓▓",
                "▒▓_____",
                "▒▓___▓▓",
                "▒▓____▓",
                "▒▓▓▓▓▓▓",
            };

            _map[(int)'h'] = _map[(int)'H'] = new string[]
            {
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓▓▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
            };

            _map[(int)'i'] = _map[(int)'I'] = new string[]
            {
                "▒▓▓▓▓▓▓",
                "__▓▓▓__",
                "__▓▓▓__",
                "__▓▓▓__",
                "▒▓▓▓▓▓▓",
            };

            _map[(int)'j'] = _map[(int)'J'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "__▓▓▓__",
                "__▓▓▓__",
                "▓_▓▓▓__",
                "▓▓▓▓▓__",
            };

            _map[(int)'k'] = _map[(int)'K'] = new string[]
            {
                "▓▓▓__▓▓",
                "▓▓▓__▓▓",
                "▓▓▓▓▓▓_",
                "▓▓▓_▓▓_",
                "▓▓▓__▓▓",
            };

            _map[(int)'l'] = _map[(int)'L'] = new string[]
            {
                "▓▓▓____",
                "▓▓▓____",
                "▓▓▓____",
                "▓▓▓▓▓▓▓",
                "▓▓▓▓▓▓▓",
            };

            _map[(int)'m'] = _map[(int)'M'] = new string[]
            {
                "▓▓▓_▓▓▓",
                "▓▓▓▓▓▓▓",
                "▓▓_▓_▓▓",
                "▓▓___▓▓",
                "▓▓___▓▓",
            };

            _map[(int)'n'] = _map[(int)'N'] = new string[]
            {
                "▓▓▓___▓",
                "▓▓▓▓__▓",
                "▓▓▓▓▓▓▓",
                "▓__▓▓▓▓",
                "▓____▓▓",
            };

            _map[(int)'o'] = _map[(int)'O'] = new string[]
            {
                "▒▓▓▓▓▓▓_",
                "▒▓___▓▓",
                "▒▓___▓▓",
                "▒▓___▓▓",
                "▒▓▓▓▓▓▓",
            };

            _map[(int)'p'] = _map[(int)'P'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "▓▓▓__▓▓",
                "▓▓▓▓▓▓▓",
                "▓▓▓____",
                "▓▓▓____",
            };

            _map[(int)'q'] = _map[(int)'Q'] = new string[]
            {
                "▓▓▓▓▓▓▓_",
                "▓_____▓",
                "▓_____▓",
                "▓___▓▓_",
                "▓▓▓▓▓_▓",
            };

            _map[(int)'r'] = _map[(int)'R'] = new string[]
            {
                "▒▓▓▓▓▓▓",
                "▒▓▓▓_▓▓",
                "▒▓▓▓▓▓_",
                "▒▓▓__▓▓",
                "▒▓▓__▓▓",
            };

            _map[(int)'s'] = _map[(int)'S'] = new string[]
            {
                "▒▓▓▓▓▓▓",
                "▒▓▓____",
                "▒▓▓▓▓▓▓",
                "____▓▓▓",
                "▒▓▓▓▓▓▓",
            };

            _map[(int)'t'] = _map[(int)'T'] = new string[]
            {
                "▒▓▓▓▓▓▓",
                "▒▓▓▓▓▓▓",
                "__▓▓▓__",
                "__▓▓▓__",
                "__▓▓▓__",
            };

            _map[(int)'u'] = _map[(int)'U'] = new string[]
            {
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓▓▓▓▓",
            };

            _map[(int)'v'] = _map[(int)'V'] = new string[]
            {
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "_▓▓▓▓▓_",
                "__▓▓▓__",
            };

            _map[(int)'w'] = _map[(int)'W'] = new string[]
            {
                "▓_____▓",
                "▓__▓__▓",
                "▓_▓▓▓_▓",
                "▓▓▓_▓▓▓",
                "▓▓___▓▓",
            };

            _map[(int)'x'] = _map[(int)'X'] = new string[]
            {
                "▓▓___▓▓",
                "_▓▓_▓▓_",
                "__▓▓▓__",
                "_▓▓_▓▓_",
                "▓▓___▓▓",
            };

            _map[(int)'y'] = _map[(int)'Y'] = new string[]
            {
                "▓▓▓_▓▓▓",
                "▓▓▓_▓▓▓",
                "_▓▓▓▓▓_",
                "__▓▓▓__",
                "__▓▓▓__",
            };

            _map[(int)'z'] = _map[(int)'Z'] = new string[]
            {
                "▓▓▓▓▓▓▓",
                "____▓▓▓",
                "_▓▓▓▓▓_",
                "▓▓▓____",
                "▓▓▓▓▓▓▓",
            };

            _map[(int)'.'] = new string[]
            {
                "_______",
                "_______",
                "_______",
                "__▒▓___",
                "__▒▓___",
            };

            _map[(int)' '] = new string[]
            {
                "_______",
                "_______",
                "_______",
                "_______",
                "_______",
            };
        }
    }
}
