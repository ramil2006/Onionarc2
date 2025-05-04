using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApi.Controllers.Admin;
using Service.DTOs.Room;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.UI
{
    
    public class RoomController : BaseController
    {
        private readonly IRoomService _service;
        private readonly ILogger<RoomController> _logger;
        public RoomController(IRoomService service,
                               ILogger<RoomController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string text)
        {
            return Ok(await _service.Search(text));
        }
        [HttpGet]
        public async Task<IActionResult> SortByName([FromQuery] string sort)
        {
            return Ok(await _service.SortByName(sort));
        }
    }
}
