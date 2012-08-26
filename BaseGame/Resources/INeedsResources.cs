using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseGame.Resources
{
    public interface INeedsResources
    {
        IEnumerable<Resource> ResourcePaths();

    }
}
