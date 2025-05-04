using Service.DTOs.Student;

namespace Service.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(StudentCreateDto student);
        Task<IEnumerable<StudentDto>> GetAllAsync();
        Task<StudentDto> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(StudentUpdateDto student);
        Task<IEnumerable<StudentDto>> Search(string text);
        Task<IEnumerable<StudentDto>> SortByAge(string sort);
    }
}
