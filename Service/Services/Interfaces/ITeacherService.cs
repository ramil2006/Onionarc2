using Service.DTOs.Student;
using Service.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface ITeacherService
    {
        Task CreateAsync(TeacherCreateDto teacher);
        Task<IEnumerable<TeacherDto>> GetAllAsync();
        Task<TeacherDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(TeacherUpdateDto teacher);
        Task<IEnumerable<TeacherDto>> Search(string text);
        Task<IEnumerable<TeacherDto>> SortByName(string sort);
    }
}
