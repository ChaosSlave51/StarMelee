using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Drivers;
using BaseGame.Functions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StarMelee.Drivers;

namespace StarMelee.Actors.Pawns
{
    class BaseEnemyShip:Ship
    {

        public BaseEnemyShip(ShmupGameState gameState,Vector3 rotation=new Vector3())
            : base (ServiceLocator.Current.GetInstance<Model>("Models/Ships/p1_saucer"), gameState, new DroneDriver(rotation), rotation)//
        {

            BaseRotation = new Vector3(MathHelper.PiOver2, 0, 0);
            BaseScale = 0.5f;
            CollisionSpheres= new List<Sphere>(){new Sphere(new Vector3(),500 )};

            DeathSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/bombexplosion");
        }

        

        protected override void Setup()
        {
            base.Setup();


            var weapon1 = new BaseEnemyWeapon(this, GameState);
            weapon1.CooldownTime = 50;

            weapon1.Position = new Vector3(0, 500, 0);
            Weapons.Add(weapon1);

        }
    }
}
