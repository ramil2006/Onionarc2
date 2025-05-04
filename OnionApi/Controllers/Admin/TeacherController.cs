using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Teacher;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.Admin
{
    public class TeacherController : BaseAdminController
    {
        private readonly ITeacherService _service;
        private readonly ILogger<TeacherController> _logger;
        public TeacherController(ITeacherService service,
                                 ILogger<TeacherController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {
            await _service.CreateAsync(request);
            return CreatedAtAction(nameof(Create), "Succecfully Created");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TeacherUpdateDto request)
        {
            await _service.UpdateAsync(request);
            return Ok("Update Success");
        }
    }
}
