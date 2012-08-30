using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Actors.Pawns;
using Microsoft.Xna.Framework;

namespace BaseGame.Physics
{
    public class CollisionHelper
    {
        public  void CheckCollisions(BasePawn i1, IEnumerable<BasePawn> l2)
        {
            CheckCollisions(new[] { i1 }, l2);
        }
        public void CheckCollisions(IEnumerable<BasePawn> l1, IEnumerable<BasePawn> l2)
        {
            foreach (var i1 in l1.Where(x => x.Alive && x.Corporeal))
            {
                foreach (var i2 in l2.Where(x => x.Alive && x.Corporeal))
                {
                    PawnsIntersect(i1, i2);
                }
            }
        }
        private void PawnsIntersect(BasePawn i1, BasePawn i2)
        {
            float maxMovement = Math.Max(i1.Movement.Length(), i2.Movement.Length());
            float i1Smallest = i1.CollisionSpheres.Min(x => x.Radius);
            float i2Smallest = i2.CollisionSpheres.Min(x => x.Radius);
            float smallestCollisionRadius = i1Smallest + i2Smallest;
            int steps = (int)Math.Ceiling(maxMovement / smallestCollisionRadius);

            for (float i = 0; i < steps; i++)//this loop is the sub steps of the physics
            {
                foreach (BoundingSphere s1 in i1.CollisionSpheres)
                {
                    foreach (var s2 in i2.CollisionSpheres)
                    {
                        if (SpheresIntersect(i1.Position - i1.Movement * (i / steps), s1, i2.Position - i2.Movement * (i / steps), s2))
                        {
                            i1.Collided(i2);
                            i2.Collided(i1);
                            return;
                        }
                    }
                }
            }
        }

        private bool SpheresIntersect(Vector3 p1, BoundingSphere s1, Vector3 p2, BoundingSphere s2)
        {
            var shere1 = s1.Transform(Matrix.CreateTranslation(p1));
            var shere2 = s2.Transform(Matrix.CreateTranslation(p2));
            return shere1.Intersects(shere2);
        }

        public bool OnCamera(BasePawn pawn, Camera camera, Vector3 offset)
        {
            return pawn.CollisionSpheres.All(
                x => camera.BindingFrustum.Contains(x.Transform(Matrix.CreateTranslation(pawn.Position + offset))) == ContainmentType.Contains
            );
        }
    }

}
