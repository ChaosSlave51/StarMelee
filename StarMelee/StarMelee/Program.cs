using System;
using BaseGame.IOC;

using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using StarMelee.IOC;

using Ninject;
namespace StarMelee
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static BaseGame.BaseGame Game { get; set; }
        static void Main(string[] args)
        {
            try
            {
                //move maybe?
                var kernel =new StandardKernel(new GameModule(),new StarMeleeModule());
                
                NinjectServiceLocator locator = new NinjectServiceLocator(kernel);
                ServiceLocator.SetLocatorProvider(() => (IServiceLocator)locator);
                //end move
                
                Game = new BaseGame.BaseGame(new Director());
                Game.Run();
            }
            finally
            {
                Game.Dispose();
            }


        }
    }
#endif
}

