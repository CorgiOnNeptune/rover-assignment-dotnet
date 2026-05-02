using Rover.Core.Commands;
using Rover.Core.DTOs;
using Rover.Core.Enums;
using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Services
{
    public static class RoverSimulationService
    {
        public static SimulationResponseDTO RunRoverSimulation(SimulationRequestDTO request)
        {
            Plateau plateau = new Plateau(request.PlateauMaxX, request.PlateauMaxY);

            List<string> finalPositions = new List<string>();
            List<List<Position>> positionHistory = new List<List<Position>>();

            foreach (RoverRequestDTO rover in request.Rovers)
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

            return new SimulationResponseDTO(finalPositions, positionHistory);
        }
    }
}
