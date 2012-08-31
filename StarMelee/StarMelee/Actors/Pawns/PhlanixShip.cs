using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Drivers;
using BaseGame.Functions;
using Microsoft.Xna.Framework;
using StarMelee.Drivers;

namespace StarMelee.Actors.Pawns
{
    class PhlanixShip : BaseEnemyShip
    {
        public PhlanixShip(ShmupGameState gameState, Vector3 rotation = new Vector3()) :
            base(gameState, rotation, "Models/Ships/p2_wedge")
        {
            Driver = new ForwardeDriver(rotation);

        }
        protected override void Setup()
        {
            base.Setup();
            Scale = 2f;
            MaxLife = 10;
            PointValue = 2000;

            Weapons.Add(new BaseEnemyWeapon(this, GameState) { CooldownTime = 50, Position = new Vector3(0, 500, 0)});
            Weapons.Add(new BaseEnemyWeapon(this, GameState) { CooldownTime = 50, Position = new Vector3(0, 500, 0), Rotation = new Vector3(0, 0, MathHelper.Pi / 10) });
            Weapons.Add(new BaseEnemyWeapon(this, GameState) { CooldownTime = 50, Position = new Vector3(0, 500, 0), Rotation = new Vector3(0, 0, -MathHelper.Pi / 10) });


        }
    }
}
