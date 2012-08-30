using System.Collections.Generic;
using BaseGame;
using BaseGame.Actors.Pawns;
using BaseGame.Drivers;
using BaseGame.Functions;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarMelee.Collision;

namespace StarMelee.Actors.Pawns
{
    internal class PlayerBullet : BaseBullet
    {
        public PlayerBullet(Vector3 rotation = new Vector3(),
                            FunctionDriver driver = null)
            : base(rotation: rotation)
        {
            if (driver == null)
            {
                Driver = new FunctionDriver(new Forward() {Scale = 400}, rotation);
            }
            Damage = 1;
            Scale = 1;

        }
    }
}
