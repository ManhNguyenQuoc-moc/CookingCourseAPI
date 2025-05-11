using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;
using CookingCourseAPI.Repositories;
using CookingCourseAPI.Services.Interfaces;

namespace CookingCourseAPI.Services
{
    public class LearningPathService : ILearningPathService
    {
        private readonly ILearningPathRepository _repo;

        public LearningPathService(ILearningPathRepository repo)
        {
            _repo = repo;
        }

        public async Task<LearningPath> CreateAsync(LearningPathDto dto)
        {
            var learningPath = new LearningPath
            {
                Title = dto.Title,
                Description = dto.Description,
                LearningPathCourses = dto.CourseIds.Select(id => new LearningPathCourse
                {
                    CourseId = id
                }).ToList()
            };

            return await _repo.CreateAsync(learningPath);
        }

        public async Task<LearningPath> UpdateAsync(int id, LearningPathDto dto)
        {
            var learningPath = await _repo.GetByIdAsync(id);
            if (learningPath == null) return null;

            learningPath.Title = dto.Title;
            learningPath.Description = dto.Description;

            // Cập nhật danh sách khóa học
            learningPath.LearningPathCourses.Clear();
            learningPath.LearningPathCourses = dto.CourseIds.Select(cid => new LearningPathCourse
            {
                CourseId = cid,
                LearningPathId = id
            }).ToList();

            return await _repo.UpdateAsync(learningPath);
        }

        public async Task<bool> DeleteAsync(int id)
            => await _repo.DeleteAsync(id);

        public async Task<LearningPath> GetByIdAsync(int id)
            => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<LearningPathDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();

            return entities.Select(lp => new LearningPathDto
            {
                Id = lp.Id,
                Title = lp.Title,
                Description = lp.Description,
                CourseIds = lp.LearningPathCourses?
                    .Select(lpc => lpc.CourseId)
                    .ToList() ?? new List<int>()
            });
        }

        public async Task<LearningPathDto> GetDtoByIdAsync(int id)
        {
            var lp = await _repo.GetByIdAsync(id);
            if (lp == null) return null;

            return new LearningPathDto
            {
                Id = lp.Id,
                Title = lp.Title,
                Description = lp.Description,
                CourseIds = lp.LearningPathCourses?
                    .Select(lpc => lpc.CourseId)
                    .ToList() ?? new List<int>()
            };
        }
    }
}
