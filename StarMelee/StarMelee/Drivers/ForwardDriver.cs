using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Drivers;
using BaseGame.Functions;
using Microsoft.Xna.Framework;
using StarMelee.Actors.Pawns;

namespace StarMelee.Drivers
{
    class ForwardeDriver : IDriver<Ship>
    {

        private BaseFunction _path;
        public ForwardeDriver(Vector3 rotation)
        {

            _path = new Forward() { Scale = 40};
            _path.Rotation = rotation;

        }



        public void Update(Ship pawn, float time)
        {

            pawn.Movement= _path.GetStep(time);
            pawn.Rotation = _path.GetRotation(time);
            pawn.Fire(0);
        }
    }
}
