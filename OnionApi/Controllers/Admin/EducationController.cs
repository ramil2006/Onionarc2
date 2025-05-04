using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Education;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.Admin
{
    public class EducationController : BaseAdminController
    {
        private readonly IEducationService _service;
        public EducationController(IEducationService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationCreateDto request)
        {
            await _service.CreateAsync(request);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EducationUpdateDto requset)
        {
            await _service.UpdateAsync(requset);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
