using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;

namespace UserAPI.Services
{
    public class TokenServices:ITokenGenerate
    {
        private readonly SymmetricSecurityKey _key;

        public TokenServices(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        }
        public string GenerateToken(UserDTO userDTO)
        {
            string token = string.Empty;         
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,userDTO.Username),
                new Claim(ClaimTypes.Role,userDTO.Role)
            };           
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);          
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = cred
            };          
            var tokenHandler = new JwtSecurityTokenHandler();
            var myToken = tokenHandler.CreateToken(tokenDescription);
            token = tokenHandler.WriteToken(myToken);
            return token;
        }
    }
}
