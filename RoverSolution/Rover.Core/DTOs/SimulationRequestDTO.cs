using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.DTOs
{
    public class SimulationRequestDTO
    {
        public int PlateauMaxX;
        public int PlateauMaxY;
        public List<RoverRequestDTO> Rovers;

        public SimulationRequestDTO(int plateauMaxX, int plateauMaxY, List<RoverRequestDTO> rovers)
        {
            PlateauMaxX = plateauMaxX;
            PlateauMaxY = plateauMaxY;
            Rovers = rovers;
        }
    }
}
