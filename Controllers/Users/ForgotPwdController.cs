using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.IO;
using AutoMapper;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Services;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Helpers;

namespace core8_vue_mysql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Forgot User Password")]
    [ApiController]
    [Route("[controller]")]
    public class ForgotPwdController : ControllerBase {

    private IMapper _mapper;
    private IUserService _userService;
    private EmailService _emailService;    
    private readonly IConfiguration _configuration;  
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ForgotPwdController> _logger;

    public ForgotPwdController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IMapper mapper,
        IUserService userService,
        EmailService emailService,
        ILogger<ForgotPwdController> logger
        )
    {
        _configuration = configuration;  
        _logger = logger;
        _mapper = mapper;
        _userService = userService;
        _emailService = emailService;
        _env = env;        
    }  

        [HttpPatch("/api/resetpassword/{email}")]
        public async Task<IActionResult> ResetPassword(string email, [FromBody]ForgotPassword model)
        {
                try {
                    var user = new User();
                    user.Mailtoken = model.Mailtoken;
                    user.Password = model.Password;
                    await _userService.ForgotPassword(user);
                    return Ok(new {message = "Password successfully changed.." });
                }
                catch (AppException ex)
                {
                    return BadRequest(new {message = ex.Message });
                }
        }

        [HttpPost("/api/emailtoken")]
        public async Task<IActionResult> EmailToken([FromBody]MailTokenModel model)
        {
           try {
             int etoken = await _userService.SendEmailToken(model.Email);             
             await _emailService.sendMailToken(model.Email,"Mail Token","Please copy or enter this token in forgot password option. " + etoken.ToString());
            return Ok(new { etoken = etoken});
           }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }    
}