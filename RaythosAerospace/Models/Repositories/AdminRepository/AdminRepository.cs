using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RaythosAerospace.Models.Repositories.AdminRepository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDBContext _context;

        public AdminRepository(AppDBContext context)
        {
            _context = context;
        }
        public string DeleteAdmin(string nic)
        {
            AdminModel foundAdmin = _context.Admins.FirstOrDefault(n => n.NIC ==nic);

            _context.Admins.Remove(foundAdmin);
            _context.SaveChanges();

            return foundAdmin.FullName;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void RegisterAdmin(AdminModel model,string password)
        {
            string hashedPassword = HashPassword(password);
            model.Password = hashedPassword;
            _context.Admins.Add(model);
            _context.SaveChanges();
        }

        public void RegisterAdmin(AdminModel model)
        {
            _context.Admins.Add(model);
            _context.SaveChanges();
        }

        public void UpdateAdmin(AdminModel model)
        {
            EntityEntry updated = _context.Admins.Attach(model);
            updated.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }

        public bool UpdatePassword(string email,string password, string oldpassword)
        {
            if (ValidateLogin(email, oldpassword))
            {
                AdminModel admin = _context.Admins.FirstOrDefault(a => a.Email == email);

                string newHashedPassword = HashPassword(password);

                admin.Password = newHashedPassword;

                UpdateAdmin(admin);

                return true;
            }

            return false;
        }

        public bool ValidateLogin(string email, string password)
        {
            AdminModel admin = _context.Admins.FirstOrDefault(a => a.Email == email);

            if (admin != null)
            {
                // Compare hashed passwords
                return VerifyPassword(password, admin.Password);
            }

            return false;
        }

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }
    }
}
