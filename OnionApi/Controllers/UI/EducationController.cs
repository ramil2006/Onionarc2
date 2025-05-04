using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionApi.Controllers.Admin;
using Service.DTOs.Education;
using Service.Services.Interfaces;

namespace OnionApi.Controllers.UI
{
   
    public class EducationController : BaseController
    {
        private readonly IEducationService _service;
        public EducationController(IEducationService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var data= await _service.GetByIdAsync(id);
            if (data == null)
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
