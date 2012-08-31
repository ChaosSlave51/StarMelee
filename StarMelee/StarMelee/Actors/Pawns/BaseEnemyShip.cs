﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Drivers;
using BaseGame.Functions;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StarMelee.Drivers;

namespace StarMelee.Actors.Pawns
{
    class BaseEnemyShip : Ship
    {

        public BaseEnemyShip(ShmupGameState gameState, Vector3 rotation = new Vector3(), string modelPath = "Models/Ships/p1_saucer")
            : base(modelPath, gameState, null, rotation)//
        {

            BaseRotation = new Vector3(MathHelper.PiOver2, 0, 0);
            BaseScale = 0.5f;
            CollisionSpheres = new List<BoundingSphere>() { new BoundingSphere(new Vector3(), 500) };
            Driver = new DroneDriver(rotation);

        }
        protected override void ResolveResources()
        {
            base.ResolveResources();
            DeathSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/bombexplosion");
        }
        public override IEnumerable<Resource> ResourcePaths()
        {
            return base.ResourcePaths().Concat(new Resource[] { new Resource("Audio/Ships/bombexplosion", typeof(SoundEffect)), });
        }

        protected override void Setup()
        {
            base.Setup();

            MaxLife = 3;
            PointValue = 100;
            var weapon1 = new BaseEnemyWeapon(this, GameState);
            weapon1.CooldownTime = 50;

            weapon1.Position = new Vector3(0, 500, 0);
            Weapons.Add(weapon1);

        }

    }
}
