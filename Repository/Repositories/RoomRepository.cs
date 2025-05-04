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
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        private readonly AppDbContext _context;
        public RoomRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> Search(string text)
        {
            return await _context.Rooms.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Room>> SortByName(string sort)
        {
            List<Room> datas = new();
            switch (sort)
            {
                case "desc":
                    datas = await _context.Rooms.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "asc":
                    datas = await _context.Rooms.OrderBy(s => s.Name).ToListAsync();
                    break;

                default:
                    datas = await _context.Rooms.ToListAsync();
                    break;
            }
            return datas;
        }
    }
}
