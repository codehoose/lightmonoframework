using GameFramework.Components.Graphics;
using Microsoft.Xna.Framework;

namespace GameFramework.Components.UI
{
    public class TextPanelComponent : NullComponent
    {
        private TextComponent _textComponent;
        private NineSliceImageComponent _background;

        public string Text
        {
            get => _textComponent?.Text;
            set
            {
                _textComponent.Text = value;
                OnPropertyChanged();
            }
        }

        public TextPanelComponent(IGame game, BaseComponent parent = null, string panelBackground = "", string fontName = "", Point size = new Point(), int border = 10) : base(game, parent)
        {
            _background = AddChild<NineSliceImageComponent>(panelBackground, size);
            _textComponent = AddChild<TextComponent>(fontName);
            _textComponent.TextAlign = TextAlign.Center;
            _textComponent.LineSpacing = 24; // TODO: HARD CODED!!
            _textComponent.Size = new Point(size.X - border, size.Y - border);
            _textComponent.Position = new Vector2((size.X - _textComponent.Size.X) / 2, (size.Y - _textComponent.Size.Y) / 2);
        }
    }
}
