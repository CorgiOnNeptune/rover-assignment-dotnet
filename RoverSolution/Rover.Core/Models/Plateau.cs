using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Core.Models
{
    public class Plateau
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public Plateau (int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
        }
    }
}
