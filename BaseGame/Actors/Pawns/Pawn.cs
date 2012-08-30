using System;
using System.Collections.Generic;
using System.Reflection;
using BaseGame.Drivers;
using BaseGame.Resources;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Pawns
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BasePawn : BaseActor,INeedsResources
    {
        private readonly string _resourcePath;
        protected Model MainModel;
        protected int DamagedFrames = 0;
      

    
#if DEBUG
        private Model _collisionSphereModel;//sometimes not used if collision shere display is comented out
#endif 
        public bool Alive = true;
        public bool Corporeal = true;
        protected IDriver Driver
        {
            get { return _driver; }
            set { _driver = value;
                if(value!=null)
                    updateMethod = value.GetType().GetMethod("Update");
            }
        }

    

        public Vector3 Movement{get;set;}

        private  MethodInfo updateMethod;
        private IDriver _driver;

        public BasePawn(string resourcePath, IDriver driver = null, Vector3 rotation= new Vector3())
        {
            _resourcePath = resourcePath;


            Movement = new Vector3();
            Driver = driver;
            Rotation = rotation;
        }

        protected override void ResolveResources()
        {
#if DEBUG
            _collisionSphereModel = ServiceLocator.Current.GetInstance<Model>("Models/Debug/sphere");
#endif 
            MainModel = ServiceLocator.Current.GetInstance<Model>(_resourcePath);

        }

        public void Draw(Camera camera)
        {
            ResolveResourcesIfNeeded();


            RenderModel(MainModel, camera,effect=>
                                              {
                                                  
                                                  //effect.FogEnabled = true;
                                                  if (DamagedFrames>0)
                                                  {
                                                      effect.EmissiveColor = new Vector3(1, 1, 1);
                                                      //effect.FogEnabled = true;
                                                      //effect.FogColor = Color.White.ToVector3();
                                                      DamagedFrames--;
                                                  }
                                                  else
                                                  {
                                                      effect.EmissiveColor = new Vector3(0, 0, 0);
                                                      //effect.FogEnabled = false;
                                                  }

                                                  effect.EnableDefaultLighting();

                                                  effect.PreferPerPixelLighting = true;

                                                  //sets location of model
                                                  effect.World = Helper.CreateRotationXYZ(BaseRotation) *
                                                                 Helper.CreateRotationXYZ(Rotation) *
                                                                 Matrix.CreateScale(Scale * BaseScale) *
                                                                 Matrix.CreateTranslation(BasePosition + Position);
                                                  effect.Projection = camera.CamperaProjectionMatrix;
                                                  effect.View = camera.CameraViewMatrix;
                                              });
            
#if DEBUG
            //if (Corporeal)
            //{
            //    foreach (var collisionSphere in CollisionSpheres)
            //    {
            //        RenderModel(CollisionSphereModel, camera, (effect) =>
            //                                                      {
            //                                                          effect.EnableDefaultLighting();

            //                                                          effect.PreferPerPixelLighting = true;

            //                                                          //sets location of model
            //                                                          effect.World =
            //                                                              Helper.CreateRotationXYZ(BaseRotation) *
            //                                                              Helper.CreateRotationXYZ(Rotation) *
            //                                                              Matrix.CreateScale(collisionSphere.Radius) *
            //                                                              Matrix.CreateTranslation(BasePosition +
            //                                                                                       Position +
            //                                                                                       collisionSphere.
            //                                                                                           Position);
            //                                                          effect.Projection = camera.CamperaProjectionMatrix;
            //                                                          effect.View = camera.CameraViewMatrix;
            //                                                      });
            //    }
            //}

#endif
        }

        private void RenderModel(Model model,Camera camera,Action<BasicEffect> setEffect=null)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    if (setEffect != null) setEffect(effect);
                }
                mesh.Draw();
            }
        }

        public virtual void Update(float time)
        {
            Position += Movement;
            Movement = Vector3.Zero;
            
            if(Driver!=null)
            {
               
                updateMethod.Invoke(Driver, new object[] { this, time });
            }
     
        }

        public virtual List<Sphere> CollisionSpheres { get; set; }

        public virtual void Collided(object o)
        {
            Die();
        }
        public virtual bool Die()
        {
            if (Alive == true)
            {
                Alive = false;
                return true;
            }
            return false;   

        }



        public void Kill()
        {
            Alive = false;
        }


        public virtual IEnumerable<Resource> ResourcePaths()
        {
            return new Resource[] { new Resource(_resourcePath, typeof(Model)),
#if DEBUG
            new Resource("Models/Debug/sphere",typeof(Model))
#endif
            };
        }
    }
}
