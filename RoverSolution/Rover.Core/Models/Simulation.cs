using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class Simulation
    {
        public int Id { get; set; } = 1; // Id gets auto-incremented by JsonFlatFileDataStore package
        public DateTime RequestTime { get; init; } = DateTime.Now;

        public SimulationRequest Instructions { get; set; }
        public List<string> FinalPositions { get; set; } = [];
        public string FinalPositionsRaw { get; set; } = string.Empty;
        public List<List<Position>> PositionHistory { get; set; } = [];

        public Simulation(SimulationRequest instructions, List<string> finalPositions, List<List<Position>> positionHistory)
        {
            Instructions = instructions;
            FinalPositions = finalPositions;
            PositionHistory = positionHistory;

            FinalPositionsRaw = string.Join(" ", finalPositions);
        }
    }
}
