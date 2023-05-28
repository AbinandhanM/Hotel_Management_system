using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
      
        [HttpPost("SignUp")]
        public ActionResult<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = _userService.Register(userRegisterDTO);
            if (user == null)
                return BadRequest(new Error(2, "Register Not Successfull"));
            return Created("User Registered", user);
        }
  
        [HttpPost("Login")]
        public ActionResult<UserDTO> Login(UserDTO userDTO)
        {
            UserDTO user = _userService.Login(userDTO);
            if (user == null)
                return BadRequest(new Error(1, "Invalid UserName or Password"));
            return Ok(user);
        }

        [HttpPost("Update")]
        public ActionResult<UserDTO> Update(User user)
        {
            var myUser=_userService.Update(user);
            if (myUser == null)
                return NotFound(new Error(3,"Unable to Update"));
            return Ok(new Error(4,"Updated Successfully"));
        }
    }
}
