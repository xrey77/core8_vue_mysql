using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Models;
using core8_vue_mysql.Models.dto;

namespace core8_vue_mysql.Services
{    
    public interface IAuthService {
        Task<User> SignupUser(User userdata, string passwd);
        Task<User> SigninUser(string usrname, string pwd);
        Task<Role> getRolename(int id);
    }

    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

         IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

        public AuthService(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public async Task<User> SignupUser(User userdata, string passwd)
        {
            var xusermail = await _context.Users.FirstOrDefaultAsync(c => c.Email == userdata.Email);            
            if (xusermail is not null) {
                throw new AppException("Email Address is already taken...");
            }

            var xusername = await _context.Users.FirstOrDefaultAsync(c => c.UserName == userdata.UserName);            
            if (xusername is not null) {
                throw new AppException("Username is already taken...");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var xkey = config["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(xkey);

            // CREATE SECRET KEY FOR USER TOKEN===============
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userdata.Email)
                }),
                // Expires = DateTime.UtcNow.AddDays(7),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var secret = tokenHandler.CreateToken(tokenDescriptor);
            var secretkey = tokenHandler.WriteToken(secret);

            userdata.Secretkey = secretkey.ToUpper();             
            userdata.Password = BCrypt.Net.BCrypt.HashPassword(passwd);
            userdata.Profilepic = "pix.png";
            userdata.RolesId = 2;
            _context.Users.Add(userdata);                
            await _context.SaveChangesAsync();
            return userdata;
        }

        public async Task<Role> getRolename(int id) {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);            
        }

        public async Task<User> SigninUser(string usrname, string pwd)
        {
           try {
                    var xuser = await _context.Users.FirstOrDefaultAsync(c => c.UserName == usrname);                    
                    if (xuser is not null) {
                        if (!BCrypt.Net.BCrypt.Verify(pwd, xuser.Password)) {
                            throw new AppException("Incorrect Password...");
                        }
                        if (xuser.Isactivated == 0) {
                            throw new AppException("Please activate your account, check your email client inbox and click or tap the Activate button.");
                        }
                        return xuser;
                    } else {
                        throw new AppException("Username not found, please register first...");
                    }
            } catch(AppException ex) {
                    throw new AppException(ex.Message);
            }            
        }
    }
}