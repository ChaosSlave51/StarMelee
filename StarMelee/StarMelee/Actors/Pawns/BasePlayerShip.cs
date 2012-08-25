using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Drivers;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StarMelee.Inputs;

namespace StarMelee.Actors.Pawns
{
    class BasePlayerShip:Ship
    {

        
        public BasePlayerShip(ShmupGameState gameState,IDriver<BasePlayerShip> driver)
            : base(  ServiceLocator.Current.GetInstance<Model>("Models/Ships/p1_saucer"), gameState, driver)
        {
            BaseRotation = new Vector3(MathHelper.PiOver2, 0, 0);
            CollisionSpheres = new List<Sphere>() { new Sphere(new Vector3(), 700) };

             SpeedSide=100.0f;
             SpeedForward = 100.0f;
             SpeedBack = 100.0f;
            MaxBank=MathHelper.ToRadians(45);
            EnterBankTime = 20;
            ExitBankTime = 40;

            DeathSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/scream-02");

        }

        
        public virtual void MoveLeft()
        {
            if (CurrentState == State.Alive)
            {
                Movement += new Vector3(- SpeedSide, 0, 0);
            }
        }
        public virtual void MoveRight()
        {
            if (CurrentState == State.Alive)
            {
                Movement += new Vector3(SpeedSide, 0, 0);
            }
        }
        public virtual void MoveForward()
        {
            if (CurrentState == State.Alive)
            {
                Movement += new Vector3(0, SpeedForward, 0);
            }
        }
        public virtual void MoveBack()
        {
            if (CurrentState == State.Alive)
            {
                Movement += new Vector3(0, - SpeedBack, 0);
            }
        }

        protected override void Setup()
        {
            base.Setup();

            Weapons.Add(new BasePlayerWeapon(this, GameState)
                                           {CooldownTime = 20, Position = new Vector3(0, 500, 0)});
            Weapons.Add(new BasePlayerWeapon(this, GameState) { CooldownTime = 20, Position = new Vector3(500, 500, 0), Rotation = new Vector3(0,  0,MathHelper.Pi/10) });
            Weapons.Add(new BasePlayerWeapon(this, GameState) { CooldownTime = 20, Position = new Vector3(-500, 500, 0), Rotation = new Vector3(0, 0, -MathHelper.Pi/10) });
        }
    }
}
