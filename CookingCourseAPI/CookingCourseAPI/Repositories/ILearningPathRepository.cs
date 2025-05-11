using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories
{
    public interface ILearningPathRepository
    {
        Task<LearningPath> CreateAsync(LearningPath path);
        Task<LearningPath> UpdateAsync(LearningPath path);
        Task<bool> DeleteAsync(int id);
        Task<LearningPath> GetByIdAsync(int id);
        Task<List<LearningPath>> GetAllAsync();

    }
}
