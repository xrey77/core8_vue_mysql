using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.IO;
using core8_vue_mysql.Models.dto;
using core8_vue_mysql.Services;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Helpers;

namespace core8_vue_mysql.Controllers.Auth
{
    
[ApiExplorerSettings(GroupName = "Sign-up or Account Registration")]
[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    /* private EmailService _emailService;    */
    private IAuthService _authService;
    private IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly IConfiguration _configuration;  
    private readonly ILogger<RegisterController> _logger;
    
    public RegisterController(
        IWebHostEnvironment env,
        IAuthService userService,
        IConfiguration configuration,
        IMapper mapper,
        ILogger<RegisterController> logger
        /* EmailService emailService,*/
        )
    {   
        _authService = userService;
        _configuration = configuration;  
        _mapper = mapper;
        _logger = logger;
        _env = env;
        /* _emailService = emailService; */
    }  

    [HttpPost("/signup")]
    public IActionResult signup(UserRegister model) {
        DateTime now = DateTime.Now;
        var user = _mapper.Map<User>(model);
            try
            {
                user.LastName = model.Lastname;
                user.FirstName = model.Firstname;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.UserName = model.Username;
                user.CreatedAt = now;
                user.Isactivated = 1;                
                _authService.SignupUser(user, model.Password);
                string fullname = model.Firstname + " " + model.Lastname;
                string emailaddress = model.Email;
                // string htmlmsg = "<div><p>Please click Activate button below to confirm you email address and activate your account.</p>"+
                //             "<a href=\"https://localhost:7280/api/activateuser/id=" + user.Id.ToString() + "\" style=\"background-color: green;color:white;text-decoration: none;border-radius: 20px; \">&nbsp;&nbsp; Activate Account &nbsp;&nbsp;</a></div>";
                // string subject = "Barclays Account Activation";                
                // IF YOU WISH TO USE USER EMAIL ACTIVATION, JUST UNCOMMENT _emailService.sendMail
                // _emailService.sendMail(emailaddress, fullname, subject, htmlmsg);
                // and comment  user.Isactivated = 1;    
                return Ok(new {statuscode = 200, message = "Please check your e-mail inbox and click button activation"});
            }
            catch (AppException ex)
            {
                return Ok(new { statuscode = 404, message = ex.Message });
            }
    }



}
    
}