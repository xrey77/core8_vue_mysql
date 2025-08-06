using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using core8_vue_mysql.Services;
using core8_vue_mysql.Helpers;

namespace core8_vue_mysql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Delete User")]
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase {

    private IUserService _userService;

    private IMapper _mapper;
    private readonly IConfiguration _configuration;  

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<DeleteController> _logger;

    public DeleteController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMapper mapper,
        ILogger<DeleteController> logger
        )
    {
        _configuration = configuration;  
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;        
    }  

        [HttpDelete]
        public IActionResult deleteUser(int id) {
            try
            {
               _userService.Delete(id);
            return Ok();
           }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}