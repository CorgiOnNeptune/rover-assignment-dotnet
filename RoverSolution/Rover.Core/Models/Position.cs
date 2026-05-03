using Rover.Core.Enums;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }

        [JsonConstructor]
        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public Position(Position position)
        {
            X = position.X;
            Y = position.Y;
            Direction = position.Direction;
        }

        /// <summary>
        /// Check that the given position isn't negative on either axis and within the range of the given plateau.
        /// </summary>
        public bool IsValid(Plateau plateau)
        {
            return
                X >= 0 &&
                Y >= 0 &&
                Y <= plateau.MaxY &&
                X <= plateau.MaxX;
        }
    }
}
