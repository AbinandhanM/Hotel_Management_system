using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class UserRepo : IUser
    {
        private readonly UserContext _context;
        public UserRepo(UserContext context)
        {
            _context = context;
        }
        public User Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;         
     
        }

        public User Delete(UserDTO userDTO)
        {
            throw new NotImplementedException();
        }

        public User Get(UserDTO userDTO)
        {
            var users = GetAll();
            var user = users.FirstOrDefault(u=>u.Username== userDTO.Username);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public List<User> GetAll()
        {
            var users= _context.Users.ToList();
            if(users.Count>0)
                return users;
            return null;
        }

        public User Update(User user)
        {
            var users = GetAll();
            var Newuser = users.FirstOrDefault(u => u.Username == user.Username);
            if (Newuser != null)
            {
                Newuser.Name = user.Name;
                Newuser.Password = user.Password;
                Newuser.PhoneNumber = user.PhoneNumber;
                Newuser.Age = user.Age;
                _context.Users.Update(Newuser);
                _context.SaveChanges();
                return Newuser;
            }
            else
                return null;
        }
    }
}
