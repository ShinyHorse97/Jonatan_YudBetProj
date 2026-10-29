using System;
using System.Collections.Generic;
using System.Text;

namespace Jonatan_YudBetProj.Models
{
    internal class RaceTrack
    {
        public string? Name { get; set; }
        public string? Id { get; set; }
        public string? Location { get; set; }
        public int Length { get; set; } // in meters
        public int Turns { get; set; }
        public string? TrackImage { get; set; } // Path to the track image
    }
}
