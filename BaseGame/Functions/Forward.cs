using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseGame.Functions
{
    public class Forward:BaseFunction
    {
        protected override Vector3 GetPosition(float time)
        {
            Vector3 ret;
            ret.X = 0;
            ret.Y = time;
            ret.Z = 0;
            return ret;
        }   
    }
}
