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
    [ApiExplorerSettings(GroupName = "Add Product")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AddProduct : ControllerBase {
        private IProductService _productService;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;  
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<AddProduct> _logger;

        public AddProduct(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<AddProduct> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpPost("/api/addproduct")]
        public IActionResult AddProducts(ProductModel model) {
            try {                
                DateTime now = DateTime.Now;
                var prods = _mapper.Map<Product>(model);
                prods.Category = model.Category;
                prods.Descriptions = model.Descriptions;
                prods.Qty = model.Qty;
                prods.Unit = model.Unit;
                prods.CostPrice = model.CostPrice;
                prods.SellPrice = model.SellPrice;
                prods.SalePrice = model.SalePrice;
                prods.AlertStocks = model.AlertStocks;
                prods.CriticalStocks = model.CriticalStocks;
                prods.CreatedAt = now;
                _productService.CreateProduct(prods);
                return Ok(new {statuscode = 200, message = "New product has been added to the database."});
            } catch(AppException ex) {
               return BadRequest(new {statuscode = 400, Message = ex.Message});
            }
        }
    }    
}