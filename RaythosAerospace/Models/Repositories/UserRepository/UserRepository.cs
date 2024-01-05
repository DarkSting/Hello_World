using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public string DeleteUser(string nic)
        {
            UserModel foundUser = _context.Users.FirstOrDefault(n => n.UserId == nic);

            _context.Users.Remove(foundUser);


            _context.SaveChanges();

            return foundUser.Name;
        }

       
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        public void RegisterUser(UserModel model, string password)
        {
            string hashedPassword = HashPassword(password);
            model.Password = hashedPassword;
            _context.Users.Add(model);
            _context.SaveChanges();
        }

        public void UpdateUser(UserModel model)
        {
            UserModel foundUser = _context.Users.Find(model.UserId);
            foundUser.UserId = model.UserId;
            foundUser.Name = model.Name;
            foundUser.Address = model.Address;
            foundUser.DOB = model.DOB;
            foundUser.Email = model.Email;
            EntityEntry updated = _context.Users.Attach(foundUser);
            updated.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
        }

        public bool UpdatePassword(string email, string password, string oldpassword)
        {
            if (ValidateLogin(email, oldpassword))
            {
                UserModel foundUser = _context.Users.FirstOrDefault(a => a.Email == email);

                string newHashedPassword = HashPassword(password);

                foundUser.Password = newHashedPassword;

                UpdateUser(foundUser);

                return true;
            }

            return false;
        }

        public bool ValidateLogin(string email, string password)
        {
           

            try
            {
                UserModel admin = _context.Users.FirstOrDefault(a => a.Email == email);

                if (admin != null)
                {
                    // Compare hashed passwords
                    return VerifyPassword(password, admin.Password);
                }
            }
            catch(Exception e)
            {
                return false;
            }
           

            return false;
        }

        public bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
        }

        public UserModel Find(string nic)
        {

            UserModel foundModel = null;

            try
            {
                foundModel = _context.Users.FirstOrDefault(a => a.UserId == nic);

            }
            catch (Exception e)
            {
                foundModel = null;
            }

            return foundModel;
         
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IList<UserModel> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}
