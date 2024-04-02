using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong.Models;

public enum PowerUpEffects
{
    None = 0,
    InverseControls = 1,
    SizeUp = 2,
    SizeDown = 3,
    Freeze = 4,
    ChangeBallSpeed = 5,
    SlowDown = 6,
    SpeedUp = 7,
    BlackHole = 8,
    Spikes = 9,
}

public enum PowerUpType
{
    Random = 0,
    Friendly = 1,
    Lethal = 2,
}