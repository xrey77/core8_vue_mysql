using System;
using AutoMapper;
using Google.Authenticator;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using core8_vue_mysql.Models;
using core8_vue_mysql.Services;

namespace core8_vue_mysql.Controllers.Auth
{
    [ApiExplorerSettings(GroupName = "Validate OTP Code from Authenticator App")]
    [ApiController]
    [Route("[controller]")]
    public class OtpController : ControllerBase {

    private IUserService _userService;
    private IMapper _mapper;
    private readonly IConfiguration _configuration;  

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<OtpController> _logger;

    public OtpController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMapper mapper,
        ILogger<OtpController> logger
        )
    {
        _configuration = configuration;  
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;        
    }  

        [HttpPost("/api/validateotp")]
        public IActionResult validateOTP(OtpModel model) {
            try {
                var user = _userService.GetById(model.Id);
                if (user != null) {
                    var secret = user.Secretkey;
                    var otp = model.Otp;
                    TwoFactorAuthenticator twoFactor =  new TwoFactorAuthenticator();
                    bool isValid = twoFactor.ValidateTwoFactorPIN(secret, otp , false);
                    if (isValid)
                    {
                        return Ok(new { statuscode=200, message = "OTP validation successfull, pls. wait.", username=user.UserName});
                    } 
                }
                return Ok(new { statuscode=404, message = "Invalid OTP Code." });
            }catch(Exception ex) {
                return Ok(new { statuscode=404, message = ex.Message});
            }
        }
    }
}