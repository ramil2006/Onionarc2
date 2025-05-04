using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> Search(string text);
        Task<IEnumerable<Teacher>> SortByName(string sort);
    }
}
