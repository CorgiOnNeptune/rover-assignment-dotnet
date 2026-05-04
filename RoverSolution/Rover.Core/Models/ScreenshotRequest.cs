using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class ScreenshotRequest
    {
        public int SimulationId { get; set; }
        public string Screenshot { get; set; } = string.Empty;
    }
}
