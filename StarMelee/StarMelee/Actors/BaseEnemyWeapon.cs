using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StarMelee.Actors.Pawns;

namespace StarMelee.Actors
{
    class BaseEnemyWeapon : BaseActor,IWeapon
    {
        public Ship ParentShip = null;
        public int CooldownTime=0;
        public int Cooldown = 0;
        private readonly ShmupGameState _gameState;

        protected SoundEffect DeathSound;      public BaseEnemyWeapon(Ship parent, ShmupGameState gameState)
        {
            _gameState = gameState;
            ParentShip = parent;

          
        }



        public void Fire()
        {
            if (Cooldown == 0)
            {
                BaseBullet baseBullet = new BaseBullet(ParentShip.Rotation);
                Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(ParentShip.Rotation.Y, ParentShip.Rotation.X, ParentShip.Rotation.Z);

                baseBullet.Position = ParentShip.TotalPosition + Vector3.Transform(Position, rotationMatrix);
                
                

                _gameState.EnemyBullets.Add(baseBullet);
                Cooldown = CooldownTime;
            }
        }
        public void Update()
        {
            if (Cooldown > 0)
            {
                Cooldown--;
            }
        }
    }
}
