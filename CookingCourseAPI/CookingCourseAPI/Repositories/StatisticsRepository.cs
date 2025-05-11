using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Dictionary<int, int> GetUserRegistrationsByMonth(int year)
        {
            return _context.Users
                .Where(u => u.CreatedAt.Year == year)
                .GroupBy(u => u.CreatedAt.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToDictionary(g => g.Month, g => g.Count);
        }

        public (int FreeCourses, int PaidCourses) GetCourseCountByType()
        {
            int free = _context.Courses.Count(c => c.IsFree);
            int paid = _context.Courses.Count(c => !c.IsFree);
            return (free, paid);
        }
        public List<CourseRevenueDto> GetPaidCourseRevenue()
        {
            var result = _context.Courses
                .Where(c => !c.IsFree)
                .Select(c => new CourseRevenueDto
                {
                    CourseId = c.Id,
                    CourseName = c.Name,
                    Price = c.Price,
                    EnrollmentCount = c.Enrollments.Count(),
                    Revenue = c.Price * c.Enrollments.Count()
                })
                .ToList();

            return result;
        }

    }

}
