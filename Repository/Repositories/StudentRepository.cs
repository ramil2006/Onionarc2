using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _ctx;
        public StudentRepository(AppDbContext context) : base(context) 
        {
            _ctx = context;
        }

       

        public async Task<IEnumerable<Student>> Search(string text)
        {
            return await _ctx.Students.Where(s=>s.FullName.Trim().ToLower().Contains(text.Trim().ToLower())).ToListAsync();
        }

        public async Task<IEnumerable<Student>> SortByAge(string sort)
        {
            List<Student> datas = new();
            switch (sort)
            {
                case "desc":
                    datas = await _ctx.Students.OrderByDescending(s => s.Age).ToListAsync();
                    break;
                case "asc":
                    datas = await _ctx.Students.OrderBy(s => s.Age).ToListAsync();
                    break;

                default:
                    datas=await _ctx.Students.ToListAsync();
                    break;
            }
            return datas;
        }
    }
}
