using System.Collections.Generic;
using BaseGame;
using BaseGame.Actors.Pawns;
using BaseGame.Drivers;
using BaseGame.Functions;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarMelee.Actors.Pawns
{
    class BaseBullet : BasePawn, INeedsResources
    {
       
        public BaseBullet(Vector3 rotation = new Vector3())
            : base("Models/Ships/pea_proj",
            new FunctionDriver(new Forward() {Scale = 200},
                rotation),rotation)
        {

            Scale = 1;
            CollisionSpheres = new List<Sphere>() { new Sphere(new Vector3(), 25) };
        }

        public override void Update(float time)
        {
            base.Update(time);
     
            if (Position.Length() > 20000) 
                Kill();
    

        }


    }
}
