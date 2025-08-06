using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace core8_vue_mysql.Models.dto
{
  public class UserPasswordUpdate
    {        
        public string Password { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    
}