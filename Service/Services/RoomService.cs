using AutoMapper;
using Domain.Models;
using Repository.Repositories.Interfaces;
using Service.DTOs.Room;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repo;
        private readonly IGroupRepository _group;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository repo,
                           IMapper mapper,
                           IGroupRepository group)
        {
            _repo = repo;
            _mapper = mapper;
            _group = group;
        }
        public async Task CreateAsync(RoomCreateDto room)
        {
            await _repo.CreateAsync(_mapper.Map<Room>(room));
        }

        public async Task DeleteAsync(int id)
        {
            var existRoom=await _repo.GetByIdAsync(id);
            var hasGroup = await _group.FindByConditionAsync(m=>m.RoomId==id);
            if (hasGroup.Count()>0)
            {
                throw new Exception("There are groups in the room");
            }
            await _repo.DeleteAsync(id);
        }

        public async Task<IEnumerable<RoomDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _repo.GetAllAsync());
        }

        public async Task<RoomDto> GetByIdAsync(int id)
        {
            return _mapper.Map<RoomDto>(await _repo.GetByIdAsync(id));
        }

        public async Task<IEnumerable<RoomDto>> Search(string text)
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _repo.Search(text));
        }

        public async Task<IEnumerable<RoomDto>> SortByName(string sort)
        {
            return _mapper.Map<IEnumerable<RoomDto>>(await _repo.SortByName(sort));
        }

        public async Task UpdateAsync(RoomUpdateDto room)
        {
           await _repo.UpdateAsync(_mapper.Map<Room>(room));
        }
    }
}
