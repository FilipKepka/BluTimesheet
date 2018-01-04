using BluTimesheet.Models.DbModels;
using System;
using System.Collections.Generic;

namespace BluTimesheet.Services.interfaces
{
    public interface IReportService
    {
        //   int SumOfHoursSpentOnProject(int projectId);
        //   IDictionary<string, int> SumOfHoursSpentOnProjectPerUser(int projectId);        
        //   IDictionary<string, int> SumOfHoursSpentOnExactActivityTypePerUser(int activityTypeId);
        //   IDictionary<ActivityType, int> SumOfHoursUserSpentOnExactActivityType(int userId);
        IEnumerable<Activity> GetActivitesByUserPerMonth(int ReportFrom, int ReportTo, int userId);
        IEnumerable<Activity> GetActivitesByUserr(string userId);
        IEnumerable<Activity> GetActivitesByProjectPerMonth(DateTime ReportFrom, DateTime ReportTo, int userId);
    }
}
