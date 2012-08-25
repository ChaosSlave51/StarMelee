using System;
using BaseGame;
using BaseGame.Actors.Pawns;
using BaseGame.Actors.Sprites;
using BaseGame.Levels;
using BaseGame.Screens;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
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



        public Shmup()
        {
            _gameState= new ShmupGameState();
            
        }

      
        private TextSprite Score;

        public override void Initialise(GraphicsDeviceManager graphics)
        {
            base.Initialise(graphics);
            Score = new TextSprite(ServiceLocator.Current.GetInstance<SpriteFont>("fonts/LcdBold"), Color.Goldenrod) { Format = "{0:000000000}" };

      

            
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(40.0f),
                Graphics.GraphicsDevice.Viewport.AspectRatio,
                1.0f,
                100000.0f);
            CameraMain = new Camera(
                new Vector3(0.0f, 0.0f, 30000.0f),
                new Vector3(0.0f, 0.0f, 0.0f),
                projection);

            _gameState.Player = new BasePlayerShip(_gameState,new PlayerDriver());
            _level = new Level1(_gameState);
            _gameState.Hud.Add(Score);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float time = GetTimecksFromTime(gameTime);
            _gameState.EachPawn(pawn =>
                                    {
                                        if (pawn.Alive)
                                        {
                                            pawn.Update(time);
                                        }
                                    });
            if (_gameState.PawnCount > 0)
            {
                _gameState.RemoveDeadPawns();
            }


            _level.Update(time);
            _gameState.CheckCollisionForPawns();


            Score.Value = _gameState.Score;
        }

        private SpriteFont _font;
        public override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Azure);
            //_graphics.GraphicsDevice.BlendState=BlendState.AlphaBlend;
            // TODO: Add your drawing code here

            
            _gameState.EachPawn(pawn =>
                                    {
                                        if (pawn.Alive)
                                        {
                                            pawn.Draw(CameraMain);
                                        }
                                    });
            SpriteBatch.Begin();
            DrawSwpites(gameTime);
            SpriteBatch.End();
        }
        protected override void DrawSwpites(GameTime gameTime)
        {
            base.DrawSwpites(gameTime);
            _gameState.EachSprite(sprite => sprite.Draw(SpriteBatch));
        }

        public override void LoadContent()
        {
            ServiceLocator.Current.GetInstance<Model>("Models/Ships/pea_proj");
            ServiceLocator.Current.GetInstance<Model>("Models/Ships/p1_saucer");

            ServiceLocator.Current.GetInstance<SpriteFont>("fonts/LcdBold");

            ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Weapons/laser-zap-01");
            ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/bombexplosion");
            ServiceLocator.Current.GetInstance<SoundEffect>("Audio/Ships/scream-02");

            #if DEBUG
            ServiceLocator.Current.GetInstance<Model>("Models/Debug/sphere");
            #endif

        }
    }
}
