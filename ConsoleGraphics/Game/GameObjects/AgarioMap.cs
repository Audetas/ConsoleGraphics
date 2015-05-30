using ConsoleGraphics.Graphics;
using ConsoleGraphics.Net;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.GameObjects
{
    class AgarioMap : GameObject
    {
        public AgarioMap(Space space) 
            : base(space)
        {

        }

        public List<NodeData> Nodes = new List<NodeData>();
        public float Scale = 9f;
        public short Color = (short)ForeGround.Gray | (short)BackGround.WHITE;

        public override void Draw()
        {
            for (float x = 0; x < Width; x += Scale)
            {
                Renderer.DrawLine(this, new Vector2(x, 0), new Vector2(x, Height), '│', Color);
            }

            for (float y = 0; y < Height; y += Scale)
            {
                Renderer.DrawLine(this, new Vector2(0, y), new Vector2(Width, y), '─', Color);
            }
        }
    }
}
