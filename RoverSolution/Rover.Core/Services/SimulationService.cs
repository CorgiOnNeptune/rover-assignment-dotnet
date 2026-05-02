using Rover.Core.Commands;
using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Services
{
    public class SimulationService
    {
        public Simulation RunSimulation(SimulationRequest request)
        {
            Plateau plateau = new Plateau(request.PlateauMaxX, request.PlateauMaxY);

            List<string> finalPositions = new List<string>();
            List<List<Position>> positionHistory = new List<List<Position>>();

            foreach (RoverRequest rover in request.Rovers)
            {
                MarsRover newRover = new MarsRover(rover.StartingPosition);

                IEnumerable<IRoverCommand> commands = CommandInvoker.CreateCommands(rover.Commands);
                CommandInvoker.ExecuteAll(commands, newRover, plateau);

                int finalX = newRover.Position.X;
                int finalY = newRover.Position.Y;
                Direction finalDirection = newRover.Position.Direction;

                finalPositions.Add($"{finalX} {finalY} {finalDirection}");
                positionHistory.Add(newRover.PositionHistory);
            }

            return new Simulation(request, finalPositions, positionHistory);
        }
    }
}
