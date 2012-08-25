using System.Collections.Generic;
using BaseGame.Actors.Pawns;

namespace BaseGame
{
    //This class maintains states that are alive all of the appication

    public interface IGameState
    {
        List<BasePawn> Pawns { get; set; }
    }

    public class GameState : IGameState
    {
        public GameState()
        {
            Pawns = new List<BasePawn>();
        }

        

        public  List<BasePawn> Pawns{get;set;}

    }
}
