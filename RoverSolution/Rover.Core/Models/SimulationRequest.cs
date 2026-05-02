using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class SimulationRequest
    {
        public int PlateauMaxX { get; set; }
        public int PlateauMaxY { get; set; }
        public List<RoverRequest> Rovers { get; set; }

        public SimulationRequest(int plateauMaxX, int plateauMaxY, List<RoverRequest> rovers)
        {
            PlateauMaxX = plateauMaxX;
            PlateauMaxY = plateauMaxY;
            Rovers = rovers;
        }
    }
}
