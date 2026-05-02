using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Rover.Core.Enums
{
    public enum Direction
    {
        [Description("North")]
        N,

        [Description("East")]
        E,

        [Description("South")]
        S,

        [Description("West")]
        W,
    }
}
