using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.UserRepository
{
    public interface IUserRepository
    {
        void Insert(UserModel user);

        UserModel Find(string nic);

        IList<UserModel> GetUsers();

        UserModel GetUserByEmail(string email);

        void RegisterUser(UserModel model,string password);

        void UpdateUser(UserModel model);

        bool UpdatePassword(string email, string password, string oldpassword);

        string DeleteUser(string nic);

        bool ValidateLogin(string email, string password);
        string HashPassword(string password);

        bool VerifyPassword(string inputPassword, string hashedPassword);

        int GetCount();
    }
}
