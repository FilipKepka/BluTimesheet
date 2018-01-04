using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;

namespace BluTimesheet.Repositories
{
    public class ActivityTypeRepository : GenericRepository<ActivityType>
    {
       
        public ActivityTypeRepository(TimesheetDbContext context) : base(context)
        {
            
        }
    }
}