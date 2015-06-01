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
            Center = Space.Camera.Position;
        }

        public bool DrawGrid = true;
        public List<NodeData> Nodes = new List<NodeData>();
        public float Scale = 9f;
        public short Color = (short)ForeGround.Gray | (short)BackGround.WHITE;
        private Random _r = new Random();

        public override void Update()
        {
            base.Update();

            foreach (NodeData n in Nodes)
            {
                Vector2 pos = new Vector2((float)n.X, (float)n.Y);
                if (pos.Distance(n.Target) <= 4f) 
                    n.Target = new Vector2(_r.Next(0, Width), _r.Next(0, Height));

                Vector2 dir = (n.Target - pos).Normalize();
                dir *= 1.5f / (float)n.Size;
                n.X += dir.X;
                n.Y += dir.Y;
            }
        } 

        public override void Draw()
        {
            if (DrawGrid)
            {
                for (float x = 0; x < Width; x += Scale)
                    for (int y = 0; y < Height; y++)
                        Renderer.Draw(this, ".", (int)x, y, Color);

                for (float y = 0; y < Height; y += Scale)
                    for (int x = 0; x < Height; x++)
                        Renderer.Draw(this, ".", x, (int)y, Color);
            }

            foreach (NodeData n in Nodes)
            {
                Renderer.FillCircle(this, (int)n.X, (int)n.Y, (float)n.Size - 1f, '░', (short)(n.Red + n.Green + n.Blue));
                Renderer.DrawCircle(this, (int)n.X, (int)n.Y, (float)n.Size, ' ', (short)(n.Red + n.Green + n.Blue));
            }

            Renderer.FillCircle(this, (int)Center.X, (int)Center.Y, 10f, '░', (short)BackGround.Black);
        }
    }
}
