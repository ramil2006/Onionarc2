using Domain.Models;


namespace Repository.Repositories.Interfaces
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<IEnumerable<Student>> Search(string text);
        Task<IEnumerable<Student>> SortByAge(string sort);
    }
}
