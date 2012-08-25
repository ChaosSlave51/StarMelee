using System;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Serialization;
using BaseGame.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseGame.Inputs
{
    public  class InputMapper
    {
        static InputMapper _instance;
        private InputMapper()
        {
        }
        public static InputMapper GetInstance()
        {
            return _instance ?? (_instance = new InputMapper());
        }

        public void  Map(IActionState actionState)
        {
            //Read config file
            Config.Config config;

            using (Stream stream = new FileStream(Directory.GetCurrentDirectory() + @"\config.xml", FileMode.Open))
            {
                XmlSerializer serialiser = new XmlSerializer(typeof(Config.Config));
                config = (Config.Config)serialiser.Deserialize(stream);

                foreach (ConfigInputAction action in config.Input.Actions)
                {
                    if (action.Binding.Category == "Buttons")
                    {
                        Expression<Func<ButtonState>> button = CreateExpression<ButtonState>(action);
                        actionState.SetProperty(action.Name, new BoolInputState(button));
                    }
                    else if (action.Binding.Category == "Analog")
                    {
                        Expression<Func<Single>> analog = CreateExpression<Single>(action);
                        actionState.SetProperty(action.Name, new AnalogInputState(analog, GetSigh(action.Binding.State)));
                    }
                    
                }

            }
         
        }
        private int GetSigh(string s)
        {
            if (s == "+")
                return 1;
            else if (s == "-")
                return -1;
            else
                return 0;
        }

        public Expression<Func<T>> CreateExpression<T>(ConfigInputAction action)
        {
            MethodCallExpression inputType; 
            
            if(true)
            {
                var paramiters = Expression.Constant(PlayerIndex.One,typeof(PlayerIndex));

                inputType = Expression.Call(null, typeof(GamePad).GetMethod("GetState", new[] { typeof(PlayerIndex) }), paramiters);
            }
            //MemberExpression category = Expression.Property(inputType, typeof(GamePadState).GetProperty(action.Binding.Category));

            Expression button =GetExpressionByDotSyntax(action.Binding.Input,inputType);

          
            
            var ret = Expression.Lambda<Func<T>>(button, new ParameterExpression[] {});
            

            return ret;
        }

        private Expression GetExpressionByDotSyntax(string path, Expression exp)
        {
            var parts = path.Split('.');
            var ret = exp;
            foreach (var part in parts)
            {
                var property = ret.Type.GetProperty(part);
                
                if(property==null)
                {
                    var field = ret.Type.GetField(part);
                    ret = Expression.Field(ret, field);
                }
                else
                {
                    ret = Expression.Property(ret, property);
                }

            }
            return ret;
        }
    }


}
