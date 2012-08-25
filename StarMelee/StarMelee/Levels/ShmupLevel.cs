using System;
using System.Collections.Generic;
using System.Linq;
using BaseGame;
using BaseGame.Levels;
using Microsoft.Practices.ServiceLocation;

namespace StarMelee.Levels
{
    abstract class ShmupLevel:ILevel
    {
        protected readonly ShmupGameState _gameState;

        protected List<Wave> Waves;
        public ShmupLevel(ShmupGameState gameState)
        {
            _gameState = gameState;
        }

        public virtual void Update(float time)
        {
            foreach (var wave in (Waves.Where(x => !x.Launched && x.LaunchTime < time)))
            {
                wave.Launch();
            }
            
        }
    }
}
