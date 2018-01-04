using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;

namespace BluTimesheet.Repositories
{
    public class ProjectRepository : GenericRepository<Project>
    {

        public ProjectRepository(TimesheetDbContext context) : base(context)
        {
            
        }        
    }
}