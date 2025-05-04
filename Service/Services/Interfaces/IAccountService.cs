using Service.DTOs.Account;
using Service.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IAccountService
    {
       
        Task CreateRoles();
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<IEnumerable<RoleDto>> GetRoles();
        Task AddRole(string UserId, string RoleId);
        Task RemoveRole(string UserId, string RoleId);
        Task<LoginResponse> LoginAsync(LoginDto entity);
    }
}
