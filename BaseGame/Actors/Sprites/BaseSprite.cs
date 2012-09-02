using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Sprites
{
    public abstract class BaseSprite : BaseActor, INeedsResources
    {
        public abstract void Draw(SpriteBatch sprite);


        public abstract Vector2 Size { get; }

        public abstract IEnumerable<Resource> ResourcePaths();
    }
}
