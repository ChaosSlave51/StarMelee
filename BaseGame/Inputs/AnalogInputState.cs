using System;
using System.Linq.Expressions;

namespace BaseGame.Inputs
{
    public class AnalogInputState : BaseInputState<Single, Single>, IInputState
    {
        private readonly int _direction;

        public AnalogInputState(Expression<Func<Single>> expression, int direction)
            : base(expression)
        {
            _direction = direction;
        }

        public override float Value()
        {
            return _compiled.Invoke();
        }

        public bool BoolValue()
        {
            return  Math.Sign(Value()) == _direction;
        }

        public float SingleValue()
        {
            return Value();
        }
    }
    
}
