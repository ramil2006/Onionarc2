using Domain.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using OnionApi.Controllers.UI;
using Service.DTOs.Account;
using Service.Helpers.Enums;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using Service.Helpers;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace StudentApp_API.Controllers.UI
{
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _accountService = accountService;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            //var result = await _accountService.RegisterAsync(request);
            //return Ok(result);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            AppUser user = new AppUser
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
                return BadRequest() ;

            }
            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string url = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, Request.Scheme, Request.Host.ToString());
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("orujnov11@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
            email.Subject = "Email Confirmation";
            email.Body = new TextPart(TextFormat.Plain) { Text = $"<a href='{url}'Click Here</a>" };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("orujnov11@gmail.com", "dymx bjon khsf lowv");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok();
        }

        

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto request) => Ok(await _accountService.LoginAsync(request));
    }

}
