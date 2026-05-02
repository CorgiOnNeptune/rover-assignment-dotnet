using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Commands
{
    internal class RotateLeftCommand : IRoverCommand
    {
        public override void Execute(MarsRover rover, Plateau plateau)
        {
            Position pos = rover.Position;
            Position newPos = new Position(pos.X, pos.Y, Utils.TurnCounterClockwise(pos.Direction));
            rover.UpdatePosition(newPos);
        }
    }
}
