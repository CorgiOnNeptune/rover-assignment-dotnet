using Rover.Core;
using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Tests;

public class UtilTests
{
    [Fact]
    public void PositionIsValid_ValueOfNegativeXY_ShouldBeFalse()
    {
        Plateau plateau = new Plateau(5, 5);
        Position badPosX = new Position(-1, 3, Direction.N);
        Position badPosY = new Position(3, -1, Direction.N);

        Assert.False(badPosX.IsValid(plateau));
        Assert.False(badPosY.IsValid(plateau));
    }

    [Fact]
    public void PositionIsValid_OutOfBounds_ShouldBeFalse()
    {
        Plateau plateau = new Plateau(5, 5);
        Position badPosX = new Position(10, 3, Direction.N);
        Position badPosY = new Position(3, 10, Direction.N);

        Assert.False(badPosX.IsValid(plateau));
        Assert.False(badPosY.IsValid(plateau));
    }

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
