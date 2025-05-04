using Service.DTOs.Group;
using Service.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Task CreateAsync(GroupCreateDto group);
        Task<IEnumerable<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(GroupUpdateDto group);
        Task<IEnumerable<GroupDto>> Search(string text);
        Task<IEnumerable<GroupDto>> SortByName(string sort);
    }
}
