using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class ListBox : UIObject
    {
        public List<object> Items = new List<object>();
        public int SelectedIndex = -1;
        private int _highlightedIndex = -1;

        public ListBox(Space space, Style style) 
            : base(space, style) 
        {
            space.UIObjects.Add(this);
        }

        public ListBox(UIObject parent, Style style) 
            : base(parent, style) 
        {
            parent.Children.Add(this);
            Parent = parent;
        }

        public override void Update()
        {
            base.Update();
            _highlightedIndex = -1;
            Vector2 mouse = Mouse.Position;
            for (int y = 0; y < Items.Count; y++)
            {
                int realX = Style.Border ? 2 : 1;
                int realY = Style.Border ? y + 2 : y + 1;
                if ((int)mouse.Y == (int)Y + realY && mouse.X > X && mouse.X < X + Width)
                {
                    _highlightedIndex = y;
                    if (Mouse.Left) SelectedIndex = y;
                }
            }
        }

        public override void Draw()
        {
            float x = X;
            if (Style.Filled) Renderer.FillRect(this, 0, 0, Width, Height, Style.BackChar, Style.BackColor);
            if (Style.Border) Renderer.DrawRect(this, 0, 0, Width, Height, Style.ForeChar, Style.ForeColor);

            for (int y = 0; y < Items.Count; y++)
            {
                int realX = Style.Border ? 2 : 1;
                int realY = Style.Border ? y + 2 : y + 1;
                Renderer.Draw(
                    this, Items[y].ToString(), realX, realY, 
                    _highlightedIndex == y || SelectedIndex == y
                    ? Style.ForeColor
                    : Style.BackColor);
            }
        }
    }
}
