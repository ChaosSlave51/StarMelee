using System;
using BaseGame.Actors.Sprites;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Screens
{
    public abstract class  BaseScreen:IDisposable 
    {
        protected Camera CameraMain;
        protected SpriteBatch  SpriteBatch;

        protected GraphicsDeviceManager Graphics;
        protected TimeSpan? StartTime;


        #if DEBUG
                private TextSprite RunningSlow;
        #endif
        public virtual void Initialise(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            SpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            #if DEBUG
                        RunningSlow = new TextSprite(ServiceLocator.Current.GetInstance<SpriteFont>("fonts/LcdBold"), Color.Red) { Position = new Vector3(700, 0, 0), Value = "SLOW" };
            #endif
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);


        protected float GetTimecksFromTime(GameTime time)
        {
            if(StartTime==null)
            {
                StartTime = time.TotalGameTime;
            }
            return (float) (time.TotalGameTime - StartTime).Value.TotalMilliseconds;
        }

        protected virtual void DrawSwpites(GameTime gameTime)
        {
            
            #if DEBUG
                        if (gameTime.IsRunningSlowly)
                        {
                            RunningSlow.Draw(SpriteBatch);
                        }
            #endif
        }

        public abstract void LoadContent();

        public virtual void Dispose()
        {
            
        }
    }
}
