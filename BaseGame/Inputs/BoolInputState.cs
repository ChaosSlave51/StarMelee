using System;
using System.Linq.Expressions;
using Microsoft.Xna.Framework.Input;

namespace BaseGame.Inputs
{
    public class BoolInputState : BaseInputState<ButtonState, bool>, IInputState
    {
        public BoolInputState(Expression<Func<ButtonState>> expression) : base(expression)
        {
        }

        public override bool Value()
        {
            return _compiled.Invoke() == ButtonState.Pressed;
        }

        public bool BoolValue()
        {
            return Value();
        }

        public float SingleValue()
        {
            return Value() ? 1 : -1;
        }
    }
}
