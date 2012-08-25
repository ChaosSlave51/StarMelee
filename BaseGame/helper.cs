using Microsoft.Xna.Framework;

namespace BaseGame
{
    static class Helper
    {
        public static Matrix CreateRotationXYZ(Vector3 Rotation)
        {
            return Matrix.CreateRotationX(Rotation.X) * Matrix.CreateRotationY(Rotation.Y) * Matrix.CreateRotationZ(Rotation.Z);
        }
    }
}


