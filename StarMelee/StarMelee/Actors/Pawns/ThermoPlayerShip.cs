using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Actors.Sprites;
using BaseGame.Drivers;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StarMelee.Collision;

namespace StarMelee.Actors.Pawns
{
    class ThermoPlayerShip:BasePlayerShip
    {
        protected int Temprature
        {
            get { return _temprature; }
            set { _temprature = value; 

                if(_temprature>OverheatTemprature)
                {
                    if (Overheated == false)
                    {
                        Overheated = true;
                        _overheatedSound.Play();
                    }
                }
                //else if ((float)_temprature/OverheatTemprature>.8)
                //{
                //    _warningSound.Play();
                //}
                else if (_temprature==0)
                {
                    if(Overheated)
                    {
                        _cooledSound.Play();
                    }
                    Overheated = false;
                }
            }
        }

        protected int OverheatTemprature = 1000;
        protected bool Overheated;
        protected int CoolingRate = 2;
        private int _temprature;
        protected  TextSprite HeatDisplay = new TextSprite("fonts/LcdBold", Color.Black);

        private SoundEffect _warningSound;
        private SoundEffect _overheatedSound;
        private SoundEffect _cooledSound;



        public ThermoPlayerShip(ShmupGameState gameState, IDriver<BasePlayerShip> driver) : base(gameState, driver)
        {
        }

        protected override void ResolveResources()
        {
            base.ResolveResources();

            _warningSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/alarm001");
            _overheatedSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/alarm-clock");
            _cooledSound = ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/shipsbell");
        }
        public override IEnumerable<BaseGame.Resources.Resource> ResourcePaths()
        {
            return base.ResourcePaths().Concat(new Resource[] { new Resource("Audio/Ships/alarm001", typeof(SoundEffect)), 
                new Resource("Audio/Ships/alarm-clock", typeof(SoundEffect)), 
                new Resource("Audio/Ships/shipsbell", typeof(SoundEffect)), 
            });
        }
        protected override void Setup()
        {
            base.Setup();

            
            HeatDisplay.Format = "{0:0}%";
            HeatDisplay.Position= new Vector3(200,0,0);
            Hud= new List<BaseSprite> {HeatDisplay};

            WeaponSet spraySet = new WeaponSet(new List<IWeapon>
                                                    {
                                                        new BasePlayerWeapon(this, GameState)
                                                            {Position = new Vector3(0, 500, 0)},
                                                        new BasePlayerWeapon(this, GameState)
                                                            {
                                                                Position = new Vector3(500, 500, 0),
                                                                Rotation = new Vector3(0, 0, MathHelper.Pi/10)
                                                            },
                                                        new BasePlayerWeapon(this, GameState)
                                                            {
                                                                Position = new Vector3(-500, 500, 0),
                                                                Rotation = new Vector3(0, 0, -MathHelper.Pi/10)
                                                            },
                                                        new BasePlayerWeapon(this, GameState)
                                                            {
                                                                Position = new Vector3(500, 500, 0),
                                                                Rotation = new Vector3(0, 0, MathHelper.Pi/5)
                                                            },
                                                        new BasePlayerWeapon(this, GameState)
                                                            {
                                                                Position = new Vector3(-500, 500, 0),
                                                                Rotation = new Vector3(0, 0, -MathHelper.Pi/5)
                                                            }
                                                    }) { CooldownTime = 3 };
            spraySet.PreFire+=SpraySetOnPreFire;
            spraySet.Fired+=SpraySetOnFired;
            WeaponSets.Add(spraySet);

            WeaponSets[0].Fired += BasicOnFired;
        }

        private void BasicOnFired()
        {
            Temprature += 2;
        }


        public override void Update(float time)
        {
            base.Update(time);
            if (CurrentState == State.Alive)
            {
                Temprature -= CoolingRate;
                if (Temprature < 0)
                {
                    Temprature = 0;
                }
                float ratio = Math.Min(((float) Temprature)/OverheatTemprature, 1);
                HeatDisplay.Value = ratio*100;

                if (Overheated)
                {
                    HeatDisplay.Color = Color.Black;
                }
                else
                {
                    BxHsb hxbColor = new BxHsb((int) ((1 - ratio)*75), 240, 120);
                    HeatDisplay.Color = hxbColor.Color;
                }
            }
        }

        private void SpraySetOnFired()
        {
            Temprature += 40;

        }

        private bool SpraySetOnPreFire()
        {
            return !Overheated && Temprature < OverheatTemprature;

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
                if(Overheated)
                {
                    Die();
                }
                else
                {
                    Temprature += OverheatTemprature;
                }


                return;
            }

            base.Collided(o);


        }
    }
}
