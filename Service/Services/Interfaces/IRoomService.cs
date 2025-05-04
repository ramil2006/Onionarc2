using Service.DTOs.Education;
using Service.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IRoomService
    {

        Task CreateAsync(RoomCreateDto room);
        Task<IEnumerable<RoomDto>> GetAllAsync();
        Task<RoomDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(RoomUpdateDto room);
        Task<IEnumerable<RoomDto>> Search(string text);
        Task<IEnumerable<RoomDto>> SortByName(string sort);
    }
}
