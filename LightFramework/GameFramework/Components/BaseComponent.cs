using Microsoft.Xna.Framework;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameFramework.Components
{
    public abstract class BaseComponent : DrawableGameComponent, INotifyPropertyChanged
    {
        public Vector2 Position { get; set; }
        public BaseComponent Parent { get; }
        public new IGame Game => (IGame)base.Game;

        private Vector2 _pivot = Vector2.Zero;
        private Vector2 _pivotPoint = Vector2.Zero;
        private Point _size = Point.Zero;

        public event PropertyChangedEventHandler PropertyChanged;

        public Vector2 WorldPosition => GetWorldPosition();

        public Vector2 Pivot
        {
            get { return _pivot; }
            set
            {
                _pivot = value;
                _pivotPoint = new Vector2(-_pivot.X * _size.X, -_pivot.Y * _size.Y);
            }
        }

        public Point Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged();
            }
        }

        public new bool Visible
        {
            get { return base.Visible; }
            set
            {
                base.Visible = value;
                OnPropertyChanged();
            }
        }

        protected Vector2 PivotPoint => _pivotPoint;

        public BaseComponent(IGame game, BaseComponent parent = null) : base((Game)game)
        {
            Parent = parent;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Vector2 GetWorldPosition(Vector2 pivot = new Vector2())
        {
            Vector2 pos = Position;
            var parent = Parent;
            while (parent != null)
            {
                pos += parent.Position;
                parent = parent.Parent;
            }

            return pos + pivot;
        }
    }
}
