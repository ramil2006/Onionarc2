using AutoMapper;
using Domain.Models;
using Repository.Data;
using Repository.Repositories.Interfaces;
using Service.DTOs.Education;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _repository;
        private readonly IMapper _mapper;
        public EducationService(IEducationRepository repository,
                                IMapper mapper)
        {
            _repository = repository;
                _mapper = mapper;
        }
        public async Task CreateAsync(EducationCreateDto education)
        {
            await _repository.CreateAsync(_mapper.Map<Education>(education));
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EducationDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<EducationDto>>(await _repository.GetAllAsync());
        }

        public async Task<EducationDto> GetByIdAsync(int id)
        {
            return _mapper.Map<EducationDto>(await _repository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<EducationDto>> Search(string text)
        {
            return _mapper.Map<IEnumerable<EducationDto>>(await _repository.Search(text));

        }

        public async Task<IEnumerable<EducationDto>> SortByName(string sort)
        {
            return _mapper.Map<IEnumerable<EducationDto>>(await _repository.SortByName(sort));
        }

        public async Task UpdateAsync(EducationUpdateDto education)
        {
            await _repository.UpdateAsync(_mapper.Map<Education>(education));
        }
    }
}
