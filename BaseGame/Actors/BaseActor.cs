using Microsoft.Xna.Framework;

namespace BaseGame.Actors
{
    /// <summary>
    /// an actor is anything in the world that has physical form
    /// </summary>
    public abstract class BaseActor
    {
        public BaseActor()
        {
        }
        protected bool ResourcesLoaded = false;
        public virtual Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 BaseRotation { get; set; }
        public Vector3 BasePosition { get; set; }
        public float Scale = 1.0f;
        public float BaseScale = 1.0f;
        public Vector3 TotalPosition 
        {
            get
            {
                return BasePosition + Position;
            }
        }
        public Vector3 TotalRotation
        {
            get
            {
                return BaseRotation + Rotation;
            }
        }
        public float TotalScale
        {
            get
            {
                return BaseScale*Scale;
            }
        }

        public  void ResolveResourcesIfNeeded()
        {
            if(!ResourcesLoaded)
            {
                ResolveResources();
                ResourcesLoaded = true;
            }
        }

        protected abstract void ResolveResources();

    }
}
