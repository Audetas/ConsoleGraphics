using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    public class Style
    {
        public string Name = "Name";
        public string Text = "Text";

        public bool Filled = true;
        public bool Border = true;

        public ForeGround ForeColor = ForeGround.DarkGray;
        public BackGround BackColor = BackGround.Black;
        public ForeGround MouseOverForeColor = ForeGround.Gray;
        public BackGround MouseOverBackColor = BackGround.Black;
        public ForeGround MouseDownForeColor = ForeGround.DarkGray;
        public BackGround MouseDownBackColor = BackGround.Black;

        public char ForeChar = '█';
        public char BackChar = '█';
        public char MouseOverForeChar = '▒';
        public char MouseOverBackChar = '▒';
        public char MouseDownForeChar = '█';
        public char MouseDownBackChar = '█';

        public static Style Default
        {
            get { return new Style(); }
        }
    }
}
