using ConsoleGraphics.Events;
using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class Window : UIObject
    {
        public Window(Space space, Style style) 
            : base(space, style) 
        {
            space.UIObjects.Add(this);
        }

        public Window(UIObject parent, Style style) 
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
            if (Style.Draggable && MouseDown && GripContains(Mouse.Position))
                Position = Mouse.Position - new Vector2(2, 2);
        }

        public override void Draw()
        {
            if (Style.Hidden) return;
            if (Style.Filled) Renderer.FillRect(this, 0, 0, Width, Height, Style.BackChar, Style.BackColor);
            if (Style.Border) Renderer.DrawRect(this, 0, 0, Width, Height, Style.ForeChar, Style.ForeColor);
            base.Draw();
        }
    }
}
