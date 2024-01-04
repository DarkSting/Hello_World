using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public class UserLoginDTO
    {
        public UserModel Credentials { get; set; }
        public string ConfirmedPass { get; set; }

    }
}
