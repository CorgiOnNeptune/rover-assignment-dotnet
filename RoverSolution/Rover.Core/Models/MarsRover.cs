using Rover.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class MarsRover
    {
        public Position Position { get; set; }
        public List<Position> PositionHistory { get; set; } = new List<Position>();

        public MarsRover(int startingX, int startingY, Direction startingDirection)
        {
            Position = new Position(startingX, startingY, startingDirection);
            PositionHistory.Add(Position);
        }

        public void UpdatePosition(Position position)
        {
            Position = position;
            PositionHistory.Add(position);
        }
    }
}
