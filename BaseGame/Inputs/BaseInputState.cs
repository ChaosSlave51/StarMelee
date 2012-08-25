using System;
using System.Linq.Expressions;

namespace BaseGame.Inputs
{
    public abstract  class BaseInputState<TInner,TOuter>
    {

        protected readonly Func<TInner> _compiled;
        public BaseInputState(Expression<Func<TInner>> expression)
        {
            _compiled = expression.Compile();
        }

        public abstract TOuter Value();
   
    }
}
