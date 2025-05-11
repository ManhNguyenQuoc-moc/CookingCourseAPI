using CookingCourseAPI.Models.Entities;

namespace CookingCourseAPI.Repositories.Interfaces
{
    public interface IBlogRepository : IGenericRepository<Blog>
    {
        Task<IEnumerable<Blog>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Blog>> SearchAsync(string keyword);
        Task<List<Blog>> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
        Task<Blog> GetByIdAsync(int id);

    }
}
