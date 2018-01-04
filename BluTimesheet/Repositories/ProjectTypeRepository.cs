using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;

namespace BluTimesheet.Repositories
{
    public class ProjectTypeRepository : GenericRepository<ProjectType>
    {
        
        public ProjectTypeRepository(TimesheetDbContext context) : base(context)
        {
            
        }

        
    }
}