using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BankingApp.AccountAPI
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string key; 

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }

        public string Authenticate(string userName, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
    }
}
