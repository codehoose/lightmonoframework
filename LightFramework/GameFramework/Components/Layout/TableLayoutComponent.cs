using Microsoft.Xna.Framework;

namespace GameFramework.Components.Layout
{
    public class TableLayoutComponent : NullComponent
    {
        private readonly int _columns;
        private readonly int _rows;

        public TableLayoutComponent(IGame game, BaseComponent parent = null, int columns = 1, int rows = 1, Point pivot = new Point(), Point size = new Point()) : base(game, parent)
        {
            _columns = columns;
            _rows = rows;
            Size = size;
        }

        protected override void OnChildAdded(BaseComponent component)
        {
            int index = ChildCount - 1;
            int cellWidth = Size.X / _columns;
            int cellHeight = Size.Y / _rows;

            int col = index % _columns;
            int row = index / _columns;

            int startX = col * cellWidth;
            int startY = row * cellHeight;

            component.Position = new Vector2 (startX, startY);
            base.OnChildAdded(component);
        }
    }
}
