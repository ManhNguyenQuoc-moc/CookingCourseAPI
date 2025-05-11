using CookingCourseAPI.DTOs;

namespace CookingCourseAPI.Services.Interfaces
{
    public interface IStatisticsService
    {
        Dictionary<int, int> GetMonthlyUserRegistrations(int year);
        (int FreeCourses, int PaidCourses) GetCourseTypeStatistics();
        List<CourseRevenueDto> GetPaidCourseRevenue();
    }

}
