using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IRoomRepository :IBaseRepository<Room>
    {
        Task<IEnumerable<Room>> Search(string text);
        Task<IEnumerable<Room>> SortByName(string sort);
    }
}
