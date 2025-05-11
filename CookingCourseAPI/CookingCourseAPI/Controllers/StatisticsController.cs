using CookingCourseAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        // GET: api/Statistics/UserRegistrations?year=2025
        [HttpGet("UserRegistrations")]
        public IActionResult GetUserRegistrations([FromQuery] int year)
        {
            var data = _statisticsService.GetMonthlyUserRegistrations(year);
            var result = Enumerable.Range(1, 12)
                .ToDictionary(month => month, month => data.ContainsKey(month) ? data[month] : 0);
            return Ok(result);
        }

        // GET: api/Statistics/CourseTypes
        [HttpGet("CourseTypes")]
        public IActionResult GetCourseTypes()
        {
            var (free, paid) = _statisticsService.GetCourseTypeStatistics();
            return Ok(new { FreeCourses = free, PaidCourses = paid });
        }
        // GET: api/Statistics/CourseRevenue
        [HttpGet("CourseRevenue")]
        public IActionResult GetCourseRevenue()
        {
            var data = _statisticsService.GetPaidCourseRevenue();
            return Ok(data);
        }

    }
}
