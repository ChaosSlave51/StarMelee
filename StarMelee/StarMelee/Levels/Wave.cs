using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using BaseGame;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using StarMelee.Actors.Pawns;

namespace StarMelee.Levels
{
    class Wave : INeedsResources
    {
        private readonly ShmupGameState _gameState;
        private List<BaseEnemyShip> _ships;
        public IEnumerable<BaseEnemyShip> Ships
        {
            get { return _ships.AsReadOnly(); }
            private set { _ships =value.ToList(); }
        }

        public  float LaunchTime { get;private set; }
        public bool Launched { get; set; }
        public Wave(List<BaseEnemyShip> ships, float launchTime, ShmupGameState gameState)
        {
            _gameState = gameState;
 
            LaunchTime = launchTime;
            Ships = ships;
        }


        internal void Launch()
        {
            Launched = true;
            _gameState.EnemyShips.AddRange(Ships);
        }

        public IEnumerable<Resource> ResourcePaths()
        {
            return Resource.Combine(Ships);
        }
    }
}
