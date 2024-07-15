using GameFramework.Components.Graphics;
using GameFramework.Input;
using Microsoft.Xna.Framework;
using System;

namespace GameFramework.Components.UI
{
    public class TextInputPanelComponent : NullComponent
    {
        private readonly NineSliceImageComponent _background;
        private readonly TextComponent _textComponent;
        private readonly KeyboardStateExtended _keyboard = new KeyboardStateExtended();
        
        public event EventHandler TextEntered;

        public string Text
        {
            get => _textComponent?.Text;
            set
            {
                _textComponent.Text = value;
                OnPropertyChanged();
            }
        }

        public TextInputPanelComponent(IGame game, BaseComponent parent = null, string panelBackground = "", string fontName = "", Point size = new Point(), int border = 10) : base(game, parent)
        {
            _background = AddChild<NineSliceImageComponent>(panelBackground, size);
            _textComponent = AddChild<TextComponent>(fontName);
            _textComponent.Tail = '_';
            _textComponent.VerticalAlign = VerticalTextAlign.Middle;
            _textComponent.TextAlign = TextAlign.Left;
            _textComponent.LineSpacing = 24; // TODO: HARD CODED!!
            _textComponent.Size = new Point(size.X - border, size.Y - border);
            _textComponent.Position = new Vector2((size.X - _textComponent.Size.X) / 2, (size.Y - _textComponent.Size.Y) / 2);
        }

        protected override void Dispose(bool disposing)
        {
            RemoveAll();
            base.Dispose(disposing);
        }

        public override void Update(GameTime gameTime)
        {
            _keyboard.Update();
            char input = _keyboard.GetKey();

            if (input == '\b' && Text.Length > 0)
            {
                Text = Text.Substring(0, Text.Length - 1);
            }
            else if (input == '\n')
            {
                TextEntered?.Invoke(this, EventArgs.Empty);
            }
            else if (input >= 32)
            {
                Text += input;
            }

            base.Update(gameTime);
        }
    }
}
