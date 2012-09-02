using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StarMelee.Actors.Pawns;

namespace StarMelee.Actors
{
    class BaseEnemyWeapon : BaseActor,IWeapon
    {
        public Ship ParentShip = null;
        private readonly ShmupGameState _gameState;

        protected SoundEffect DeathSound;      
        
        public BaseEnemyWeapon(Ship parent, ShmupGameState gameState)
        {
            _gameState = gameState;
            ParentShip = parent;
        }



        public void Fire()
        {

                BaseBullet baseBullet = new BaseBullet(new Vector3(ParentShip.Rotation.X + Rotation.X, 0, ParentShip.Rotation.Z + Rotation.Z));
                //Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(ParentShip.Rotation.Y, ParentShip.Rotation.X, ParentShip.Rotation.Z);

                baseBullet.Position = ParentShip.TotalPosition; //+ Vector3.Transform(Position, rotationMatrix);

                _gameState.EnemyBullets.Add(baseBullet);
             
            
        }

        public void Update()
        {
         
        }

        public IEnumerable<Resource> ResourcePaths()
        {
            BaseBullet baseBullet = new BaseBullet();
            return baseBullet.ResourcePaths();

        }

        protected override void ResolveResources()
        {
            
        }
    }
}
