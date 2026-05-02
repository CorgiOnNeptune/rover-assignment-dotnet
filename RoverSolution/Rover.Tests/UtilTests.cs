using Rover.Core;
using Rover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Tests;

public class UtilTests
{
    [Fact]
    public void TurnClockwise_ValueOfWest_ShouldBeNorth()
    {
        Assert.Equal(Direction.N, Utils.TurnClockwise(Direction.W));
    }

    [Fact]
    public void TurnCounterClockwise_ValueOfNorth_ShouldBeWest()
    {
        Assert.Equal(Direction.W, Utils.TurnCounterClockwise(Direction.N));
    }
}
