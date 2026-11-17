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
        public static bool UserExists(string username)
        {
            return UsersList.Exists(u => u.Name == username);
        }
        public static bool ValidateUser(string email, string password)
        {
            return UsersList.Exists(u => u.Email == email && u.Password == password);
        }
        public static bool EmailExists(string email)
        {
            return UsersList.Exists(u => u.Email == email);
        }
        public static void AddUser(User user)
        {
            UsersList.Add(user);
        }

        public static List<RaceTrack> RaceTracksList = new List<RaceTrack>
        {
            new RaceTrack { Id = "1", Name = "Monaco Grand Prix", Location = "Monaco", Length = 3337, Turns = 19, TrackImage = "monacogp.jpg" },
            new RaceTrack { Id = "2", Name = "Silverstone Grand Prix", Location = "United Kingdom", Length = 5891, Turns = 18, TrackImage = "silverstonegp.png" },
            new RaceTrack { Id = "3", Name = "Suzuka Grand Prix", Location = "Japan", Length = 5807, Turns = 18, TrackImage = "suzukagp.png" }
        };
        public static List<Racecar> RacecarsList = new List<Racecar>
        {
            new Racecar { Id = "1", Nickname = "Rocky", Team = "Red Bull Racing", Driver = UsersList[1], CarImage = "https://cdn-2.motorsport.com/images/amp/6xEXD9r0/s6/red-bull-racing-rb19-front-win.jpg", CarModel = "RB19", Marque = "Red Bull", Year = 2023, Horsepower = 1000, Torque = 800, Weight = 798 },
            new Racecar { Id = "2", Nickname = "kart", Team = "Tony Kart Racing", Driver = UsersList[1], CarImage = "https://i0.wp.com/bbrkarting.com/wp-content/uploads/2025/12/2026-Tony-Kart-401T-BBR-Karting.jpg?fit=2000%2C1333&ssl=1", CarModel = "Racer 401 TS", Marque = "Tony Kart", Year = 2027, Horsepower = 30, Torque = 20, Weight = 75 },
            new Racecar { Id = "3", Nickname = "GT3", Team = "Verstappen Racing", Driver = UsersList[1], CarImage = "https://cdn-6.motorsport.com/images/amp/0mXRjmA6/s1000/3-mercedes-amg-team-verstappen.webp", CarModel = "GT3 Evo", Marque = "Mercedes-AMG", Year = 2020, Horsepower = 550, Torque = 650, Weight = 1285 }
        };
    }
}
