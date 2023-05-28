using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUser _userRepo;
        private readonly ITokenGenerate _tokenService;

        public UserService(IUser userRepo,ITokenGenerate tokenService)
        {
            _userRepo = userRepo;
            _tokenService= tokenService;
        }
        public UserDTO Login(UserDTO userDTO)
        {
            UserDTO user = null;
            var userData = _userRepo.Get(userDTO);
            if (userData != null)
            {
                var hmac = new HMACSHA512(userData.HashKey);
                var userPass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userPass.Length; i++)
                {
                    if (userPass[i] != userData.Password[i])
                        return null;
                }
                user = new UserDTO();
                user.Username = userData.Username;
                user.Role = userData.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public UserDTO Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = null;
            var hmac = new HMACSHA512();
            userRegisterDTO.Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDTO.UserPassword));
            userRegisterDTO.HashKey = hmac.Key;
            var resultUser = _userRepo.Add(userRegisterDTO);
            if (resultUser != null)
            {
                user = new UserDTO();
                user.Username = resultUser.Username;
                user.Role = resultUser.Role;
                user.Token = _tokenService.GenerateToken(user);
            }
            return user;
        }

        public User Update(User user)
        {
            var myUser = _userRepo.Update(user);
            if (myUser != null)
                return myUser;
            return null;
        }
    }
}
