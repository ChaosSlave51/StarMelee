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
    public class StillSprite:BaseSprite,INeedsResources
    {
        private readonly string _texturePath;
        private  Texture2D _texture;

        public StillSprite(string texturePath)
        {
            _texturePath = texturePath;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Vector2(Position.X, Position.Y), Color.White);
        }

        public override Vector2 Size
        {
            get
            {
                ResolveResourcesIfNeeded();
                return new Vector2(_texture.Width,_texture.Height);
            }
        }

        protected override void ResolveResources()
        {
            _texture = ServiceLocator.Current.GetInstance<Texture2D>(_texturePath); ;
        }

        public IEnumerable<Resource> ResourcePaths()
        {
            return new Resource[] { new Resource(_texturePath, typeof(Texture2D)) };
        }
    }
}
