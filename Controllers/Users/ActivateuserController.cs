using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using core8_vue_mysql.Services;
using core8_vue_mysql.Helpers;
using Microsoft.Extensions.Caching.Memory;
using core8_vue_mysql.Models.dto;

namespace core8_vue_mysql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Forgot User Password")]
    [ApiController]
    [AllowAnonymous] 
    [Route("[controller]")]
    public class ActivateUserController : ControllerBase {
    private IUserService _userService;
    private readonly IConfiguration _configuration;  
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ActivateUserController> _logger;
    private readonly IMemoryCache _cache;

    public ActivateUserController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMemoryCache cache,
        ILogger<ActivateUserController> logger
        )
    {
        _cache = cache;
        _configuration = configuration;  
        _userService = userService;
        _logger = logger;
        _env = env;        
    }  

        [HttpPatch("/api/activateuser/{id}")]
        public async Task<IActionResult> ActivateUser(int id, [FromBody]ActivationRequest model) 
        {
            try
            {
                string cacheKey = $"user_{id}";
                var user = await _cache.GetOrCreateAsync(cacheKey, entry => 
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                    return _userService.GetById(id);
                });

                if (user is null) {
                    return NotFound(new { message = "User not found" });
                } 
                await _userService.ActivateUser(id, model.Activation);

                _cache.Remove(cacheKey);
                return Ok(new { message = "Your Account is activated successfully."});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Activation failed");
                return StatusCode(500, ex.Message);                 
                // return BadRequest(new { message = ex.Message });
            }
        }
    }    
}