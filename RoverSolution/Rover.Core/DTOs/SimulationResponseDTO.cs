using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.DTOs
{
    public class SimulationResponseDTO
    {
        public List<string> FinalPositions { get; set; }
        public List<List<Position>> PositionHistory { get; set; }

        public SimulationResponseDTO(List<string> finalPositions, List<List<Position>> positionHistory)
        {
            FinalPositions = finalPositions;
            PositionHistory = positionHistory;
        }
    }
}
