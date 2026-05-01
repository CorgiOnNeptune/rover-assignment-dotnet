using Rover.Core.Interfaces;
using Rover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Rover.Core.Models;

namespace Rover.Core.Commands
{
    public class MoveCommand : IRoverCommand
    {
        public override void Execute(MarsRover rover, Plateau plateau)
        {
            Position newPosition = new Position(rover.Position);

            switch (newPosition.Direction)
            {
                case Direction.North: 
                    newPosition.Y++;
                    break;

                case Direction.East: 
                    newPosition.X++;
                    break;

                case Direction.South: 
                    newPosition.Y--;
                    break;

                case Direction.West: 
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
