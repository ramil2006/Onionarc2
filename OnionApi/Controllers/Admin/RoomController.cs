using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Room;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.Admin
{
    public class RoomController : BaseAdminController
    {
        private readonly IRoomService _service;
        private readonly ILogger<RoomController> _logger;
        public RoomController(IRoomService service,
                               ILogger<RoomController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            _logger.LogInformation("Delete method working");
            await _service.DeleteAsync(id);
            _logger.LogInformation("Delete method finished");
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Created");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RoomUpdateDto request)
        {
            await _service.UpdateAsync(request);
            return Ok();
        }
    }
}
