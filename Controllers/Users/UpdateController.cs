using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Services;
using Microsoft.Extensions.Caching.Memory;

namespace core8_vue_mysql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Update User")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UpdateController : ControllerBase {
        
    private IUserService _userService;
    private readonly IMemoryCache _cache;
    private IMapper _mapper;
    private readonly IConfiguration _configuration;  

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<UpdateController> _logger;

    public UpdateController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<UpdateController> logger
        )
    {
        _cache = cache;
        _configuration = configuration;  
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;        
    }  

        [HttpPatch("/api/updateprofile/{id}")]        
        public async Task<IActionResult> updateUser(int id, [FromBody]UserUpdate model) {
            var user = _mapper.Map<User>(model);
            user.Id = id;
            user.FirstName = model.Firstname;
            user.LastName = model.Lastname;
            user.Mobile = model.Mobile;

            try
            {
                // 1. Perform the update in the database
                await _userService.UpdateProfile(user);

                // 2. Invalidate the cache for this specific user
                // Ensure this key matches the one used in your "Get" method (e.g., "user_1")
                string cacheKey = $"user_{id}";
                _cache.Remove(cacheKey); 

                return Ok(new { message = "Your profile has been updated successfully." });
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // [HttpPatch("/api/updateprofile/{id}")]        
        // public async Task<IActionResult> updateUser(int id, [FromBody]UserUpdate model) {
        //     var user = _mapper.Map<User>(model);
        //     user.Id = id;
        //     user.FirstName = model.Firstname;
        //     user.LastName = model.Lastname;
        //     user.Mobile = model.Mobile;
        //     try
        //     {
        //         await _userService.UpdateProfile(user);
        //         return Ok(new {message="Your profile has been updated successfully.",user = model});
        //     }
        //     catch (AppException ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }
        // }

    }
}