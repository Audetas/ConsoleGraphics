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

        public List<NodeData> Cells = new List<NodeData>();
        public bool DrawGrid = true;
        public float Scale = 15f;
        public short Color = (short)ForeGround.Gray | (short)BackGround.WHITE;

        public override void Update()
        {
            base.Update();
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

            foreach (NodeData cell in Cells.ToArray())
            {
                Renderer.FillCircle(this, cell.X, cell.Y, cell.Size - 1f, '░', (short)(cell.Red * cell.Green * cell.Blue));
                Renderer.DrawCircle(this, cell.X, cell.Y, cell.Size, ' ', (short)(cell.Red * cell.Green * cell.Blue));
            }
        }
    }
}
