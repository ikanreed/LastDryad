using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LastDryad.UI
{
    interface IMenuOption
    {
        IMenuRenderer Renderer{get;}
        void action();
    }
}
