using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseGame;
using BaseGame.Inputs;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Ninject.Modules;

using StarMelee.Inputs;

namespace StarMelee.IOC
{
    public class StarMeleeModule : NinjectModule
    {
        public override void Load()
        {

            Bind<ActionState>().ToMethod(context =>
                                             {
                                                 ActionState actionState = new ActionState();
                                                 InputMapper mapper = InputMapper.GetInstance();
                                                 mapper.Map(actionState);
                                                 return actionState;
                                             }
                ).InSingletonScope();

            //game models
            //BindResource<Model>("Models/Ships/pea_proj");
            
            //BindResource<Model>("Models/Ships/p1_saucer");
            
            //BindResource<SpriteFont>("fonts/LcdBold");
            //BindResource<SoundEffect>("Audio/Weapons/laser-zap-01");
            //BindResource<SoundEffect>("Audio/Ships/bombexplosion");
            //BindResource<SoundEffect>("Audio/Ships/scream-02");

            //BindResource<SpriteFont>("fonts/7th Service");
            //BindResource<Texture2D>("Sprites/Controllers/XBox/small_start");
            //BindResource<Song>("Audio/Music/An_Infinite_Univers");
            BindResource<Effect>("Shaders/MotionBlur");
        }

        private void BindResource<T>(string key)
        {
            Bind<T>().ToMethod(context =>
            {
                return Program.Game.Content.Load<T>(key);
            }).InSingletonScope().Named(key);  
        }

    }
}
