using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.DTOs
{
    public class RoverRequestDTO
    {
        public Position StartingPosition { get; set; }
        internal List<char> Commands { get; set; }

        public RoverRequestDTO(Position startingPosition, List<char> commands)
        {
            StartingPosition = new Position(startingPosition);
            Commands = commands;
        }
    }
}
