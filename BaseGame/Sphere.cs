using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace BaseGame
{
    public class Sphere
    {
        public Sphere(Vector3 position, float radius)
        {
            Position = position;
            Radius = radius;
        }
        public Vector3 Position { get; set; }
        public float Radius { get; set; }

    }
}
