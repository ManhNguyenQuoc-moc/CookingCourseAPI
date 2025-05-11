using CookingCourseAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookingCourseAPI.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Dictionary<int, int> GetUserRegistrationsByMonth(int year);
        (int FreeCourses, int PaidCourses) GetCourseCountByType();
        List<CourseRevenueDto> GetPaidCourseRevenue();

    }


}
