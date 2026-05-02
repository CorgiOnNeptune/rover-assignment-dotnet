using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Commands
{
    internal class RotateRightCommand : IRoverCommand
    {
        public override void Execute(MarsRover rover, Plateau plateau)
        {
            Position pos = rover.Position;
            Position newPos = new Position(pos.X, pos.Y, Utils.TurnClockwise(pos.Direction));
            rover.UpdatePosition(newPos);
        }
    }
}
