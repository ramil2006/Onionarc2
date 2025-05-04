using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using OnionApi.Controllers.Admin;
using Service.DTOs.Student;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.UI
{
    public class StudentController : BaseController
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
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Error");

            return Ok(await _studentService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _studentService.GetByIdAsync(id));
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string text)
        {
            return Ok(await _studentService.Search(text));
        }
        [HttpGet]
        public async Task<IActionResult> SortByAge([FromQuery] string sort)
        {
            return Ok(await _studentService.SortByAge(sort));
        }
    }
}
