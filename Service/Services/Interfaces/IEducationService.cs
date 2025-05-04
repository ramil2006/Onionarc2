using Service.DTOs.Education;
using Service.DTOs.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Interfaces
{
    public interface IEducationService
    {
        Task CreateAsync(EducationCreateDto education);
        Task<IEnumerable<EducationDto>> GetAllAsync();
        Task<EducationDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(EducationUpdateDto education);
        Task<IEnumerable<EducationDto>> Search(string text);
        Task<IEnumerable<EducationDto>> SortByName(string sort);
    }
}
