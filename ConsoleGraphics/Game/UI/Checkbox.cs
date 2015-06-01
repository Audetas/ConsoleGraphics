using ConsoleGraphics.Graphics;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game.UI
{
    class Checkbox : UIObject
    {
        public bool Checked = false;
        private bool _lastState = false;

        public Checkbox(Space space, Style style)
            : base(space, style)
        {
            space.UIObjects.Add(this);
        }

        public Checkbox(UIObject parent, Style style)
            : base(parent, style)
        {
            parent.Children.Add(this);
        }

        public void SetText(string text)
        {
            Style.Text = text;
            Width = text.Length + 4;
            Height = 3;
        }

        public override void Update()
        {
            base.Update();

            if (_lastState && !MouseDown) Checked = !Checked;
            _lastState = MouseDown;
        }

        public override void Draw()
        {
            if (MouseOver && !MouseDown)
            {
                Renderer.DrawRect(this, 0, 0, 2, 2, Style.ForeChar, Style.ForeColor);
                Renderer.Draw(this, Style.Text, 4, 1, Style.ForeColor);
                if (Checked) Renderer.Draw(this, "x", 1, 1, Style.ForeColor);
            }
            else
            {
                Renderer.DrawRect(this, 0, 0, 2, 2, Style.ForeChar, Style.BackColor);
                Renderer.Draw(this, Style.Text, 4, 1, Style.BackColor);
                if (Checked) Renderer.Draw(this, "x", 1, 1, Style.BackColor);
            }
        }
    }
}
