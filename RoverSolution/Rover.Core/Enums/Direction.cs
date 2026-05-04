using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rover.Core.Enums
{
    public enum Direction
    {
        [Display(Name = "North")]
        N,

        [Display(Name = "East")]
        E,

        [Display(Name = "South")]
        S,

        [Display(Name = "West")]
        W,
    }
}
