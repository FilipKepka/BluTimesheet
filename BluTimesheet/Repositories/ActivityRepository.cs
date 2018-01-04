using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;

namespace BluTimesheet.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>
    {
        
        public ActivityRepository(TimesheetDbContext context) : base(context)
        {           
        }
        
        public void Approve(Activity activity)
        {
            var activityFromDb = Get(activity.Id);
            if (activityFromDb != null)
            {
                activityFromDb.ApprovedByManager = true;
                context.SaveChanges();
            }
        }
    }
}