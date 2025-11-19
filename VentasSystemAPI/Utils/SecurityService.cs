using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace VentasSystemAPI.Utils
{
    public class SecurityService(IConfiguration config) : ISecurityService
    {
        private readonly IConfiguration _config = config;

        public string GeneratePass()
        {
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6);
            return clave;
        }

        public string HashPassword(string clave)
        {
            StringBuilder sb = new();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(clave));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

            }

            return sb.ToString();
        }

        public string CreateToken(int id)
        {
            Claim[] claims = [
                new("userId", id.ToString())
                ];

            string? tokenKeyString = _config.GetSection("AppSettings:TokenKey").Value;

            SymmetricSecurityKey tokenKey = new(
                Encoding.UTF8.GetBytes(
                    tokenKeyString ?? ""
                )
            );

            SigningCredentials credentials = new(tokenKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(90)
            };

            JwtSecurityTokenHandler tokenHandler = new();

            SecurityToken token = tokenHandler.CreateToken(descriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
