using BluTimesheet.Context;
using BluTimesheet.Models.DbModels;
using BluTimesheet.Repositories;
using BluTimesheet.Services.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BluTimesheet.Services.implementations
{
    public class ProjectRoleTypeService : IProjectRoleTypeService
    {
        private readonly ProjectRoleTypeRepository projectRoleTypeRepository;
        private readonly TimesheetDbContext context;

        public ProjectRoleTypeService(TimesheetDbContext context)
        {
            this.context = new TimesheetDbContext();
            this.projectRoleTypeRepository = new ProjectRoleTypeRepository(context);
        }

        public ProjectRoleType Add(ProjectRoleType projectRoleType)
        {
            return projectRoleTypeRepository.Add(projectRoleType);
        }

        public  ProjectRoleType Get(int id)
        {
            var result = projectRoleTypeRepository.Get(id);
            return result;
        }

        public IEnumerable<ProjectRoleType> GetAll()
        {
            var result = projectRoleTypeRepository.GetAll();
            return result;
        }

        public void Remove(int id)
        {
            ProjectRoleType entityToDelete = context.ProjectRoleType.Find(id);
            List<Activity> activityToDelete = new List<Activity>();
            var zmienna = context.Activity.Where(i => i.CurrentProjectRoleType.RoleId == entityToDelete.RoleId);
            foreach (var item in zmienna)
            {
                context.Activity.Remove(item);
            }
            context.ProjectRoleType.Remove(entityToDelete);
            context.SaveChanges();
        }

        public ProjectRoleType Update(ProjectRoleType projectRoleType)
        {
            ProjectRoleType projectRoleTypeTest = context.ProjectRoleType.Find(projectRoleType.RoleId);

            projectRoleTypeTest.RoleName = projectRoleType.RoleName;

            context.Entry(projectRoleTypeTest).State = EntityState.Modified;
            context.SaveChanges();

            return projectRoleTypeTest;
            //projectRoleTypeRepository.Update(projectRoleType);
        }
    }
}