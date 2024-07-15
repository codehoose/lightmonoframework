using GameFramework.Bubbling.Events;
using GameFramework.Components.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.ComponentModel;

namespace GameFramework.Components.UI.GUIEvent
{
    public class ButtonComponent : NullComponent
    {
        private bool _selected;
        private BaseComponent _defaultImage;
        private BaseComponent _hoverImage;
        private MouseHitComponent _mouseHit;

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler Click;

        public event EventHandler MouseEntered
        {
            add => _mouseHit.MouseEntered += value;
            remove => _mouseHit.MouseEntered -= value;
        }

        public event EventHandler MouseExited
        {
            add => _mouseHit.MouseExited += value;
            remove => _mouseHit.MouseExited -= value;
        }

        public ButtonComponent(IGame game, BaseComponent parent = null, string nineSlice = "", Point size = new Point()) : base(game, parent)
        {
            _defaultImage = AddChild<NineSliceImageComponent>(nineSlice, size);
            _hoverImage = _defaultImage;
            _mouseHit = AddChild<MouseHitComponent>();
            _mouseHit.Size = size;
            Size = size;
            SetupEvents(_mouseHit);
            PropertyChanged += ButtonComponent_PropertyChanged;
        }

        public void Select()
        {
            _selected = true;
        }

        private void ButtonComponent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Visible))
            {
                foreach (var child in Children)
                {
                    child.Visible = Visible;
                }
            }
            else if (e.PropertyName == nameof(Selected))
            {
                if (Selected)
                {
                    Game.MessageQueue.BubbleEvent(new ItemSelectedBubbleEvent(this, EventArgs.Empty));
                }
            }
        }

        public ButtonComponent(IGame game, BaseComponent parent = null, string defaultImage = "", string hoverImage = "") : base(game, parent)
        {
            _defaultImage = AddChild<StaticImageComponent>(defaultImage);
            _defaultImage.Visible = true;
            _hoverImage = AddChild<StaticImageComponent>(hoverImage);
            _hoverImage.Visible = false;

            _mouseHit = AddChild<MouseHitComponent>();
            _mouseHit.Size = _defaultImage.Size;
            Size = _defaultImage.Size;
            SetupEvents(_mouseHit);
        }

        private void SetupEvents(MouseHitComponent mouseHit)
        {
            mouseHit.LeftClick += (o, e) =>
            {
                if (!Visible) return;
                Game.MessageQueue.BubbleEvent(new ClickBubbleEvent(this, EventArgs.Empty));
                Click?.Invoke(this, EventArgs.Empty);
            };

            mouseHit.MouseEntered += (o, e) => Selected = true;
            mouseHit.MouseExited += (o, e) => Selected = false;

            if (_hoverImage != _defaultImage)
            {
                mouseHit.MouseEntered += (o, e) =>
                {
                    if (!Visible) return;
                    _defaultImage.Visible = false;
                    _hoverImage.Visible = true;
                };

                mouseHit.MouseExited += (o, e) =>
                {
                    if (!Visible) return;
                    _defaultImage.Visible = true;
                    _hoverImage.Visible = false;
                };
            }
        }
    }
}
