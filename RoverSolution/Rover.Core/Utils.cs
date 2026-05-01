using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core
{
    public static class Utils
    {
        /// <summary>
        /// Turn along the "compass" based on just 4 cardinal directions, but could become extensible with NE, SE, etc. 
        /// Modulo to normalize the equation and make sure we stay within the enum range for negative or positive calculations.
        /// e.g.  North -> West would calc as  (0 + (-1) + 4) % 4 = 3 (West)
        /// </summary>
        public static Direction Turn(Direction initialDirection, int steps)
        {
            int directionCount = Enum.GetNames<Direction>().Length;

            // Modulo here makes sure negative steps stay inside enum range.
            int directionSteps = (int)(initialDirection + steps) % directionCount;

            // Modulo here keeps positive steps within enum range.
            return (Direction)((directionSteps + directionCount) % directionCount);
        }

        public static Direction TurnClockwise(Direction initialDirection)
        {
            return Turn(initialDirection, 1);
        }

        public static Direction TurnCounterClockwise(Direction initialDirection)
        {
            return Turn(initialDirection, -1);
        }
    }
}
