using Rover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Rover.Core.Models;

namespace Rover.Core.Commands
{
    internal class MoveCommand : IRoverCommand
    {
        public override void Execute(MarsRover rover, Plateau plateau)
        {
            Position newPosition = new Position(rover.Position);

            switch (newPosition.Direction)
            {
                case Direction.N:
                    newPosition.Y++;
                    break;

                case Direction.E:
                    newPosition.X++;
                    break;

                case Direction.S:
                    newPosition.Y--;
                    break;

                case Direction.W:
                    newPosition.X--;
                    break;

                default:
                    break;
            };

            if (newPosition.IsValid(plateau))
            {
                rover.UpdatePosition(newPosition);
            }
        }
    }
}
