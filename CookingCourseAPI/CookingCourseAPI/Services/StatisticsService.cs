using CookingCourseAPI.Data;
using CookingCourseAPI.DTOs;
using CookingCourseAPI.Repositories.Interfaces;
using CookingCourseAPI.Services.Interfaces;
using System.Linq;

namespace CookingCourseAPI.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public Dictionary<int, int> GetMonthlyUserRegistrations(int year)
        {
            return _statisticsRepository.GetUserRegistrationsByMonth(year);
        }

        public (int FreeCourses, int PaidCourses) GetCourseTypeStatistics()
        {
            return _statisticsRepository.GetCourseCountByType();
        }
        public List<CourseRevenueDto> GetPaidCourseRevenue()
        {
            return _statisticsRepository.GetPaidCourseRevenue();
        }

    }

}