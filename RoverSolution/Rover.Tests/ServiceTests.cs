using Rover.Core.Enums;
using Rover.Core.Models;
using Rover.Core.Services;
using System.Text.Json;

namespace Rover.Tests;

public class ServiceTests
{

    private string rawInputExample = @"5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM";

    private string largeInput = @"100 100
10 20 N
LMLMLMLMMMRMLMRMRMRMRMLMLMLMLMMMM
30 30 E
MMRMMRMRRMLMRMMMMMMRRRRLLLLMMMMMMMLMMMMRMMMM";

    [Fact]
    public void RawRequestParserService_RawInput_ShouldEqualExpected()
    {
        RoverRequest expectedRoverOne = new(new Position(1, 2, Direction.N), [.. "LMLMLMLMM"]);
        RoverRequest expectedRoverTwo = new(new Position(3, 3, Direction.E), [.. "MMRMMRMRRM"]);
        List<RoverRequest> expectedRovers = [expectedRoverOne, expectedRoverTwo];

        SimulationRequest expectedObject = new(5, 5, expectedRovers);
        SimulationRequest actualObject = RawRequestParserService.Parse(rawInputExample);

        string expectedResult = JsonSerializer.Serialize(expectedObject);
        string actualResult = JsonSerializer.Serialize(actualObject);

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void RawRequestParserService_LargeInput_ShouldEqualExpected()
    {
        RoverRequest expectedRoverOne = new(new Position(10, 20, Direction.N), [.. "LMLMLMLMM"]);
        RoverRequest expectedRoverTwo = new(new Position(30, 30, Direction.E), [.. "MMRMMRMRRM"]);
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
        SimulationRequest request = RawRequestParserService.Parse(rawInputExample);
        SimulationResponse response = RoverSimulationService.RunRoverSimulation(request);

        List<string> expectedPositions = new List<string> { "1 3 N", "5 1 E" };

        Assert.Equal(expectedPositions, response.FinalPositions);
    }

    [Fact]
    public void SimulationService_LargeInput_ShouldEqualExpectedFinalPositions()
    {
        SimulationRequest request = RawRequestParserService.Parse(largeInput);
        SimulationResponse response = RoverSimulationService.RunRoverSimulation(request);

        List<string> expectedPositions = new List<string> { "11 26 N", "49 33 E" };

        Assert.Equal(expectedPositions, response.FinalPositions);
    }

    [Fact]
    public void SimulationService_RawInput_RoverShouldNotExceedUpperBounds()
    {
        string outOfBoundsAttempt = @"5 5
5 5 N
MMMMMMMRMMMMM";

        SimulationRequest request = RawRequestParserService.Parse(outOfBoundsAttempt);
        SimulationResponse response = RoverSimulationService.RunRoverSimulation(request);

        List<string> expectedPositions = new List<string> { "5 5 E" };

        Assert.Equal(expectedPositions, response.FinalPositions);
    }

    [Fact]
    public void SimulationService_RawInput_RoverShouldNotExceedLowerBounds()
    {
        string outOfBoundsAttempt = @"5 5
0 0 W
MMMMMMMMMMLMMMMMM";

        SimulationRequest request = RawRequestParserService.Parse(outOfBoundsAttempt);
        SimulationResponse response = RoverSimulationService.RunRoverSimulation(request);

        List<string> expectedPositions = new List<string> { "0 0 S" };

        Assert.Equal(expectedPositions, response.FinalPositions);
    }
}
