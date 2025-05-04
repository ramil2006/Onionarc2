using Azure.Core;
using Domain.Models;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using Service.DTOs.Account;
using Service.Helpers;
using Service.Helpers.Enums;
using Service.Helpers.Responses;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSetting _jwtSetting;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IOptions<JwtSetting> options, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSetting = options.Value;
            _configuration = configuration;
        }

        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            List<UserDto> users = [];

            var dbUsers = await _userManager.Users.ToListAsync();
            foreach (var user in dbUsers)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                users.Add(new UserDto
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = userRoles.ToArray()
                });
            }
            return users;
        }

        public async Task<IEnumerable<RoleDto>> GetRoles()
        {
            List<RoleDto> roles = [];

            var dbRoles = await _roleManager.Roles.ToListAsync();

            foreach (var item in dbRoles)
            {
                var users = await _userManager.GetUsersInRoleAsync(item.ToString());
                roles.Add(new RoleDto
                {
                    Name = item.Name,
                    Users = users.Select(m => new UserDto
                    {
                        FullName = m.FullName,
                        Email = m.Email,
                        UserName = m.UserName
                    }).ToArray()
                });
            }
            return roles;
        }
        public async Task<LoginResponse> LoginAsync(LoginDto entity)
        {
            AppUser? user = await _userManager.FindByEmailAsync(entity.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(entity.UserNameOrEmail);
            if (user == null)
                return new LoginResponse { Succes = false, Token = null, ErrorMessage = "no user found" };

            bool result = await _userManager.CheckPasswordAsync(user,entity.Password);

            if (!result)
                return new LoginResponse { Succes = false, Token = null, ErrorMessage = "no user found" };
            var roles = await _userManager.GetRolesAsync(user);
            var token = GenerateJwtToken(entity.UserNameOrEmail,roles.ToList());
            return new LoginResponse { Succes = true, ErrorMessage = null, Token = token };
        }
        //public async Task<RegisterResponse> RegisterAsync(RegisterDto entity)
        //{
        //    AppUser user = new AppUser()
        //    {
        //        FullName = entity.FullName,
        //        Email = entity.Email,
        //        UserName = entity.UserName,
        //    };

        //    IdentityResult result = await _userManager.CreateAsync(user, entity.Password);
        //    if (!result.Succeeded)
        //    {
        //        return new RegisterResponse
        //        {
        //            Success = false,
        //            Errors = result.Errors.Select(m => m.Description).ToArray()
        //        };
        //    }

        //    await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
        //    return new RegisterResponse { Success = true, Errors = null };
        //}

        public async Task AddRole(string UserId,string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user==null)
                throw new NullReferenceException("No User Found");

            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
                throw new NullReferenceException("No Role Found");
            await _userManager.AddToRoleAsync(user, role.ToString());
        }
        public async Task RemoveRole(string UserId, string RoleId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
                throw new NullReferenceException("No User Found");
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null)
                throw new NullReferenceException("No Role Found");
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count <= 1)
                throw new Exception("A user must have at least one role");
            if (role.ToString().ToLower()=="member")
                throw new Exception("Can't remove member role");
            await _userManager.RemoveFromRoleAsync(user, role.ToString());
        }

        private string GenerateJwtToken(string username, List<string> roles)
        {
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, username)
        };

            roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSetting.ExpireDays));

            var token = new JwtSecurityToken(
                _jwtSetting.Issuer,
                _jwtSetting.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}