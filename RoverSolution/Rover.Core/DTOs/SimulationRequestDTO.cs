using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.DTOs
{
    public class SimulationRequestDTO
    {
        public int PlateauMaxX { get; set; }
        public int PlateauMaxY { get; set; }
        public List<RoverRequestDTO> Rovers { get; set; }

        public SimulationRequestDTO(int plateauMaxX, int plateauMaxY, List<RoverRequestDTO> rovers)
        {
            PlateauMaxX = plateauMaxX;
            PlateauMaxY = plateauMaxY;
            Rovers = rovers;
        }
    }
}
