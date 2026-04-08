using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using core8_vue_mysql.Services;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Helpers;

namespace core8_vue_mysql.Controllers.Products
{
    [ApiExplorerSettings(GroupName = "List All Products")]    
    [ApiController]
    [AllowAnonymous] 
    [Route("[controller]")]
    public class ListController : ControllerBase {
        private IProductService _productService;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;  
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ListController> _logger;

        public ListController(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<ListController> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpGet("/api/listproducts/{page}")]
        public async Task<IActionResult> ListProducts(int page) {
            try {                
                int totalpage = await _productService.TotPageAsync();
                var products = await _productService.ListAllAsync(page);
                var model = _mapper.Map<IList<ProductModel>>(products);
                return Ok(new {totpage = totalpage, page = page, products=model});
            } catch(AppException ex) {
               return BadRequest(new {message = ex.Message});
            }
        }
    }    
}