using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDBContext _context;
        public UserRepository(AppDBContext context)
        {
            _context = context;
        }
        public void Insert(UserModel user)
        {
         
        }

        public int GetCount()
        {
            return 0;
        }

        public UserModel Find(string id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }
    }
}
