using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class DropDown : UIObject
    {
        public List<Object> Items = new List<object>();
        public bool Dropped = false;

        public DropDown(Space space, Style style) 
            : base(space, style) 
        {
            space.UIObjects.Add(this);
        }

        public DropDown(UIObject parent, Style style) 
            : base(parent, style) 
        {
            parent.Children.Add(this);
        }

        public override void Update()
        {
            base.Update();
            if (MouseDown) Dropped = true;
            if (!MouseOver && Mouse.Left) Dropped = true;
        }

        public override void Draw()
        {
            // Body
            if (Style.Filled) Renderer.FillRect(this, 0, 0, Width, 3, Style.BackChar, Style.BackColor);
            if (Style.Border) Renderer.DrawRect(this, 0, 0, Width, 3, Style.ForeChar, Style.ForeColor);
            Renderer.Draw(this, Style.Text, 1, 1, Style.BackColor);

            // DropDown
            if (!Dropped) return;

        }
    }
}
