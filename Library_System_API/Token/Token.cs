using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_System_API.Token
{
    public class Token:IToken
    {
        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string name, string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issure"],
                    audience: _configuration["Jwt:Audience"],
                    claims: new[] { new Claim(ClaimTypes.Name, name), new Claim(ClaimTypes.Email, email) },
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireInMinutes"])),
                    signingCredentials: cred



                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
