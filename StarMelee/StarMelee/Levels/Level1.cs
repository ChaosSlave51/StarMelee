using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using StarMelee.Actors.Pawns;

namespace StarMelee.Levels
{
    class Level1:ShmupLevel
    {
        public Level1(ShmupGameState gameState) : base(gameState)
        {
            Waves = new List<Wave>();
            //{
            //    new Wave(new List<BaseEnemyShip>()
            //                 {
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-10000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(0,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(10000,10000,0)}
            //                 },0,_gameState ),
            //    new Wave(new List<BaseEnemyShip>()
            //                 {
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-10000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(0,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(10000,10000,0)}
            //                 },3000,_gameState ),
            //   new Wave(new List<BaseEnemyShip>()
            //                 {
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-10000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(-5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(0,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(5000,10000,0)},
            //                     new BaseEnemyShip(gameState){Position = new Vector3(10000,10000,0)}
            //                 },6000,_gameState )


            //};
            for (int i = 0; i < 30000;i=i+3000 )
            {
                Waves.Add(
                    new Wave(new List<BaseEnemyShip>()
                                 {
                                     new BaseEnemyShip(gameState,rotation:new Vector3(0,0,MathHelper.Pi)) {Position = new Vector3(-10000, 13000, 0) },
                                     new BaseEnemyShip(gameState,rotation:new Vector3(0,0,MathHelper.Pi)) {Position =   new Vector3(-5000, 13000, 0)},
                                     new PhlanixShip(gameState,rotation:new Vector3(0,0,MathHelper.Pi)) {Position = new Vector3(0, 13000, 0)},
                                     new BaseEnemyShip(gameState,rotation:new Vector3(0,0,MathHelper.Pi)) {Position =   new Vector3(5000, 13000, 0)},
                                     new BaseEnemyShip(gameState,rotation:new Vector3(0,0,MathHelper.Pi)) {Position = new Vector3(10000, 13000, 0)}
                                 }, i, _gameState));
            }
        }
    }
}
