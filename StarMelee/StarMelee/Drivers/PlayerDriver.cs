using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Drivers;
using Microsoft.Practices.ServiceLocation;
using StarMelee.Actors.Pawns;
using StarMelee.Inputs;

namespace StarMelee.Drivers
{
    class PlayerDriver:IDriver<BasePlayerShip>
    {
        public PlayerDriver()
        {
            _actionState = ServiceLocator.Current.GetInstance<ActionState>();
        }
        private ActionState _actionState;
        public void Update(BasePlayerShip pawn, float time)
        {

            if (_actionState.Left.BoolValue())
                pawn.MoveLeft();
            if (_actionState.Right.BoolValue())
                pawn.MoveRight();
            if (_actionState.Up.BoolValue())
                pawn.MoveForward();
            if (_actionState.Down.BoolValue())
                pawn.MoveBack();
            if (_actionState.Fire1.BoolValue())
                pawn.Fire(0);
            if (_actionState.Fire2.BoolValue())
                pawn.Fire(1);
            if (_actionState.Fire3.BoolValue())
                pawn.Fire(2);
            if (_actionState.Fire4.BoolValue())
                pawn.Fire(3);
        }
    }
}
