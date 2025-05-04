using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Student;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.Admin
{
    public class StudentController : BaseAdminController
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        private readonly IWebHostEnvironment _env;
        public StudentController(IStudentService studentService,
                                 ILogger<StudentController> logger,
                                 IWebHostEnvironment env)
        {
            _studentService = studentService;
            _logger = logger;
            _env = env;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] StudentCreateDto request)
        {


            if (!ModelState.IsValid)
            {
                _logger.LogError("Error");
            }

            await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(Create),"");
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] StudentUpdateDto request)
        {
            if (id != request.Id) return BadRequest();
            await _studentService.UpdateAsync(request);
            return Ok();
        }
    }
}
