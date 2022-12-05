using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pac_man
{
    public enum GhostMode
    {
        Chase,
        Scatter,
        Frightened,
        Eaten
    }

    public static class GhostModeTimer
    {
        public static Dictionary<GhostMode, int> Time = new Dictionary<GhostMode, int>()
        {
            { GhostMode.Chase, 30000 },
            { GhostMode.Scatter, 10000 },
            { GhostMode.Frightened, 10000 },
            { GhostMode.Eaten, int.MaxValue }
        };
    }
}
