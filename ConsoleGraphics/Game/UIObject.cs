using ConsoleGraphics.Game.UI;
using ConsoleGraphics.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGraphics.Game
{
    public class UIObject : GameObject
    {
        public Style Style;
        public List<UIObject> Children;
        public UIObject Parent = null;

        public UIObject(Space space, Style style) : base(space)
        {
            Style = style;
            Children = new List<UIObject>();
        }

        public UIObject(UIObject parent, Style style)
        {
            this.Space = parent.Space;
            this.Parent = parent;
            Style = style;
            Children = new List<UIObject>();
        }

        public Vector2 GetRawPosition()
        {
            if (Parent != null)
                return Parent.GetRawPosition() + this.Position;
            else
                return this.Position;
        }
    }
}
