using Rover.Core.Enums;
using Rover.Core.Interfaces;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Commands
{
    public class RotateLeftCommand : IRoverCommand
    {
        public override void Execute(MarsRover rover, Plateau plateau)
        {
            rover.Position.Direction = Utils.TurnCounterClockwise(rover.Position.Direction);
            rover.PositionHistory.Add(rover.Position);
        }
    }
}
