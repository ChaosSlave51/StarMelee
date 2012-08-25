using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Sprites
{
    public class StillSprite:BaseSprite
    {
        private readonly Texture2D _texture;

        public StillSprite(Texture2D texture)
        {
            _texture = texture;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), Color.White);
        }

        public override Vector2 Size
        {
            get { return new Vector2(_texture.Width,_texture.Height); }
        }
    }
}
