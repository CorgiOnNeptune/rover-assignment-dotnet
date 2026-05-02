using Rover.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Commands
{
    internal static class CommandInvoker
    {

        /// <summary>
        /// Create the commands to be executed based on the instructions
        /// </summary>
        public static IEnumerable<IRoverCommand> CreateCommands(List<char> instructions)
        {
            List<IRoverCommand> commands = new List<IRoverCommand>();

            foreach (char instruction in instructions)
            {
                switch (instruction)
                {
                    case 'R':
                        commands.Add(new RotateRightCommand());
                        break;

                    case 'L':
                        commands.Add(new RotateLeftCommand());
                        break;

                    case 'M':
                        commands.Add(new MoveCommand());
                        break;

                    default:
                        break;
                }
            }

            return commands;
        }

        /// <summary>
        /// Execute all the given IRoverCommands on the rover with the bounds of the current plateau
        /// </summary>
        public static void ExecuteAll(IEnumerable<IRoverCommand> commands, MarsRover rover, Plateau plateau)
        {
            foreach (IRoverCommand command in commands)
            {
                command.Execute(rover, plateau);
            }
        }
    }
}
