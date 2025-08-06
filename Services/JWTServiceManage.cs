using AutoMapper.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Entities;

namespace core8_vue_mysql.Services
{
    public interface IJWTTokenServices
    {
        JWTTokens Authenticate(User users);
    }
    public class JWTServiceManage : IJWTTokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbcontext;
 
        public JWTServiceManage(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbcontext = dbContext;
        }
        public JWTTokens Authenticate(User users)
        {
             
            if (!_dbcontext.Users.Any(e => e.UserName == users.UserName && e.Password == users.Password))
            {
                return null;            
            }
 
            var tokenhandler = new JwtSecurityTokenHandler();
            var tkey = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var ToeknDescp = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tkey), SecurityAlgorithms.HmacSha256Signature)
            };
            var toekn = tokenhandler.CreateToken(ToeknDescp);
 
            return new JWTTokens { Token = tokenhandler.WriteToken(toekn) };
 
        }
    }    
    
}