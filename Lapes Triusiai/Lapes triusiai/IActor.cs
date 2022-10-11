using System.Collections.Generic;
using System.Drawing;

namespace Lapes_triusiai
{
    internal interface IActor
    {

        void Act(List<IActor> newActors);
        bool IsAlive { get; }
        Point Location { get; }
       
    }
}
