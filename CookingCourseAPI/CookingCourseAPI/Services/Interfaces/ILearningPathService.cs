using CookingCourseAPI.DTOs;
using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface ILearningPathService
    {
        Task<LearningPath> CreateAsync(LearningPathDto dto);
        Task<LearningPath> UpdateAsync(int id, LearningPathDto dto);
        Task<bool> DeleteAsync(int id);
        Task<LearningPath> GetByIdAsync(int id);
        Task<IEnumerable<LearningPathDto>> GetAllAsync();

    }
}
