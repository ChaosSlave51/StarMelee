using System;
using System.Collections.Generic;
using System.Linq;
using BaseGame.Actors.Pawns;
using BaseGame.Drivers;
using BaseGame.Functions;
using BaseGame.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StarMelee.Collision;
using StarMelee.Functions;

namespace StarMelee.Actors.Pawns
{
    class Ship:BasePawn
    {
        protected readonly ShmupGameState GameState;

        public int PointValue = 0;
        public Ship(string resourcePath, ShmupGameState gameState, IDriver driver = null, Vector3 rotation = new Vector3())
            : base(resourcePath, driver, rotation)
        {
            GameState = gameState;
            Setup();
        }
        protected SoundEffect DeathSound;
        //speed is always positive
        public float SpeedSide=50.0f;
        public float SpeedForward = 40.0f;
        public float SpeedBack = 40.0f;
        public float MaxBank=MathHelper.ToRadians(45);
        public float EnterBankTime = 20;
        public float ExitBankTime = 40;

      


        public List<IWeapon> Weapons = new List<IWeapon>();
        public float DyingDuration = 300;
        public float DyingTimer = 0;
        public BaseFunction DeathPath;

        protected State _currentState = State.Alive;
        private int _maxLife;
        private int _life;

        public int MaxLife
        {
            get { return _maxLife; }
            set { _maxLife = value;
                Life = value;
            }
        }

        protected override void ResolveResources()
        {
            base.ResolveResources();
            foreach (ModelMesh mesh in MainModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.FogEnabled = true;
                    effect.FogColor = Color.Azure.ToVector3();
                    effect.FogStart = 30000f;
                    effect.FogEnd = 100000f;
                }
                mesh.Draw();
            }
        }


        public int Life
        {
            get { return _life; }
            set { _life = value;
            if (Life < 1)
                Die();
            }
        }


        public State CurrentState
        {
            get { return _currentState; }
            set 
            { 
                _currentState = value;
                if (value == State.Dying)
                {
                    Corporeal = false;


                }
                else if (value == State.Dead)
                {
                    Alive = false;
                }
            }
        }



        public override bool Die()
        {
            ResolveResourcesIfNeeded();
            if (CurrentState == State.Alive)
            {
                GameState.Score += PointValue;

                CurrentState = State.Dying;
                Driver = new FunctionDriver(DeathPath,Rotation);

                Random r = new Random();
                DeathSound.Play((float)r.NextDouble() / 2 + 0.5f, 0.5f- (float)(r.NextDouble()), 0f);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Update(float time)
        {
            //do moving logic
            if (CurrentState == State.Alive)
            {
                CalculateBank();

                foreach (var weapon in Weapons)
                {
                    weapon.Update();
                }
            }
            else if (CurrentState == State.Dying)
            {
            //    Movement= DeathPath.GetStep(DyingTimer);

            //    Rotation = DeathPath.GetRotation(DyingTimer) ;
                DyingTimer++;
                if (DyingTimer > DyingDuration)
                {
                    CurrentState = State.Dead;
                }
            }
            base.Update(time);
        }
        private void CalculateBank()
        {
            float NewY=Rotation.Y;
            //if the ship is being moved in a direction
            if (Movement.X != 0)
            {
                //bank it in that direction 1 unit
                float bankSpeed = MaxBank / EnterBankTime;
                //don't overbank when correctin
                NewY += bankSpeed * Math.Sign(Movement.X);

            }
            else
            {
                //if the ship is not moving, and it is banked, remove bank
                if (NewY != 0)
                {
                    float bankSpeed = MaxBank / ExitBankTime;
                    if(bankSpeed<Math.Abs(NewY))
                        NewY -= bankSpeed * Math.Sign(NewY);
                    else
                        NewY = 0;
                }
            }
            NewY = MathHelper.Clamp(NewY, -MaxBank, MaxBank);
            Rotation = new Vector3(Rotation.X, NewY, Rotation.Z);
        }
        protected virtual void Setup()
        {

            MaxLife = 1;
            DeathPath = new DeathSpiral() {Rotation = Rotation, Radious = 1000, Speed = 0.01f};

        }

        public virtual bool Fire1()
        {
            if (CurrentState == State.Alive)
            {
                
                Weapons.ForEach(x=>x.Fire());
                return true;
            }
            return false;
        }
        public override IEnumerable<BaseGame.Resources.Resource> ResourcePaths()
        {
            var resources = Resource.Combine(Weapons);
            return resources.Concat(base.ResourcePaths());
        }


        public override void Collided(object o)
        {
            var lethal = o as ILethal;
            if (lethal != null)
            {
                Die();
                return;
            }

            var damaging = o as IDamaging;
            if (damaging != null)
            {
                Life -= damaging.Damage;
                DamagedFrames = 2;
                return;
            }

            base.Collided(o);


        }
    }
    public enum State { Alive, Dying, Dead }

    
}
