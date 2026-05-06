using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Commands
{
    public interface IRoverCommand
    {
        public abstract void Execute(MarsRover rover, Plateau plateau);
    }
}
