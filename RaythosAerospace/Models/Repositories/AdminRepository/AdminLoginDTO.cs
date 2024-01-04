using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AdminRepository
{
    public class AdminLoginDTO
    {
        public AdminModel credintials { get; set; }

        public string confirmedPass { get; set; }
    }
}
