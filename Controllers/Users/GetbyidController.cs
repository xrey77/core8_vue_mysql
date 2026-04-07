using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using core8_vue_mysql.Services;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Helpers;
using Microsoft.Extensions.Caching.Memory;

namespace core8_vue_mysql.Controllers.Users {
    [ApiExplorerSettings(GroupName = "Retrieve User ID")]
    [Authorize]    
    [ApiController]
    [Route("api/[controller]/{id}")]
    public class GetbyidController : ControllerBase {

        private readonly IMemoryCache _cache;
        private IUserService _userService;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;  
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<GetbyidController> _logger;

        public GetbyidController(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IUserService userService,
            IMapper mapper,
            IMemoryCache cache,
            ILogger<GetbyidController> logger)
        {
            _cache = cache;
            _configuration = configuration;  
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpGet]
        public  async Task<IActionResult> getByuserid(int id) {


            try {
                var user = await _userService.GetById(id);
                var model = _mapper.Map<UserModel>(user);
                return Ok(new {message = "User found, retrieving record, please wait.",user = model});
            } catch(AppException ex) {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}