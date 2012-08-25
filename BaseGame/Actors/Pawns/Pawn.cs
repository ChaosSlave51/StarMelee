using System;
using System.Collections.Generic;
using System.Reflection;
using BaseGame.Drivers;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseGame.Actors.Pawns
{
    /// <summary>
    /// 
    /// </summary>
    public class BasePawn : BaseActor
    {
        private Model MainModel;
        
#if DEBUG
        private Model CollisionSphereModel;
#endif 
        public bool Alive = true;
        public bool Corporeal = true;
        protected IDriver Driver
        {
            get { return _driver; }
            set { _driver = value;
             updateMethod = Driver.GetType().GetMethod("Update");
            }
        }

    

        public Vector3 Movement{get;set;}

        private  MethodInfo updateMethod;
        private IDriver _driver;

        public BasePawn(Model _model, IDriver driver = null, Vector3 rotation= new Vector3())
        {
#if DEBUG
            CollisionSphereModel = ServiceLocator.Current.GetInstance<Model>("Models/Debug/sphere");
#endif 

            MainModel = _model;
            Movement = new Vector3();
            Driver = driver;
            Rotation = rotation;

 

        }
        public void Draw(Camera camera)
        {
            RenderModel(MainModel, camera,effect=>
                                              {
                                                  

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
            //                                                              Helper.CreateRotationXYZ(BaseRotation)*
            //                                                              Helper.CreateRotationXYZ(Rotation)*
            //                                                              Matrix.CreateScale(collisionSphere.Radius)*
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

        public virtual void Collided()
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
    }
}
