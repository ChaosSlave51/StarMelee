using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Drivers;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using StarMelee.Inputs;

namespace StarMelee.Actors.Pawns
{
    class BasePlayerShip : Ship
    {
        private readonly ShmupGameState _gameState;

        public BasePlayerShip(ShmupGameState gameState,IDriver<BasePlayerShip> driver)
            : base("Models/Ships/p1_saucer", gameState, driver)
        {
            _gameState = gameState;
          
            CollisionSpheres = new List<BoundingSphere>() { new BoundingSphere(new Vector3(), 700) };

             SpeedSide=100.0f;
             SpeedForward = 100.0f;
             SpeedBack = 100.0f;
            MaxBank=MathHelper.ToRadians(45);
            EnterBankTime = 20;
            ExitBankTime = 40;

           

        }

        protected override void ResolveResources()
        {
            base.ResolveResources();
            DeathSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/scream-02");
        }
        public override IEnumerable<Resource> ResourcePaths()
        {
            return base.ResourcePaths().Concat(new Resource[] { new Resource("Audio/Ships/scream-02", typeof(SoundEffect)), });
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
        public override void Update(float time)
        {
            if (this.CurrentState == State.Alive)
            {
                if (!this.OnCamera(_gameState.MainCamera,new Vector3(Movement.X,0,0)))
                {
                    Movement= new Vector3(0,Movement.Y,Movement.Z);
                }
                if (!this.OnCamera(_gameState.MainCamera, new Vector3(0, Movement.Y, 0)))
                {
                    Movement = new Vector3(Movement.X, 0, Movement.Z);
                }
                if (!this.OnCamera(_gameState.MainCamera, new Vector3(0, 0, Movement.Z)))
                {
                    Movement = new Vector3(Movement.X, Movement.Y, 0);
                }
            }

            base.Update(time);
        }
        protected override void Setup()
        {
            base.Setup();
            WeaponSet basicWeaponSet = new WeaponSet(new List<IWeapon>
                                                    {
                                                        new BasePlayerWeapon(this, GameState)
                                                            {Position = new Vector3(0, 500, 0)},
                                                        new BasePlayerWeapon(this, GameState)
                                                            {
                                                                Position = new Vector3(500, 500, 0),
                                                                Rotation = new Vector3(0, 0, MathHelper.Pi/40)
                                                            },
                                                        new BasePlayerWeapon(this, GameState)
                                                            {

                                                                Position = new Vector3(-500, 500, 0),
                                                                Rotation = new Vector3(0, 0, -MathHelper.Pi/40)
                                                            }
                                                    }){CooldownTime = 5};

            WeaponSets.Add(basicWeaponSet);

        }

    }
}
