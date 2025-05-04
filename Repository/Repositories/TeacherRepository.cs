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
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        private readonly AppDbContext _ctx;
        public TeacherRepository(AppDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Teacher>> Search(string text)
        {
            return await _ctx.Teachers.Where(s => s.Name.Trim().ToLower().Contains(text.Trim().ToLower())

            || s.Surname.Trim().ToLower().Contains(text.Trim().ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Teacher>> SortByName(string sort)
        {
            List<Teacher> datas = new();
            switch (sort)
            {
                case "desc":
                    datas = await _ctx.Teachers.OrderByDescending(s => s.Name).ToListAsync();
                    break;
                case "asc":
                    datas = await _ctx.Teachers.OrderBy(s => s.Name).ToListAsync();
                    break;

                default:
                    datas = await _ctx.Teachers.ToListAsync();
                    break;
            }
            return datas;
        }

    }
}
