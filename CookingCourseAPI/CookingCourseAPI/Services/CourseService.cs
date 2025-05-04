using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Models.Responses;
using CookingCourseAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookingCourseAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;

        public CourseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<Course>> CreateCourseAsync(CourseCreateDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return new ApiResponse<Course>(true, "Tạo khóa học thành công", course);
        }

        public async Task<ApiResponse<IEnumerable<Course>>> GetAllCoursesAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return new ApiResponse<IEnumerable<Course>>(true, "Danh sách khóa học", courses);
        }

        public async Task<ApiResponse<Course>> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return new ApiResponse<Course>(false, "Không tìm thấy khóa học");

            return new ApiResponse<Course>(true, "Chi tiết khóa học", course);
        }

        public async Task<ApiResponse<Course>> UpdateCourseAsync(int id, CourseCreateDto dto)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return new ApiResponse<Course>(false, "Không tìm thấy khóa học");

            course.Name = dto.Name;
            course.Description = dto.Description;
            await _context.SaveChangesAsync();

            return new ApiResponse<Course>(true, "Cập nhật khóa học thành công", course);
        }

        public async Task<ApiResponse<string>> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
                return new ApiResponse<string>(false, "Không tìm thấy khóa học");

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return new ApiResponse<string>(true, "Xóa khóa học thành công");
        }
    }
}
