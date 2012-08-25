using System;
using Microsoft.Xna.Framework;

namespace BaseGame.Functions
{
    public class BaseFunction
    {
        public Vector3 Rotation { get; set; }
        public float Scale = 1;
        public float Speed = 1;
        protected float lastTime = -1;
        protected Vector3 lastStep;

        protected float? StartOfsetTime=null;

    public Vector3 GetStep(float time)
    {
        float runtTime = FuncRunTime(time);


        if (time != lastTime)
            {
                Vector3 ret = GetPosition(runtTime * Speed) - GetPosition(runtTime * Speed - 1);
                Matrix rotationMatrix = Matrix.CreateFromYawPitchRoll(Rotation.Y, Rotation.X, Rotation.Z);
                ret = Vector3.Transform(ret, rotationMatrix);
                ret = ret * Scale;
                lastTime = runtTime;
                lastStep = ret;
                return ret;
            }
            else
            {
                return lastStep;
            }
        }
        public Vector3 GetRotation(float time)
        {
            Vector3 ret = DoRotation(time);//Rotation + DoRotation(time);
            return ret;
        }


        protected float FuncRunTime(float time)
        {
            if (StartOfsetTime == null)
            {
                StartOfsetTime = time;
            }
            return time - StartOfsetTime.Value;

        }

        protected virtual Vector3 GetPosition(float time)
        {
            if (StartOfsetTime == null)
            {
                StartOfsetTime = time;
            }


            Vector3 ret;
            ret.X = 100*(float)Math.Cos(time/10); //state.ThumbSticks.Left.X;
            ret.Y = 100 * (float)Math.Sin(time/10);// state.ThumbSticks.Left.Y;
            ret.Z = 0;
            return ret;
        }


        protected virtual Vector3 DoRotation(float time)
        {
            Vector3 ret;
            Vector3 step = GetStep(time);
            step.Normalize();

            ret.X = (float)Math.Atan(step.Z / (Math.Sqrt(step.X * step.X + step.Y * step.Y))); //;
            ret.Y = 0;//;
            ret.Z = -(float)Math.Atan(step.X / step.Y);

            if (float.IsNaN(ret.X))
                ret.X = 0;
            if (float.IsNaN(ret.Y))
                ret.Y = 0;
            if (float.IsNaN(ret.Z))
                ret.Z = 0;

            if (step.Y < 0)
            {
                ret.Z += MathHelper.Pi;
                //ret.X *= -1;
            }
            return ret;
            
        }
        
    }
}
