using GameFramework.General.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace GameFramework.Graphics.Palette
{
    public class PaintDotNetPaletteCreator : IPaletteCreator
    {
        private readonly Dictionary<string, Color[]> _palettes = new Dictionary<string, Color[]>();
        private readonly IGame _game;
        private string _filename;
        private int _backgroundColor;

        public PaintDotNetPaletteCreator(IGame game, string filename)
        {
            _game = game;
            _filename = filename;
            GetPalette();
        }

        public int BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                _backgroundColor %= _palettes[_filename].Length;
                _game.BackgroundColor = _palettes[_filename][_backgroundColor];
            }
        }

        public Color[] GetPalette(string filename = "")
        {
            if (!string.IsNullOrEmpty(filename)) _filename = filename.ToLower();

            if (_palettes.TryGetValue(_filename, out Color[] palette))
            {
                return palette;
            }

            List<Color> colours = new List<Color>();

            File.ReadAllLines(_filename)
                .ForEach(line =>
                {
                    if (!line.StartsWith(";"))
                    {
                        var tuple = line.ToRGBA();
                        colours.Add(new Color(tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4));
                    }
                });

            _palettes.Add(_filename, colours.ToArray());
            return _palettes[_filename];
        }
    }
}
