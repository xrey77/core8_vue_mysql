using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using core8_vue_mysql.Services;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Helpers;

namespace core8_vue_mysql.Controllers.Products
{
    [ApiExplorerSettings(GroupName = "Delete Product")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DeleteProduct : ControllerBase {
        private IProductService _productService;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;  
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<DeleteProduct> _logger;

        public DeleteProduct(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<DeleteProduct> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpDelete("/api/deleteproduct/{id}")]
        public IActionResult PurgeProduct(int id) {
            try {                
                _productService.ProductDelete(id);
                return Ok(new {statuscode = 200, message = "Product has been deleted."});
            } catch(AppException ex) {
               return BadRequest(new {statuscode = 400, Message = ex.Message});
            }
        }
    }    
}