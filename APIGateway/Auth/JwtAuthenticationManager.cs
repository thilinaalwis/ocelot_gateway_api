using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGateway.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly string tokenKey;

        public JwtAuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }
        public string Authenticate(string _key)
        {
            if (_key != "APIGATEWAYKEY")
            {
                return null;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, "Ocelot API Gateway Token")
                //}),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
