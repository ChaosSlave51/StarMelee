using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Sprites
{
    public class TextSprite:BaseSprite,INeedsResources
    {
        private SpriteFont _font;
        private readonly string _fontPath;
        public Color Color { get; set; }
    
        public string Format { get; set; }
        public object Value { get; set; }

        public override Vector2 Size 
        {
            get
            {
                ResolveResourcesIfNeeded();
                return _font.MeasureString(string.Format(Format, Value));
            }
        }

        public TextSprite(string fontPath,Color color)
        {
            _fontPath = fontPath;
            Color = color;
            Format = "{0:0}";
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            ResolveResourcesIfNeeded();
            spriteBatch.DrawString(_font, string.Format(Format, Value), new Vector2(Position.X, Position.Y), Color);
        }

        protected override void ResolveResources()
        {
            _font = ServiceLocator.Current.GetInstance<SpriteFont>(_fontPath); ;
        }

        public override IEnumerable<Resource> ResourcePaths()
        {
            return new Resource[] { new Resource(_fontPath,typeof(SpriteFont))};
        }
    }
}
