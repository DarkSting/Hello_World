using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaythosAerospace.Models.Repositories.AdminRepository
{
    public interface IAdminRepository
    {
        void RegisterAdmin(AdminModel model);

        void RegisterAdmin(AdminModel model,string password);

        void UpdateAdmin(AdminModel model);

        bool UpdatePassword(string email,string password, string oldpassword);

        string DeleteAdmin(string nic);

        bool ValidateLogin(string username, string password);
        string HashPassword(string password);

        bool VerifyPassword(string inputPassword, string hashedPassword);

        AdminModel GetAdmin(string email);
    }
}
