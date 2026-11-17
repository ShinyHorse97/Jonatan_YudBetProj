using System;
using System.Collections.Generic;
using System.Text;

namespace Jonatan_YudBetProj.Models
{
    internal class Racecar
    {
        public string? Nickname { get; set; }
        public string? Id { get; set; }
        public string? Team { get; set; }
        public User? Driver { get; set; }
        public string? CarImage { get; set; } // Path to the car image
        public string? CarModel { get; set; }
        public string? Marque { get; set; }
        public int Year { get; set; }
        public int Horsepower { get; set; }
        public int Torque { get; set; } // in Nm
        public double Weight { get; set; } // in kg

    }
}
