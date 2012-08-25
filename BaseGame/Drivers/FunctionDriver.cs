using BaseGame.Actors.Pawns;
using BaseGame.Functions;
using Microsoft.Xna.Framework;

namespace BaseGame.Drivers
{
    public class FunctionDriver:IDriver<BasePawn>
    {
        private readonly BaseFunction _path;

        public FunctionDriver(BaseFunction path, Vector3 rotation)
        {
            _path = path;
            _path.Rotation = rotation;
        }

        public virtual void Update(BasePawn pawn, float time)
        {
            pawn.Movement= _path.GetStep(time);
            pawn.Rotation = _path.GetRotation(time);
        }


    }
}
