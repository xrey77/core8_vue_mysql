#nullable enable
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Google.Authenticator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using core8_vue_mysql.Models;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Services;
using Microsoft.Extensions.Caching.Memory;

namespace core8_vue_mysql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Enable or Disable 2-Factor Authentication")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ActivatemfaController : ControllerBase {

    private IUserService _userService;
    private readonly IMemoryCache _cache;
    private IMapper _mapper;
    private readonly IConfiguration _configuration;  

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<ActivatemfaController> _logger;

    public ActivatemfaController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMapper mapper,
        IMemoryCache cache,
        ILogger<ActivatemfaController> logger
        )
    {
        _cache = cache;
        _configuration = configuration;  
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;        
    }  

        [HttpPatch("/api/activatemfa/{id}")]
        public async Task<IActionResult> EnableMFA(int id, MfaModel model) 
        {
            if (model.Twofactorenabled == true) 
            {
                var user = await _userService.GetById(id);
                if (user == null) return NotFound(new { message = "User not found." });

                // 1. Define a unique cache key for this user
                string cacheKey = $"mfa_setup_{id}";

                // 2. Try to get existing setup info from cache to avoid regenerating
                if (!_cache.TryGetValue(cacheKey, out string? imageUrl))
                {
                    var fullname = $"{user.FirstName} {user.LastName}";
                    var twoFactor = new TwoFactorAuthenticator();
                    var setupInfo = twoFactor.GenerateSetupCode(fullname, user.Email, user.Secretkey, false, 3);
                    imageUrl = setupInfo.QrCodeSetupImageUrl;

                    // 3. Set cache options (e.g., expire in 5 minutes)
                    var cacheOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                    // 4. Save to cache
                    _cache.Set(cacheKey, imageUrl, cacheOptions);
                }

                await _userService.ActivateMfa(id, true, imageUrl);
                
                return Ok(new {
                    message = "Muliti-Factor has been enabled. (cached for 5 minutes)",
                    qrcode = imageUrl
                });
            } 
            else 
            {
                // Remove from cache if they disable MFA
                _cache.Remove($"mfa_setup_{id}");
                await _userService.ActivateMfa(id, false, null);
                return Ok(new { message = "Multi-Factor Authenticator has been disabled." });
            }
        }

        // [HttpPatch("/api/activatemfa/{id}")]
        // public async Task<IActionResult> EnableMFA(int id,MfaModel model) {
        //     if (model.Twofactorenabled == true) {
        //         var user = await _userService.GetById(id);
        //         if(user != null) {
        //             QRCode qrimageurl = new QRCode();
        //             var fullname = user.FirstName + " " + user.LastName;
        //             TwoFactorAuthenticator twoFactor = new TwoFactorAuthenticator();
        //             var setupInfo = twoFactor.GenerateSetupCode(fullname, user.Email, user.Secretkey, false, 3);
        //             var imageUrl = setupInfo.QrCodeSetupImageUrl;
        //             await _userService.ActivateMfa(id, true, imageUrl);
        //             return Ok(new {
        //                 message="Multi-Factor Authenticator has been enabled.",
        //                 qrcode=imageUrl});
        //         } else {
        //             return NotFound(new {message="User not found."});
        //         }
        //     } else {
        //         await _userService.ActivateMfa(id, false, null);
        //         return Ok(new {message="Multi-Factor Authenticator has been disabled."});
        //     }
        // }
    }    
}