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

        public Director()
        {

            var opening = new Opening();
            opening.StartGame+=OpeningOnStartGame;
            CurrentScreen = opening;

            //CurrentScreen.StartGame += StartGameEvent;
        }

        private void OpeningOnStartGame()
        {
            var thread = new Thread(() =>
                                        {
                                            var shmup = new Shmup();
                                            shmup.Initialise(_graphics);
                                            shmup.LoadContent();
                                            CurrentScreen.Dispose();
                                            CurrentScreen = shmup;
                                        });
            thread.Start();
        }

        public void Initialise(GraphicsDeviceManager graphics)
        {
            _graphics = graphics;
            CurrentScreen.Initialise(graphics);
        }

        public void Update(GameTime time)
        {
            CurrentScreen.Update(time);
        }
        public void Draw(GameTime time)
        {
            CurrentScreen.Draw(time);
        }

        public void LoadContent()
        {
            CurrentScreen.LoadContent();
        }


    }
}
