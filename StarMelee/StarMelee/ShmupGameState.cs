using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Actors.Pawns;
using BaseGame.Actors.Sprites;
using BaseGame.Physics;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using StarMelee.Actors.Pawns;

namespace StarMelee
{
    class ShmupGameState 
    {

        public long Score { get; set; }

        public BasePlayerShip Player { get; set; }
        public List<BaseBullet> PlayerBullets { get; set; }
        public List<BaseEnemyShip> EnemyShips { get; set; }
        public List<BaseBullet> EnemyBullets { get; set; }
        public CollisionHelper CollisionsHelper;

        public List<BaseSprite> Hud { get; set; }
        public ShmupGameState()
        {
            Score = 0;

            CollisionsHelper = ServiceLocator.Current.GetInstance<CollisionHelper>();

            PlayerBullets = new List<BaseBullet>();
            EnemyShips = new List<BaseEnemyShip>();
            EnemyBullets = new List<BaseBullet>();
            Hud= new List<BaseSprite>();
        }

        public decimal PawnCount
        {
            get { return PlayerBullets.Count + EnemyShips.Count + EnemyBullets.Count; }

        }
        public void EachSprite(Action<BaseSprite> action)
        {
            Hud.ForEach(x => action(x));
        }
        public void EachPawn (Action<BasePawn> action)
        {
            action(Player);
            PlayerBullets.ForEach(x=>action(x));
            EnemyShips.ForEach(x => action(x));
            EnemyBullets.ForEach(x => action(x));
        }

        public void RemoveDeadPawns()
        {
            PlayerBullets.RemoveAll(x => !x.Alive);
            EnemyShips.RemoveAll(x => !x.Alive);
            EnemyBullets.RemoveAll(x => !x.Alive);
        }

        public void CheckCollisionForPawns()
        {
            CollisionsHelper.CheckCollisions(Player, EnemyBullets);
            CollisionsHelper.CheckCollisions(Player, EnemyShips);
            CollisionsHelper.CheckCollisions(PlayerBullets, EnemyShips);

        }

      
    }
}
