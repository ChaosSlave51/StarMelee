using System;
using BaseGame.Functions;
using Microsoft.Xna.Framework;

namespace StarMelee.Functions
{
    class DeathSpiral:BaseFunction
    {
        public float Radious =500;
        protected override Vector3 GetPosition(float time)
        {
            Vector3 ret;
            ret.X = Radious * (float)Math.Cos(time / MathHelper.TwoPi);
            ret.Y = Radious * (float)Math.Sin(time / MathHelper.TwoPi) + time;
            ret.Z = -time*time*10 ;
            return ret;
        }

    }
}
