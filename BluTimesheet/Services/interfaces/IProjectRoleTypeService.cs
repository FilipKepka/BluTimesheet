using BluTimesheet.Models.DbModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluTimesheet.Services.interfaces
{
    public interface IProjectRoleTypeService
    {
        ProjectRoleType Add(ProjectRoleType projectRoleType);
        void Remove(int id);
        ProjectRoleType Get(int id);
        ProjectRoleType Update(ProjectRoleType projectRoleType);
        IEnumerable<ProjectRoleType> GetAll();
    }
}
