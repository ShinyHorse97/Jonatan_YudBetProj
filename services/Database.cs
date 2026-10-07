using Jonatan_YudBetProj.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jonatan_YudBetProj.services
{
    internal class Database
    {
        public static List<Users> UsersList = new List<Users>
        {
            new Users { Id = 1, Name = "Jonatan Shlain", Email = "jonatan.shlain@example.com", ImageSRC = "default_img.jpg" },
            new Users { Id = 2, Name = "Max Verstappen", Email = "max.verstappen@example.com", ImageSRC = "ver_img.jpg" },
            new Users { Id = 3, Name = "Lewis Hamilton", Email = "lewis.hamilton@example.com", ImageSRC = "ham_img.jpg" },
            new Users { Id = 4, Name = "Charles Leclerc", Email = "charles.leclerc@example.com", ImageSRC = "lec_img.jpg" },
            new Users { Id = 5, Name = "Lando Norris", Email = "lando.norris@example.com", ImageSRC = "nor_img.jpg" }
        };
    }
}
