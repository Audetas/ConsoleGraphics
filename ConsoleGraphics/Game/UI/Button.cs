using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class Button : UIObject
    {
        public Button(Space space, Style style) 
            : base(space, style) 
        {
            space.UIObjects.Add(this);
        }

        public Button(UIObject parent, Style style) 
            : base(parent, style) 
        {
            parent.Children.Add(this);
        }

        public bool GripContains(Vector2 point)
        {
            return point.X >= Position.X && point.X <= Position.X + Size.X &&
                point.Y >= Position.Y && point.Y <= Position.Y + 3;
        }
        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            if (MouseOver && !MouseDown && Style.Enabled)
            {
                if (Style.Filled) Renderer.FillRect(this, 0, 0, Width, Height, Style.BackChar, Style.MouseOverBackColor);
                if (Style.Border) Renderer.DrawRect(this, 0, 0, Width, Height, Style.ForeChar, Style.MouseOverForeColor);
                Renderer.Draw(this, Style.Text, (int)(Width / 2f - Style.Text.Length / 2f), (int)(Height / 2f), Style.MouseOverBackColor);
            }
            else
            {
                if (Style.Filled) Renderer.FillRect(this, 0, 0, Width, Height, Style.BackChar, Style.BackColor);
                if (Style.Border) Renderer.DrawRect(this, 0, 0, Width, Height, Style.ForeChar, Style.ForeColor);
                Renderer.Draw(this, Style.Text, (int)(Width / 2f - Style.Text.Length / 2f), (int)(Height / 2f), Style.BackColor);
            }
        }
    }
}
