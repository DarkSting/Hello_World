using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void Insert(UserModel user);

        UserModel Find(string id);
        int GetCount();
    }
}
