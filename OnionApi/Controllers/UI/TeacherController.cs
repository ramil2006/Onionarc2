using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApi.Controllers.Admin;
using Service.DTOs.Teacher;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.UI
{
    
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _service;
        private readonly ILogger<TeacherController> _logger;
        public TeacherController(ITeacherService service,
                                 ILogger<TeacherController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAll is Success ");
            return Ok(await _service.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data= await _service.GetByIdAsync(id);
            if (data is null)
            { 
                return NotFound();
            }
            return Ok(data);
        }
        
        [HttpGet]
        public async Task<IActionResult> SortByName([FromQuery] string sort)
        {
            return Ok(await _service.SortByName(sort));
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string text)
        {
            return Ok(await _service.Search(text));
        }
    }
}
