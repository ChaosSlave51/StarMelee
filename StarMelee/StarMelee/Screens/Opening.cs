using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors.Sprites;
using BaseGame.Screens;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace StarMelee.Screens
{
    class Opening : BaseScreen
    {
        private TextSprite _gameName;
        private StillSprite _start;
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                StartGame();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            DrawSwpites(gameTime);
            SpriteBatch.End();
        }

        public override void Initialise(GraphicsDeviceManager graphics)
        {
            base.Initialise(graphics);
            _gameName = new TextSprite(ServiceLocator.Current.GetInstance<SpriteFont>("fonts/7th Service"), Color.Blue){Value = "Star Melee"};
            _start = new StillSprite(ServiceLocator.Current.GetInstance<Texture2D>("Sprites/Controllers/XBox/small_start"));
            var x= (graphics.GraphicsDevice.Viewport.Width - (_gameName.Size.X + _start.Size.X))/2;
            var textY = (graphics.GraphicsDevice.Viewport.Height -_gameName.Size.Y)/2;
            var buttonY = (graphics.GraphicsDevice.Viewport.Height - _start.Size.Y-8) / 2;
            _gameName.Position = new Vector3(x, textY, 0);
            _start.Position = new Vector3(x + _gameName.Size.X, buttonY, 0);


            MediaPlayer.Play(ServiceLocator.Current.GetInstance<Song>("Audio/Music/An_Infinite_Univers"));

            
        }

        protected override void DrawSwpites(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Azure);
            base.DrawSwpites(gameTime);
            _gameName.Draw(SpriteBatch);
            _start.Draw(SpriteBatch);
            
        }
        public override void LoadContent()
        {
            
        }
        public override void Dispose()
        {
            base.Dispose();
            MediaPlayer.Stop();
        }

        public event Action StartGame;
    }
}
