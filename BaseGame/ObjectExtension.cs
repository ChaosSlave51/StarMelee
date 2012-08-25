using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGame
{
    public static class ObjectExtension
    {
        public static void SetProperty<T>(this object o, string property, T value)
        {
            o.GetType().GetProperty(property).SetValue(o, value, null);
        }
    }
}
