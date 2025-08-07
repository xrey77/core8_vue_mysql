using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using core8_vue_mysql.Services;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Models;

namespace core8_vue_mysql.Controllers.Products
{
    [ApiExplorerSettings(GroupName = "Search Product Description")]
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase {

        private IProductService _productService;

        private IMapper _mapper;
        private readonly IConfiguration _configuration;  

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<SearchController> _logger;

        public SearchController(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<SearchController> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpGet("/api/searchproducts/{page}/{key}")]
        public IActionResult SearchProducts(int page, string key) {
            try {                
                int totalpage = _productService.TotPageSearch(page, key);
                var products = _productService.SearchAll(page, key);
                if (products != null) {
                    var model = _mapper.Map<IList<ProductModel>>(products);
                    return Ok(new {totpage = totalpage, page = page, products=model});
                } else {
                    return BadRequest(new {statuscode=404, message="No Data found."});
                }
            } catch(AppException ex) {
               return BadRequest(new {statuscode = 404, Message = ex.Message});
            }
        }
    }    
}