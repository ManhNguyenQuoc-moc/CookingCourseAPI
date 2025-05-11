using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
        }

        // GET: api/news
        [HttpGet]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            var newsList = await _service.GetAllNewsAsync();
            return Ok(newsList);
        }

        // GET: api/news/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> GetById(int id)
        {
            var news = await _service.GetNewsByIdAsync(id);
            if (news == null) return NotFound();
            return Ok(news);
        }

        // POST: api/news
        [HttpPost]
        public async Task<ActionResult<News>> Create([FromBody] News news)
        {
            var created = await _service.CreateNewsAsync(news);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/news/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<News>> Update(int id, [FromBody] News news)
        {
            var existingNews = await _service.GetNewsByIdAsync(id);
            if (existingNews == null) return NotFound();

            existingNews.Title = news.Title;
            existingNews.Content = news.Content;

            var updated = await _service.UpdateNewsAsync(existingNews);
            return Ok(updated);
        }

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteNewsAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
