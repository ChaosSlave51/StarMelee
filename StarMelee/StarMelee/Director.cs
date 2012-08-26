using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BaseGame;

using BaseGame.Screens;
using Microsoft.Xna.Framework;

using StarMelee.Screens;

namespace StarMelee
{
    class Director : IDirector
    {
        private GraphicsDeviceManager _graphics;
        public BaseScreen CurrentScreen { get; set; }
        private bool _screenChange = false;

        private void SwitchScreen(BaseScreen newScreen)
        {
            _screenChange = true;
            CurrentScreen.Dispose();
            newScreen.CreateBindings();

            newScreen.Initialise(_graphics);

            CurrentScreen = newScreen;

            _screenChange = false;
        }

        private void OpeningOnStartGame()
        {
            var screen = new Shmup();
            screen.Lose += ShmupOnLose;
            SwitchScreen(screen);
        }
        private void ShmupOnLose()
        {
            var screen = new Opening();
            SwitchScreen(screen);

            screen.StartGame += OpeningOnStartGame;
        }
        public void Initialise(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;

            var opening = new Opening();
            opening.StartGame += OpeningOnStartGame;
            opening.CreateBindings();
            CurrentScreen = opening;


            CurrentScreen.Initialise(graphics);
        }

        public void Update(GameTime time)
        {
            if(!_screenChange)
                CurrentScreen.Update(time);
        }
        public void Draw(GameTime time)
        {
            if (!_screenChange)
                CurrentScreen.Draw(time);
        }

        public void LoadContent()
        {
            
        }


    }
}
