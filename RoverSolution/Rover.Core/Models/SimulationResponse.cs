using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class SimulationResponse
    {
        public List<string> FinalPositions { get; set; }
        public List<List<Position>> PositionHistory { get; set; }

        public SimulationResponse(List<string> finalPositions, List<List<Position>> positionHistory)
        {
            FinalPositions = finalPositions;
            PositionHistory = positionHistory;
        }
    }
}
