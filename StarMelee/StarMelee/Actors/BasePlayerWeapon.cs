using System;
using System.Collections.Generic;
using System.Linq;
using BaseGame;

using BaseGame.Actors;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StarMelee.Actors.Pawns;

namespace StarMelee.Actors
{
    class BasePlayerWeapon : BaseActor, IWeapon
    {
        public Ship ParentShip = null;
        public int CooldownTime=0;
        public int Cooldown = 0;
        private readonly ShmupGameState _gameState;


        protected SoundEffect FireSound;
        public BasePlayerWeapon(Ship parent,ShmupGameState gameState)
        {
            _gameState = gameState;
            ParentShip = parent;

        
        }


        public void Fire()
        {
            ResolveResourcesIfNeeded();
            if (Cooldown == 0)
            {
                BaseBullet baseBullet = new BaseBullet(new Vector3(ParentShip.Rotation.X + Rotation.X, 0, ParentShip.Rotation.Z + Rotation.Z));
                //Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(ParentShip.Rotation.Y + Rotation.Y, ParentShip.Rotation.X + Rotation.X, ParentShip.Rotation.Z + Rotation.Z);

                baseBullet.Position = ParentShip.TotalPosition; //+ Vector3.Transform(Position, rotationMatrix);
                _gameState.PlayerBullets.Add(baseBullet);
                Cooldown = CooldownTime;
                Random r = new Random();

                FireSound.Play((float)r.NextDouble() / 2 + 0.5f, 0.5f - (float)(r.NextDouble()), 0f);
            }
        }
        public void Update()
        {
            if (Cooldown > 0)
            {
                Cooldown--;
            }
        }

        public IEnumerable<Resource> ResourcePaths()
        {
            BaseBullet baseBullet = new BaseBullet();
            IEnumerable<Resource> resources = baseBullet.ResourcePaths();
            return resources.Concat(new Resource[]{new Resource("Audio/Weapons/laser-zap-01", typeof (SoundEffect))});

        }

        protected override void ResolveResources()
        {
            FireSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Weapons/laser-zap-01");
        }
    }
}
