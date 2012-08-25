using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Sprites
{
    public abstract class BaseSprite : BaseActor
    {
        public abstract void Draw(SpriteBatch sprite);


        public abstract Vector2 Size { get; }

    }
}
