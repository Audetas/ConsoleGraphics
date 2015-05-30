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

        public bool Hidden = false;
        public bool Enabled = true;
        public bool Filled = true;
        public bool Border = true;
        public bool Draggable = true;

        public short ForeColor = (short)ForeGround.WHITE | (short)BackGround.Black;
        public short BackColor = (short)ForeGround.Black | (short)BackGround.WHITE;
        public short MouseOverForeColor = (short)ForeGround.Black | (short)BackGround.WHITE;
        public short MouseOverBackColor = (short)ForeGround.WHITE | (short)BackGround.Black;

        public char ForeChar = '│';
        public char BackChar = ' ';
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
