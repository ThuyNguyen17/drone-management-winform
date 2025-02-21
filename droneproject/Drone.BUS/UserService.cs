using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Drone.DAL;
using Drone.DAL.Entities;

namespace Drone.BUS
{
    public class UserService
    {
        DroneContextDB context = new DroneContextDB();
        public List<User> GetAll()
        {

            return context.Users.ToList();
        }
        public bool IsLoginValid(string username, string password)
        {
           // string hashedPassword = HashPassword(password);

            var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            return user != null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}