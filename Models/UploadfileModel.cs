using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core8_vue_mysql.Models
{
    public class UploadfileModel {
        public int Id { get; set; }
        public IFormFile Profilepic { get; set; }

    }
    
}