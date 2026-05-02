using Rover.Core.DTOs;
using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Services
{
    public static class RawRequestParserService
    {
        public static SimulationRequestDTO Parse(string rawInput)
        {
            // Format expected input
            List<string> rawInputList = rawInput.Split('\n').Select(l => l.Trim()).ToList();

            // Handle first line of raw input (plateau bounds)
            string[] rawPlateau = rawInputList[0].Split(" ");

            if (!int.TryParse(rawPlateau[0].ToString(), out int plateauX))
            {
                // TODO: Decide throw or have a fallback "default" value
                throw new FormatException("Invalid input for plateauX");
            }

            if (!int.TryParse(rawPlateau[1].ToString(), out int plateauY))
            {
                throw new FormatException("Invalid input for plateauY");
            }

            // Remove first element of input (plateau bounds)
            rawInputList.RemoveAt(0);

            // Split rovers into groups of 2 (First line starting position, second line commands)
            IEnumerable<string[]> roverGroups = rawInputList.Chunk(2);
            List<RoverRequestDTO> rovers = new List<RoverRequestDTO>();

            foreach (string[] group in roverGroups)
            {
                string[] coords = group[0].Split(" ");
                List<char> instructions = group[1].ToList();

                // Catch errors and initialize return variables if valid conversion.
                if (!int.TryParse(coords[0].ToString(), out int roverX))
                {
                    // TODO: Decide, throw or just return default values of 0 and N for bad input ?
                    throw new FormatException("Invalid input for rover starting x.");
                }

                if (!int.TryParse(coords[1].ToString(), out int roverY))
                {
                    throw new FormatException("Invalid input for rover starting x.");
                }

                if (!Enum.TryParse(coords[2].ToString(), out Direction roverDirection))
                {
                    throw new FormatException("Invalid input for rover starting direction.");
                }

                Position roverPosition = new Position(roverX, roverY, roverDirection);

                RoverRequestDTO rover = new RoverRequestDTO(roverPosition, instructions);
                rovers.Add(rover);
            }
            
            return new SimulationRequestDTO(plateauX, plateauY, rovers);
        }
    }
}
