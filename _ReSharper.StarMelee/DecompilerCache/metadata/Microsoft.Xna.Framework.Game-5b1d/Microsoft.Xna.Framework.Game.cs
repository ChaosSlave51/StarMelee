// Type: Microsoft.Xna.Framework.Game
// Assembly: Microsoft.Xna.Framework.Game, Version=4.0.0.0, Culture=neutral, PublicKeyToken=842cf8be1de50553
// Assembly location: C:\Program Files (x86)\Microsoft XNA\XNA Game Studio\v4.0\References\Windows\x86\Microsoft.Xna.Framework.Game.dll

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Microsoft.Xna.Framework
{
    public class Game : IDisposable
    {
        public LaunchParameters LaunchParameters { get; }
        public GameComponentCollection Components { get; }
        public GameServiceContainer Services { get; }
        public TimeSpan InactiveSleepTime { get; set; }
        public bool IsMouseVisible { get; set; }
        public TimeSpan TargetElapsedTime { get; set; }
        public bool IsFixedTimeStep { get; set; }
        public GameWindow Window { get; }
        public bool IsActive { get; }
        public GraphicsDevice GraphicsDevice { get; }
        public ContentManager Content { get; set; }

        #region IDisposable Members

        public void Dispose();

        #endregion

        public void Run();
        public void RunOneFrame();
        public void Tick();
        public void SuppressDraw();
        public void Exit();
        protected virtual void BeginRun();
        protected virtual void EndRun();
        protected virtual void Update(GameTime gameTime);
        protected virtual bool BeginDraw();
        protected virtual void Draw(GameTime gameTime);
        protected virtual void EndDraw();
        protected virtual void Initialize();
        public void ResetElapsedTime();
        protected virtual void OnActivated(object sender, EventArgs args);
        protected virtual void OnDeactivated(object sender, EventArgs args);
        protected virtual void OnExiting(object sender, EventArgs args);
        protected virtual void LoadContent();
        protected virtual void UnloadContent();
        ~Game();
        protected virtual void Dispose(bool disposing);
        protected virtual bool ShowMissingRequirementMessage(Exception exception);
        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;
        public event EventHandler<EventArgs> Exiting;
        public event EventHandler<EventArgs> Disposed;
    }
}
