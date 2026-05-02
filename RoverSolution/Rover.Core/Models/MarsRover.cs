using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class MarsRover
    {
        public Position Position { get; set; }
        public List<Position> PositionHistory { get; set; } = new List<Position>();

        public MarsRover(Position position)
        {
            Position = new Position(position.X, position.Y, position.Direction);
            PositionHistory.Add(new Position(Position));
        }

        public void UpdatePosition(Position position)
        {
            Position = position;
            PositionHistory.Add(new Position(position));
        }
    }
}
