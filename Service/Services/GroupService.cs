using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Repository.Repositories.Interfaces;
using Service.DTOs.Group;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _group;
        private readonly IStudentRepository _student;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        
        public GroupService(IGroupRepository group,
                            IMapper mapper,
                            IWebHostEnvironment env,
                            IStudentRepository student)
        {
            _group = group;
            _mapper = mapper;
            _env = env;
            _student = student;
        }
        public async Task CreateAsync(GroupCreateDto group)
        {
            await _group.CreateAsync(_mapper.Map<Group>(group));
        }

        public async Task DeleteAsync(int id)
        {
            var students = await _student.FindByConditionAsync(m => m.GroupId == id);

            if (students is not null)
            {
                foreach (var student in students)
                {
                    var imagePath = Path.Combine(_env.WebRootPath, "images", student.Image);

                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
            }

            await _group.DeleteAsync(id);
        }


        public async Task<IEnumerable<GroupDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<GroupDto>>(await _group.GetAllAsync());
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            return _mapper.Map<GroupDto>(await _group.GetByIdAsync(id));
        }

        public async Task<IEnumerable<GroupDto>> Search(string text)
        {
            return _mapper.Map<IEnumerable<GroupDto>>(await _group.Search(text));
        }

        public async Task<IEnumerable<GroupDto>> SortByName(string sort)
        {
            return _mapper.Map<IEnumerable<GroupDto>>(await _group.SortByName(sort));
        }

        public async Task UpdateAsync(GroupUpdateDto group)
        {
            await _group.UpdateAsync(_mapper.Map<Group>(group));
        }
    }
}
