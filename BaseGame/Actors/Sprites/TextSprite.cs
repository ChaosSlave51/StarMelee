using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Sprites
{
    public class TextSprite:BaseSprite
    {
        private readonly SpriteFont _font;
        private readonly Color _color;
        public string Format { get; set; }
        public object Value { get; set; }

        public override Vector2 Size 
        {
            get { return _font.MeasureString(string.Format(Format, Value)); }
        }

        public TextSprite(SpriteFont font,Color color)
        {
            _font = font;
            _color = color;
            Format = "{0:0}";
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font, string.Format(Format, Value), new Vector2(Position.X, Position.Y), _color);
        }
    }
}
