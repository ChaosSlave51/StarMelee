using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseGame.Functions
{
    public class Wave : BaseFunction
    {
        public float Amplitude=10;
        public float Frequency=100;
        public float? StartPhase;

        protected override Vector3 GetPosition(float time)
        {
            float runtTime = FuncRunTime(time);
            Vector3 ret;
            ret.X = Amplitude * (float)Math.Sin((runtTime / Frequency));
            ret.Y = runtTime;
            ret.Z = 0;
            return ret;
        }
    }
}
