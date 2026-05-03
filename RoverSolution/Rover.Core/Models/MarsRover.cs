using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class MarsRover
    {
        public Position Position { get; set; }
        public List<Position> PositionHistory { get; set; } = new List<Position>();

        public MarsRover(Position initialPosition)
        {
            Position = new Position(initialPosition);
            PositionHistory.Add(new Position(Position));
        }

        public void UpdatePosition(Position position)
        {
            Position = position;
            PositionHistory.Add(new Position(position));
        }
    }
}
