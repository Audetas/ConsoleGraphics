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

        public void CenterVertical()
        {
            if (Parent == null) Y = Space.Camera.Center.Y - Height / 2f;
            else Y = Parent.Height / 2f - Height / 2f;
        }

        public void CenterHorizontal()
        {
            if (Parent == null) X = Space.Camera.Center.X - Width / 2f;
            else X = Parent.Width / 2f - Width / 2f;
        }

        public override void Update()
        {
            base.Update();
            if (!Style.Hidden && Style.Enabled)
                foreach (UIObject uo in Children) uo.Update();
        }

        public override void Draw()
        {
            if (!Style.Hidden)
                foreach (UIObject uo in Children) uo.Draw();
        }
    }
}
