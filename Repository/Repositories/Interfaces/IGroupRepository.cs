using Domain.Models;

namespace Repository.Repositories.Interfaces
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        Task<IEnumerable<Group>> Search(string text);
        Task<IEnumerable<Group>> SortByName(string sort);
    }
}
