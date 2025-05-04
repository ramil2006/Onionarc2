using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly AppDbContext _context;
        public EducationRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Education>> Search(string text)
        {
            return await _context.Educations.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Education>> SortByName(string sort)
        {
            List<Education> datas = new();
            switch (sort)
            {
                case "desc":
                    datas = await _context.Educations.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "asc":
                    datas = await _context.Educations.OrderBy(s => s.Name).ToListAsync();
                    break;

                default:
                    datas = await _context.Educations.ToListAsync();
                    break;
            }
            return datas;
        }
    }
}
