using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame.Physics;
using Ninject.Modules;

namespace BaseGame.IOC
{
    public class GameModule:NinjectModule
    {
        public override void Load()
        {
            Bind<CollisionHelper>().ToSelf().InSingletonScope();
        }
    }
}
