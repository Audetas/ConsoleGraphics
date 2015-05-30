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
    class Label : UIObject
    {
        public Label(Space space, Style style)
            : base(space, style)
        {
            space.UIObjects.Add(this);
        }

        public Label(UIObject parent, Style style)
            : base(parent, style)
        {
            parent.Children.Add(this);
        }

        public Label(UIObject parent, string text)
            : base(parent, Style.Default)
        {
            parent.Children.Add(this);
            Style.Text = text;
            Size = new Vector2(text.Length, 1);
        }

        public override void Draw()
        {
            Renderer.Draw(this, Style.Text, 0, 0, Style.BackColor);
        }
    }
}
