using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class RoverRequest
    {
        public Position StartingPosition { get; set; }
        public string Commands { get; set; }

        public RoverRequest(Position startingPosition, string commands)
        {
            StartingPosition = new Position(startingPosition);
            Commands = commands;
        }
    }
}
