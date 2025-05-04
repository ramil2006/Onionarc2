using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Group;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.Admin
{
    public class GroupController : BaseAdminController
    {
        private readonly IGroupService _service;
        private readonly ILogger<GroupController> _logger;
        public GroupController(IGroupService service,
                                ILogger<GroupController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create),"Succeflyy");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] GroupUpdateDto request)
        {
            await _service.UpdateAsync(request);
            return Ok();
        }
    }
}
