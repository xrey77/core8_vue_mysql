using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core8_vue_mysql.Models
{
    public class UploadProductpicModel {
        public int Id { get; set; }
        public IFormFile ProductPicture { get; set; }

    }
    
}