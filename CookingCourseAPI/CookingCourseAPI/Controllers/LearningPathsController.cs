using CookingCourseAPI.DTOs;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearningPathsController : ControllerBase
    {
        private readonly ILearningPathService _service;

        public LearningPathsController(ILearningPathService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(LearningPathDto dto)
        {
            var path = await _service.CreateAsync(dto);
            return Ok(path);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LearningPathDto dto)
        {
            var path = await _service.UpdateAsync(id, dto);
            if (path == null) return NotFound();
            return Ok(path);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var path = await _service.GetByIdAsync(id);
            if (path == null) return NotFound();
            return Ok(path);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var paths = await _service.GetAllAsync();
            return Ok(paths);
        }

    }
}
