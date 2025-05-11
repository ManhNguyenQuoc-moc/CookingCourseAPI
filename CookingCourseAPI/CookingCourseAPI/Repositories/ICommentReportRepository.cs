using CookingCourseAPI.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories.Interfaces
{
    public interface ICommentReportRepository : IGenericRepository<CommentReport>
    {
        Task<IEnumerable<CommentReport>> GetReportedCommentsWithDetailsAsync();
    }
}