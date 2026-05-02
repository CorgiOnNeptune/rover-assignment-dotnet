using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class RoverRequest
    {
        public Position StartingPosition { get; set; }
        internal List<char> Commands { get; set; }

        public RoverRequest(Position startingPosition, List<char> commands)
        {
            StartingPosition = new Position(startingPosition);
            Commands = commands;
        }
    }
}
