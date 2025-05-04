using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.DTOs.Teacher;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _repository;
        private readonly IMapper _mapper;
        private readonly IGroupRepository _group;
        public TeacherService(ITeacherRepository repository,
                               IMapper mapper,
                               IGroupRepository group)
        {
            _repository = repository;
            _mapper = mapper;
            _group = group;
        }
        public async Task CreateAsync(TeacherCreateDto teacher)
        {
            await _repository.CreateAsync(_mapper.Map<Teacher>(teacher));
        }

        public async Task DeleteAsync(int id)
        {
            var groups = await _group.FindByConditionAsync(m=>m.TeacherId==id);
            if (groups.Count()>0)
            {
                throw new Exception("This teacher has groups");
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TeacherDto>> GetAllAsync()
        {
            var data= _mapper.Map<IEnumerable<TeacherDto>>(await _repository.GetAllAsync());
            return data;
        }

        public async Task<TeacherDto> GetByIdAsync(int id)
        {
            return _mapper.Map<TeacherDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<TeacherDto>> Search(string text)
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _repository.Search(text));
        }

        public async Task<IEnumerable<TeacherDto>> SortByName(string sort)
        {
            return _mapper.Map<IEnumerable<TeacherDto>>(await _repository.SortByName(sort));
        }

        public async Task UpdateAsync(TeacherUpdateDto teacher)
        {
            await _repository.UpdateAsync(_mapper.Map<Teacher>(teacher));
        }
    }
}
