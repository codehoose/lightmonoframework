using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameFramework.Components.Graphics
{
    public class TextComponent : BaseComponent
    {
        private SpriteFont _font;
        private string _text;
        private string[] _printableText;
        private TextWrap _wrap;
        private string _fontName;
        private int _lineSpacing;
        private TextAlign _align;
        public VerticalTextAlign VerticalAlign { get; set; }

        public Color Color { get; set; } = Color.White;

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public int LineSpacing
        {
            get => _lineSpacing;
            set
            {
                _lineSpacing = value;
            }
        }

        public TextWrap TextWrap
        {
            get => _wrap;
            set
            {
                _wrap = value;
            }
        }

        public TextAlign TextAlign
        {
            get => _align;
            set
            {
                _align = value;
                OnPropertyChanged();
            }
        }

        public char Tail { get; set; } = '\0';

        public string Font
        {
            get { return _fontName; }
            set
            {
                if (_fontName?.ToLower() == value?.ToLower()) return;

                if (_font != null) _font = null;
                _font = Game.Content.Load<SpriteFont>(value);
                _fontName = value.ToLower();

                if (_lineSpacing == 0)
                {
                    _lineSpacing = (int)_font.MeasureString("X").Y;
                }
            }
        }

        public TextComponent(IGame game, BaseComponent parent = null)
            : this(game, parent, "", "", Point.Zero)
        {

        }

        public TextComponent(IGame game, BaseComponent parent = null, string fontName = "")
            : this(game, parent, fontName, "", Point.Zero)
        {

        }

        public TextComponent(IGame game, BaseComponent parent = null, string fontName = "", string text = "")
            : this(game, parent, fontName, text, Point.Zero)
        {

        }

        public TextComponent(IGame game, BaseComponent parent = null, string fontName = "", string text = "", Point size = new Point()) : base(game, parent)
        {
            PropertyChanged += TextComponent_PropertyChanged;
            Font = fontName;
            Size = size;
            Text = text;
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 half = _font.MeasureString("X") / 2;

            if (Visible && _printableText != null)
            {
                for (int y = 0; y < _printableText.Length; y++)
                {
                    var printableText = Tail >= 32 ? _printableText[y] + Tail : _printableText[y];

                    Vector2 stringSize = _font.MeasureString(printableText);
                    float pivotBoundary = 8; // TODO: HARDCODED
                    if (_align == TextAlign.Center)
                    {
                        pivotBoundary = (Size.X - stringSize.X) / 2;
                    }
                    else if (_align == TextAlign.Right)
                    {
                        pivotBoundary = -stringSize.X + Size.X;
                    }

                    Vector2 vertical = Vector2.Zero;
                    if (VerticalAlign == VerticalTextAlign.Middle)
                    {
                        vertical = new Vector2(0, -half.Y + Size.Y * 0.5f);
                    }

                    Game.SpriteBatch.DrawString(_font, printableText, GetWorldPosition() + vertical + new Vector2(pivotBoundary, y * _lineSpacing), Color);
                }
            }
        }

        private void TextComponent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Text) || e.PropertyName == nameof(Size))
            {
                CalcPrintableText();
            }
            else if (e.PropertyName == nameof(TextAlign))
            {
                SetPivot(_align);
            }
        }

        private void SetPivot(TextAlign align)
        {
            switch (align)
            {
                case TextAlign.Left:
                    Pivot = new Vector2(0, 0);
                    break;
                case TextAlign.Center:
                    Pivot = new Vector2(-0.5f, 0);
                    break;
                default:
                    Pivot = new Vector2(1, 0);
                    break;
            }
        }

        private void CalcPrintableText()
        {
            if (_text == null) return;
            if (Size == Point.Zero)
            {
                _printableText = new string[] { _text };
                return;
            }

            string tmp = "";
            int index = 0;
            List<string> printableText = new List<string>();

            string[] lineBreaks = _text.Split("\n");
            int lbIndex = 0;
            while (lbIndex < lineBreaks.Length)
            {
                string[] split = lineBreaks[lbIndex++].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                while (index < split.Length)
                {
                    string previous = tmp;
                    tmp += split[index++] + " ";
                    if (_font.MeasureString(tmp).X > Size.X)
                    {
                        printableText.Add(previous.Trim());
                        tmp = split[index - 1];
                    }
                }

                printableText.Add(tmp.Trim());
                tmp = "";
                index = 0;
            }
            _printableText = printableText.ToArray();
        }
    }
}
