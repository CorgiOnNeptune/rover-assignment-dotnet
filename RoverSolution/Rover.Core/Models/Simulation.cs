using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class Simulation
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime RequestTime { get; init; } = DateTime.Now;

        public SimulationRequest Instructions { get; set; }
        public List<string> FinalPositions { get; set; }
        public string FinalPositionsRaw { get; set; }
        public List<List<Position>> PositionHistory { get; set; }

        public Simulation(SimulationRequest instructions, List<string> finalPositions, List<List<Position>> positionHistory)
        {
            Instructions = instructions;
            FinalPositions = finalPositions;
            PositionHistory = positionHistory;

            FinalPositionsRaw = string.Join(" ", finalPositions);
        }
    }
}
