using Jonatan_YudBetProj.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jonatan_YudBetProj.services
{
    internal class Database
    {
        public static List<User> UsersList = new List<User>
        {
            new User { Id = 1, Name = "Jonatan Shlain", Email = "jonatan.shlain@example.com", ImageSRC = "default_img.jpg" },
            new User { Id = 2, Name = "Max Verstappen", Email = "max.verstappen@example.com", ImageSRC = "ver_img.jpg" },
            new User { Id = 3, Name = "Lewis Hamilton", Email = "lewis.hamilton@example.com", ImageSRC = "ham_img.jpg" },
            new User { Id = 4, Name = "Charles Leclerc", Email = "charles.leclerc@example.com", ImageSRC = "lec_img.jpg" },
            new User { Id = 5, Name = "Lando Norris", Email = "lando.norris@example.com", ImageSRC = "nor_img.jpg" },
            new User { Id = 97, Name = "admin", Email = "admin", Password = "admin", ImageSRC = "default_img.jpg" }
        };
        public static bool UserExists(string email, string password)
        {
            return UsersList.Exists(u => u.Email == email && u.Password == password);
        }

        public static List<RaceTrack> RaceTracksList = new List<RaceTrack>
        {
            new RaceTrack { Id = "1", Name = "Monaco Grand Prix", Location = "Monaco", Length = 3337, Turns = 19, TrackImage = "monacogp.jpg" },
            new RaceTrack { Id = "2", Name = "Silverstone Grand Prix", Location = "United Kingdom", Length = 5891, Turns = 18, TrackImage = "silverstonegp.png" },
            new RaceTrack { Id = "3", Name = "Suzuka Grand Prix", Location = "Japan", Length = 5807, Turns = 18, TrackImage = "suzukagp.png" }
        };
    }
}
