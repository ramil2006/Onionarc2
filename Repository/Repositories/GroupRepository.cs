using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class GroupRepository :BaseRepository<Group>,IGroupRepository
    {
        private readonly AppDbContext _context;
        public GroupRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Group>> Search(string text)
        {
            return await _context.Groups.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Group>> SortByName(string sort)
        {
            List<Group> datas = new();
            switch (sort)
            {
                case "desc":
                    datas = await _context.Groups.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "asc":
                    datas = await _context.Groups.OrderBy(s => s.Name).ToListAsync();
                    break;

                default:
                    datas = await _context.Groups.ToListAsync();
                    break;
            }
            return datas;
        }
    }
}
