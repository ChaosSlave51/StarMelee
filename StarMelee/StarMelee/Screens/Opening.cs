using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors.Sprites;
using BaseGame.Audio;
using BaseGame.Resources;
using BaseGame.Screens;
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Ninject;

namespace StarMelee.Screens
{
    class Opening : BaseScreen
    {
        private TextSprite _gameName;
        private StillSprite _start;
       
        private Music _music;
        public Opening()
        {
            _gameName = new TextSprite("fonts/7th Service", Color.Blue) { Value = "Star Melee" };
            _start = new StillSprite("Sprites/Controllers/XBox/small_start");
            _music = new Music("Audio/Music/An_Infinite_Univers");
           
            
        }
        
        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
            {
                if (StartGame != null)
                    StartGame();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            DrawSprites(gameTime);
            SpriteBatch.End();
        }

        public override void CreateBindings()
        {
            var kernel = ((NinjectServiceLocator)ServiceLocator.Current).Kernel;

            ReasourceList.Add(_gameName);
            ReasourceList.Add(_start);
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
            var x = (graphics.GraphicsDevice.Viewport.Width - (_gameName.Size.X + _start.Size.X)) / 2;
            var textY = (graphics.GraphicsDevice.Viewport.Height - _gameName.Size.Y) / 2;
            var buttonY = (graphics.GraphicsDevice.Viewport.Height - _start.Size.Y - 8) / 2;
            _gameName.Position = new Vector3(x, textY, 0);
            _start.Position = new Vector3(x + _gameName.Size.X, buttonY, 0);

            
            _music.Play();

            
            
        }

        protected override void DrawSprites(GameTime gameTime)
        {
            
            Graphics.GraphicsDevice.Clear(Color.Azure);
            base.DrawSprites(gameTime);
            _gameName.Draw(SpriteBatch);
            _start.Draw(SpriteBatch);
            
        }

        public override void Dispose()
        {
            _music.Stop();
            var kernel = ((NinjectServiceLocator)ServiceLocator.Current).Kernel;
            foreach (var resource in Resource.Combine(ReasourceList))
            {
                kernel.Unbind(resource.Type);
            }
            
            base.Dispose();
         
        }

        public event Action StartGame;
    }
}
