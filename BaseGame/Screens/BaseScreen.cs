using System;
using System.Collections.Generic;
using BaseGame.Actors.Sprites;
using BaseGame.Resources;
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
        protected List<INeedsResources> ReasourceList;

        #if DEBUG
                private TextSprite RunningSlow;
        #endif

        protected BaseScreen()
        {
            ReasourceList = new List<INeedsResources>();
#if DEBUG
            RunningSlow = new TextSprite("fonts/LcdBold", Color.Red) { Position = new Vector3(700, 0, 0), Value = "SLOW" };
            ReasourceList.Add(RunningSlow);
#endif            
        }

        public virtual void Initialise(GraphicsDeviceManager graphics)
        {
            Graphics = graphics;
            SpriteBatch = new SpriteBatch(graphics.GraphicsDevice);
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);

        public abstract void CreateBindings();
        
        protected float GetTimecksFromTime(GameTime time)
        {
            if(StartTime==null)
            {
                StartTime = time.TotalGameTime;
            }
            return (float) (time.TotalGameTime - StartTime).Value.TotalMilliseconds;
        }

        protected virtual void DrawSprites(GameTime gameTime)
        {
            
            #if DEBUG
                        if (gameTime.IsRunningSlowly)
                        {
                            RunningSlow.Draw(SpriteBatch);
                        }
            #endif
        }
        

        public virtual void Dispose()
        {
            
        }
    }
}
