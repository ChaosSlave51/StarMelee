using System;
using System.Collections.Generic;
using BaseGame;
using BaseGame.Actors.Pawns;
using BaseGame.Actors.Sprites;
using BaseGame.Audio;
using BaseGame.Levels;
using BaseGame.Physics;
using BaseGame.Resources;
using BaseGame.Screens;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Ninject;
using StarMelee.Actors.Pawns;
using StarMelee.Drivers;
using StarMelee.Levels;

namespace StarMelee.Screens
{
    class Shmup:BaseScreen
    {
        //temp

        ILevel _level;
        //end temp


        
        private readonly ShmupGameState _gameState;
        private Music _music;
        private TextSprite Score;
        public Shmup()
        {
            

            _gameState= new ShmupGameState();
           
            _level = new Level1(_gameState);
            _gameState.Player = new ThermoPlayerShip(_gameState, new PlayerDriver());
            Score = new TextSprite("fonts/LcdBold", Color.Goldenrod) { Format = "{0:000000000}" };
            _music = new Music("Audio/Music/Seeking-Revenge");
            _gameState.Hud.Add(Score);
            
        }

        public override void CreateBindings()
        {
            var kernel = ((NinjectServiceLocator)ServiceLocator.Current).Kernel;

            ReasourceList.Add(_level);
            ReasourceList.Add(_gameState.Player);
            ReasourceList.Add(Score);
            ReasourceList.Add(_music);
            foreach (var resource in Resource.Combine(ReasourceList))
            {

                kernel.Bind(resource.Type).ToMethod(context =>
                {
                    return Program.Game.Content.Load<object>(resource.Path);
                }).InSingletonScope().Named(resource.Path);

                kernel.Get(resource.Type, resource.Path);
            }
        }
      
       

        public override void Initialise(GraphicsDeviceManager graphics)
        {
            base.Initialise(graphics);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(40.0f),
                Graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                100000.0f);
            CameraMain = new Camera(
                new Vector3(0.0f, 0.0f, 30000.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                projection);
            _gameState.MainCamera = CameraMain;
            _music.Play();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float time = GetTimecksFromTime(gameTime);
            _gameState.EnemyBullets.ForEach(x=>
                {
                    if(!x.OnCamera(CameraMain))
                    {
                        x.Kill();
                    }
                });
            _gameState.PlayerBullets.ForEach(x =>
            {
                if (!x.OnCamera(CameraMain))
                {
                    x.Kill();
                }
            });
            _gameState.EachPawn(pawn =>
                                    {
                                        if (pawn.Alive)
                                        {
                                            pawn.Update(time);
                                        }
                                    });
            if (_gameState.PawnCount > 1000)
            {
                _gameState.RemoveDeadPawns();
            }


            _level.Update(time);
            _gameState.CheckCollisionForPawns();


            Score.Value = _gameState.Score;

            if(_gameState.Player.CurrentState==State.Dead)
            {
                if (Lose != null)
                    Lose();

            }
            

        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Azure);
            //_graphics.GraphicsDevice.BlendState=BlendState.AlphaBlend;
            // TODO: Add your drawing code here
            SpriteBatch.Begin();
            
            _gameState.EachPawn(pawn =>
                                    {
                                        if (pawn.Alive)
                                        {
                                            pawn.Draw(CameraMain,SpriteBatch);
                                        }
                                    });
          
            DrawSprites(gameTime);
            SpriteBatch.End();
        }
        protected override void DrawSprites(GameTime gameTime)
        {
            
            base.DrawSprites(gameTime);
            _gameState.EachSprite(sprite => sprite.Draw(SpriteBatch));
        }
        public override void Dispose()
        {
            
            var kernel = ((NinjectServiceLocator)ServiceLocator.Current).Kernel;
            foreach (var resource in Resource.Combine(ReasourceList))
            {
                kernel.Unbind(resource.Type);
            }
            _music.Stop();
            base.Dispose();
        }


        public event Action Lose;
    }
}
