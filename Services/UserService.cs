using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Models.dto;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace core8_vue_mysql.Services
{
    public interface IUserService {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task UpdateProfile(User user);
        void Delete(int id);
        Task ActivateMfa(int id, bool opt, string qrcode_url);
        void UpdatePicture(int id, string file);
        Task UpdatePassword(User user, string password = null);
        int EmailToken(int etoken);
        int SendEmailToken(string email);
        void ActivateUser(int id);
        Task ForgotPassword(User userParam);
        Task<bool> GetMailToken(int mailtoken);
    }

    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

         IConfiguration config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

        public UserService(
            ApplicationDbContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public void Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else {
               throw new AppException("User not found");
            }   
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
                if (user == null) {
                    throw new AppException("User does'not exists....");
                }
                return user;
        }


        public async Task UpdateProfile(User userParam)
        {
            // Use FindAsync to retrieve the user asynchronously
            var user = await _context.Users.FindAsync(userParam.Id);
            
            if (user is null) {
                throw new AppException("User not found");
            }
            
            if (!string.IsNullOrWhiteSpace(userParam.FirstName)) {
                user.FirstName = userParam.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.LastName)) {
                user.LastName = userParam.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userParam.Mobile)) {
                user.Mobile = userParam.Mobile;
            }

            user.UpdatedAt = DateTime.Now;

            _context.Users.Update(user);

            await _context.SaveChangesAsync();            
        }

        public async Task UpdatePassword(User userParam, string password = null)
        {
            var user = await _context.Users.FindAsync(userParam.Id);
            if (user is null)
                throw new AppException("User not found");

            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                 user.Password = BCrypt.Net.BCrypt.HashPassword(userParam.Password);

            }
            user.UpdatedAt = DateTime.Now;            
            _context.Users.Update(user);
            await _context.SaveChangesAsync();        
        }


        public async Task ActivateMfa(int id, bool opt, string qrcode_url)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                if (opt == true ) {

                    user.Qrcodeurl = qrcode_url;
                } else {
                    user.Qrcodeurl = null;
                }
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            else {
               throw new AppException("User not found");
            }                    }

        public void UpdatePicture(int id, string file)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Profilepic = file;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
            else {
               throw new AppException("User not found");
            }                    
        }

       public void ActivateUser(int id) 
       {
            var user = _context.Users.Find(id);
            if (user.Isblocked == 1) {
                throw new AppException("Account has been blocked.");
            }
            if ( user.Isactivated == 1) {
                throw new AppException("Account is alread activated.");
            }
            user.Isactivated = 1;
            if (user == null)
            {
                throw new AppException("User not found");
            }
            _context.Users.Update(user);
            _context.SaveChanges();            
       }

        public int SendEmailToken(string email)
        {
           var user =  _context.Users.AsQueryable().FirstOrDefault(c => c.Email == email);
           if (user == null) {
                throw new AppException("Email Address not found...");
           }
            var etoken = EmailToken(user.Mailtoken);
            user.Mailtoken = etoken;
            _context.Users.Update(user);
            _context.SaveChanges();
            return etoken;
        }       

        public int EmailToken(int etoken)
        {
            int _min = etoken;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task ForgotPassword(User userParam)
        {
            if (userParam.Password is null){
                throw new AppException("Please enter your new password.");
            }
            var user = await _context.Users.FirstOrDefaultAsync(c => c.Mailtoken == userParam.Mailtoken);
            if (user is not null) {
                user.Password = BCrypt.Net.BCrypt.HashPassword(userParam.Password);            
                user.Mailtoken = 0;
                await _context.SaveChangesAsync();
            } else {
                throw new AppException("Mailtoken not found...");
            }           
        }

        public async Task<bool> GetMailToken(int mailtoken)
        {
            return await _context.Users.AnyAsync(c => c.Mailtoken == mailtoken);            
        }


    }
}