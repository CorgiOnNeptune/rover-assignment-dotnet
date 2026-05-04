using Rover.Core.Enums;
using Rover.Core.Models;
using Rover.Core.Services;
using System.Text.Json;

namespace Rover.Tests;

public class ServiceTests
{
    private readonly SimulationService simulationService = new SimulationService();

    RawSimulationRequest rawInput = new RawSimulationRequest { Input = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM" };

    RawSimulationRequest largeInput = new RawSimulationRequest { Input = @"100 100
10 20 N
LMLMLMLMMMRMLMRMRMRMRMLMLMLMLMMMM
30 30 E
MMRMMRMRRMLMRMMMMMMRRRRLLLLMMMMMMMLMMMMRMMMM" };

    [Fact]
    public void RawRequestParserService_RawInput_ShouldEqualExpected()
    {
        RoverRequest expectedRoverOne = new(new Position(1, 2, Direction.N), "LMLMLMLMM");
        RoverRequest expectedRoverTwo = new(new Position(3, 3, Direction.E), "MMRMMRMRRM");
        List<RoverRequest> expectedRovers = [expectedRoverOne, expectedRoverTwo];

        SimulationRequest expectedObject = new(5, 5, expectedRovers);
        SimulationRequest actualObject = RawRequestParserService.Parse(rawInput);

        string expectedResult = JsonSerializer.Serialize(expectedObject);
        string actualResult = JsonSerializer.Serialize(actualObject);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void RawRequestParserService_LargeInput_ShouldEqualExpected()
    {
        RoverRequest expectedRoverOne = new(new Position(10, 20, Direction.N), "LMLMLMLMMMRMLMRMRMRMRMLMLMLMLMMMM");
        RoverRequest expectedRoverTwo = new(new Position(30, 30, Direction.E), "MMRMMRMRRMLMRMMMMMMRRRRLLLLMMMMMMMLMMMMRMMMM");
        List<RoverRequest> expectedRovers = [expectedRoverOne, expectedRoverTwo];

        SimulationRequest expectedObject = new(100, 100, expectedRovers);
        SimulationRequest actualObject = RawRequestParserService.Parse(largeInput);

        string expectedResult = JsonSerializer.Serialize(expectedObject);
        string actualResult = JsonSerializer.Serialize(actualObject);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void SimulationService_RawInput_ShouldEqualExpectedFinalPositions()
    {
        SimulationRequest request = RawRequestParserService.Parse(rawInput);
        Simulation response = simulationService.RunSimulation(request);

        string expectedPositions = "1 3 N 5 1 E";
        Assert.Equal(expectedPositions, response.FinalPositionsRaw);
    }

    [Fact]
    public void SimulationService_LargeInput_ShouldEqualExpectedFinalPositions()
    {
        SimulationRequest request = RawRequestParserService.Parse(largeInput);
        Simulation response = simulationService.RunSimulation(request);

        string expectedPositions = "11 26 N 49 33 E";
        Assert.Equal(expectedPositions, response.FinalPositionsRaw);
    }

    [Fact]
    public void SimulationService_RawInput_RoverShouldNotExceedUpperBounds()
    {
        RawSimulationRequest outOfBounds = new RawSimulationRequest() { Input = @"5 5
5 5 N
MMMMMMMRMMMMM" };

        SimulationRequest request = RawRequestParserService.Parse(outOfBounds);
        Simulation response = simulationService.RunSimulation(request);

        string expectedPositions = "5 5 E";
        Assert.Equal(expectedPositions, response.FinalPositionsRaw);
    }

    [Fact]
    public void SimulationService_RawInput_RoverShouldNotExceedLowerBounds()
    {
        RawSimulationRequest outOfBounds = new RawSimulationRequest() { Input = @"5 5
0 0 W
MMMMMMMMMMLMMMMMM" };

        SimulationRequest request = RawRequestParserService.Parse(outOfBounds);
        Simulation response = simulationService.RunSimulation(request);

        string expectedPositions = "0 0 S";
        Assert.Equal(expectedPositions, response.FinalPositionsRaw);
    }
}
