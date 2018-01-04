using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;

namespace BluTimesheet.Repositories
{
    public class ProjectRoleTypeRepository : GenericRepository<ProjectRoleType>
    {

        public ProjectRoleTypeRepository(TimesheetDbContext context) : base(context)
        {

        }
    }
}